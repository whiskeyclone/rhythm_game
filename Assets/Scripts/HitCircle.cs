using UnityEngine;

public class HitCircle : MonoBehaviour
{
    private Color pressDownColor = Color.grey;
    private Color normalColor;
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private SpriteRenderer textSpriteRenderer;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalColor = spriteRenderer.color;
    }

    // Changes color of hit circle and text to pressDownColor
    public void PressDownColor()
    {
        spriteRenderer.color = pressDownColor;
        textSpriteRenderer.color = pressDownColor;
    }

    // Changes color of hit circle and text to normalColor
    public void NormalColor()
    {
        spriteRenderer.color = normalColor;
        textSpriteRenderer.color = normalColor;
    }

    public Vector2 GetRaycastOriginPosition()
    {
        if (raycastOrigin != null)
        {
            return (raycastOrigin.position);
        }
        else
        {
            Debug.LogError("rayCastOrigin not set!");
            return (Vector2.zero);
        }
    }

    /*
    [SerializeField] private KeyCode key;
    [SerializeField] private Transform raycastOrigin;

    private float hitConfirmDistance = 1f; // Max distance from the Raycast Origin to the note circle that will count as a hit when the player hits the key
    private bool hittingHoldNote = false;
    private HoldNoteCircle hitHoldNote;

    // Update is called once per frame
    void Update()
    {
        // If key is pressed
        if (Input.GetKeyDown(key))
        {
            // Change hit circle color to grey when key is pressed
            gameObject.GetComponent<SpriteRenderer>().color = Color.grey;

            // Fire raycast and get hit results
            RaycastHit2D raycastResults = Physics2D.Raycast(raycastOrigin.position, Vector2.up);

            // If raycast hit a note circle
            if (raycastResults.collider != null)
            {
                GameObject hitObject = raycastResults.collider.gameObject;

                if (hitObject.tag == "Note Circle")
                {
                    float distance = Mathf.Abs(hitObject.transform.position.y - transform.position.y); // Get position between hit circle and detected note circle

                    // If distance to hit note circle is less than or equal to hitConfirmDistance, set note circle as hit
                    if (distance <= hitConfirmDistance)
                    {
                        hitObject.GetComponent<NoteCircle>().SetAsHit(transform.position.x, transform.position.y);
                    }
                    else
                    {
                        hitObject.GetComponent<NoteCircle>().SetAsMiss();
                    }
                }
                else if (hitObject.tag == "Hold Note Circle")
                {
                    // If distance to hit note circle is less than or equal to hitConfirmDistance, set note circle as hit
                    if (raycastResults.distance <= hitConfirmDistance)
                    {
                        hitObject.GetComponent<HoldNoteCircle>().SetAsHit(transform.position.x, transform.position.y);
                        hittingHoldNote = true;
                        hitHoldNote = hitObject.GetComponent<HoldNoteCircle>();
                    }
                    else
                    {
                        // Set hold note as miss
                        hitObject.GetComponent<HoldNoteCircle>().SetAsMiss();
                    }
                }
            }
        }

        // If key is released
        if (Input.GetKeyUp(key))
        {
            // Change hit circle color to black when key is released
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        }

        // If hitting a hold note
        if (hittingHoldNote == true)
        {
            if (hitHoldNote.GetTailLength() == 0) // If hold note tail has finished shrinking
            {
                // Stop hitting hold note
                hittingHoldNote = false;
            }
            else if (Input.GetKeyUp(key)) // Else if key is released
            {
                // Stop hitting hold note and set hold note as miss
                hittingHoldNote = false;
                hitHoldNote.GetComponent<HoldNoteCircle>().SetAsMiss();
            } 
        }
    }
    */
}
