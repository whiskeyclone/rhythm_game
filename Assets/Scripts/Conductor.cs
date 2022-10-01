// This class handles song timing and note spawning

using System.Collections;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public static Conductor instance;
    [SerializeField] private float bpm = 0;
    [SerializeField] private GameObject noteCircle;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Transform[] hitCircleTransforms = new Transform[4];

    [SerializeField] private float noteCircleSpawnHeight = 15; // Increase to make notes move faster
    private float songStartDspTime = 0f;
    private float songPosition = 0f;
    private float secPerBeat = 0f;
    private float songPositionInBeats = 0f;
    private int nextIndex = 0;
    private int beatsShownInAdvance = 3;
    private float beatOffset = 4;
    private Color[] noteCircleColors = new Color[4];

    // Start is called before the first frame update
    void Start()
    {
        // Set instance
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this); // Destroy instance if another instance exists
            return;
        }

        // Set note circle colors
        noteCircleColors[0] = Color.red;
        noteCircleColors[1] = Color.blue;
        noteCircleColors[2] = Color.green;
        noteCircleColors[3] = Color.yellow;

        secPerBeat = 60 / bpm;
        songStartDspTime = (float)AudioSettings.dspTime;
        StartCoroutine(WaitThenPlaySong());
    }

    // Waits for a bit to play the song so the song begins when the first note circle overlaps the hit circle
    IEnumerator WaitThenPlaySong()
    {
        float currentDspTime = (float)AudioSettings.dspTime;
        float stopDspTime = currentDspTime + secPerBeat * beatOffset;

        while (currentDspTime < stopDspTime)
        {
            currentDspTime = (float)AudioSettings.dspTime;
            yield return null;
        }

        audioManager.PlaySound("Song");
        StartCoroutine(CheckForSongEnd());
    }

    // Check if song is over. If so, display score
    IEnumerator CheckForSongEnd()
    {
        while (AudioManager.instance.IsSoundPlaying("Song"))
        {
            yield return null;
        }

        Score.instance.DisplayScore();
    }

    // Spawns a note circle set to given color targeting given beat at the given column
    void SpawnNoteCircle(int column, float targetBeat, Color color)
    {
        GameObject circleInstance = Instantiate(noteCircle);
        circleInstance.transform.position = new Vector2(hitCircleTransforms[column].position.x, noteCircleSpawnHeight);
        circleInstance.GetComponent<NoteCircle>().SetTargetBeat(targetBeat);
        circleInstance.GetComponent<NoteCircle>().SetColor(color);
    }

    public float GetSongPositionInBeats()
    {
        return (songPositionInBeats);
    }

    public float GetBeatsShownInAdvance()
    {
        return (beatsShownInAdvance);
    }

    // Checks beats at current index to see if a note should be spawned, spawns note if so
    void CheckBeats()
    {
        for (int i = 0; i < 4; i++)
        {
            // Check if index is within range of beat line
            if (nextIndex < Beats.instance.GetBeatLineLength(i))
            {
                // Check for beat time
                if (songPositionInBeats + beatsShownInAdvance >= Beats.instance.GetBeat(i))
                {
                    // Spawn Note
                    SpawnNoteCircle(i, beatOffset + Beats.instance.GetBeat(i), noteCircleColors[i]);

                    // Remove first beat of beat line
                    Beats.instance.RemoveBeat(i);
                }
            }           
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get song position
        songPosition = (float)AudioSettings.dspTime - songStartDspTime;
        songPositionInBeats = songPosition / secPerBeat;

        // Check beats
        CheckBeats();
    }
}

    
