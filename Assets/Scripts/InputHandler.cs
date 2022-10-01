using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private HitCircle[] hitCircles = new HitCircle[4];
    private KeyCode[] keys = new KeyCode[4];
    [SerializeField] private float leeway; // The max distance between the beat when the player hit the key and the beat that the current note matches that will register the note as a hit

    // Start is called before the first frame update
    void Start()
    {
        // Get keys
        keys[0] = KeyCode.V;
        keys[1] = KeyCode.B;
        keys[2] = KeyCode.N;
        keys[3] = KeyCode.M;

        // Make cursor invisible
        Cursor.visible = false;
    }

    private void CheckTiming(int colToCheck)
    {
        if ((colToCheck >= 0) && (colToCheck < 4))
        {
            // Fire raycast and get hit results
            Vector2 raycastOrigin = hitCircles[colToCheck].GetRaycastOriginPosition();

            RaycastHit2D raycastResults = Physics2D.Raycast(raycastOrigin, Vector2.up, 5 - raycastOrigin.y); // Limit raycast distance to top edge of screen

            // If raycast hit a note circle
            if ((raycastResults.collider != null) && (raycastResults.collider.tag == "Note Circle"))
            {
                NoteCircle hitNoteCircle = raycastResults.collider.GetComponent<NoteCircle>();

                if (hitNoteCircle.GetIsMiss() == false)
                {
                    float currentBeat = Conductor.instance.GetSongPositionInBeats();
                    float targetBeat = hitNoteCircle.GetTargetBeat();

                    // If difference between target beat and current beat is within leeway
                    if (Mathf.Abs(targetBeat - currentBeat) <= leeway)
                    {
                        // Set as hit
                        hitNoteCircle.SetAsHit(hitCircles[colToCheck].transform.position.x, hitCircles[colToCheck].transform.position.y);
                    }
                    else
                    {
                        // Set as miss and increment miss count of score object
                        hitNoteCircle.SetAsMiss();
                        Score.instance.IncrementMissCount();
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Invalid column!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check all keys for down input
        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyDown(keys[i]) == true)
            {
                // Play sound
                // AudioManager.instance.PlaySound("Wood Block");

                // Change hitcircle color
                hitCircles[i].PressDownColor();

                // Check timing
                CheckTiming(i);
            }
        }

        // Check all keys for up input
        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyUp(keys[i]) == true)
            {
                // Change hitcircle color
                hitCircles[i].NormalColor();
            }
        }
    }
}
