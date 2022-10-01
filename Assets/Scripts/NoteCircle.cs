using UnityEngine;

public class NoteCircle : MonoBehaviour
{
    private float fadeTime = 0.25f;
    private Vector2 startPosition;
    private bool isMoving = true;
    private bool miss = false;

    private Vector2 hitPosition; // Position where note overlaps hit circle
    private Vector2 targetPosition; // Position that note circle moves to
    private float destroyHeight; // Height where note circle is destroyed
    private float targetBeat; // Beat where the note will overlap the hit circle
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] Color missColor;

    // bool soundPlayed = false;

    private void Start()
    {
        startPosition = transform.position;
        hitPosition = new Vector2(startPosition.x, -3.5f);
        targetPosition = new Vector2(startPosition.x, hitPosition.y - (startPosition.y - hitPosition.y)); // Distance from hit position to destroy position should be the same as distance from start position to hit position so note is perfectly overlapping the hit circle in the middle of its interpolation
        destroyHeight = -6;
    }

    // Update is called once per frame
    void Update()
    {
        // Move towards destroy position
        if (isMoving == true)
        {
            float t = (Conductor.instance.GetBeatsShownInAdvance() - (targetBeat - Conductor.instance.GetSongPositionInBeats())) / Conductor.instance.GetBeatsShownInAdvance();
            transform.position = Vector2.Lerp(startPosition, targetPosition, t / 2);
        }
        
        /*
        // Play sound if note has passed hit position
        if ((transform.position.y <= hitPosition.y) && (soundPlayed == false))
        {
            AudioManager.instance.PlaySound("Wood Block");
            soundPlayed = true;
        }
        */

        // If note has passed destroy height, destroy note and increment score miss count
        if (transform.position.y <= destroyHeight)
        {
            if (miss == false)
            {
                Score.instance.IncrementMissCount();
            }
            
            Destroy(gameObject);
        }
    }

    // Move to specified position, stop moving, destroy collider, and start fade
    public void SetAsHit(float snapX, float snapY)
    {
        transform.position = new Vector2(snapX, snapY);
        Destroy(GetComponent<CircleCollider2D>());
        GetComponent<FadeOut>().StartFade(fadeTime);
        isMoving = false;
    }

    // Change color, set miss to true
    public void SetAsMiss()
    {
        miss = true;
        spriteRenderer.color = missColor;
    }

    public void SetTargetBeat(float beat)
    {
        if (beat >= 0)
        {
            targetBeat = beat;
        }
        else
        {
            Debug.LogError("Invalid beat!");
        }
    }

    public float GetTargetBeat()
    {
        return (targetBeat);
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public bool GetIsMiss()
    {
        return (miss);
    }
}
