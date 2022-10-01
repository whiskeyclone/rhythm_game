using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score instance;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Transform playAgainButton;
    int totalNoteCount = 0;
    int missCount = 0;

    // Start is called before the first frame update
    void Awake()
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
    }

    // Calculate and show score and rating, show and enable play again button
    public void DisplayScore()
    {
        // Calculate score
        int hitNoteCount = totalNoteCount - missCount;
        float score = ((float)hitNoteCount / totalNoteCount) * 100;

        // Calculate rating
        char rating = ' ';

        if (score < 65)
        {
            rating = 'F';
        }
        else if (score >= 65 && score < 70)
        {
            rating = 'D';
        }
        else if (score >= 70 && score < 80)
        {
            rating = 'C';
        }
        else if (score >= 80 && score < 90)
        {
            rating = 'B';
        }
        else if (score >= 90 && score < 100)
        {
            rating = 'A';
        }
        else if (score == 100)
        {
            rating = 'S';
        }

        // Show score and rating
        text.text = "Score: " + hitNoteCount + "/" + totalNoteCount + "\n" + "Rating: " + rating;

        // Show and enable play again button
        playAgainButton.localScale = new Vector3(1, 1, 1);
        playAgainButton.GetComponent<Button>().interactable = true;

        // Show mouse cursor
        Cursor.visible = true;
    }

    public void SetTotalNoteCount(int x)
    {
        totalNoteCount = x;
    }

    public void IncrementMissCount()
    {
        missCount++;
    }
}
