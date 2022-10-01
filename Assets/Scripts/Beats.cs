// This class reads a text file containing a beatmap and stores the beat information in an array

using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class Beats : MonoBehaviour
{
    public static Beats instance;
    [SerializeField] private TextAsset beatMap;
    private List<float>[] beats = new List<float>[4];
    private string beatMapText;
    private float tickLength = 0.25f; // How many beats there are between each character read in
    private char[] keys = new char[4]; // characters in text file that represent beats

    // Start is called before the first frame update
    void Start()
    {
        // Initialize beats array
        for (int i = 0; i < beats.Length; i++)
        {
            beats[i] = new List<float>();
        }

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

        // Set keys
        keys[0] = 'v';
        keys[1] = 'b';
        keys[2] = 'n';
        keys[3] = 'm';

        // Load beatmap
        LoadBeatmap();
    }

    // stores beat info from text file into beats array
    void LoadBeatmap()
    {
        // Get beat map text
        if (beatMap != null)
        {
            beatMapText = beatMap.text;
        }
        else
        {
            Debug.LogError("No beatmap found!");
        }

        // Remove '|'s and whitespace from beat map text
        beatMapText = beatMapText.Replace("|", string.Empty).Trim();
        beatMapText = String.Concat(beatMapText.Where(c => !Char.IsWhiteSpace(c)));

        // Read beat map text and store beat info in beat array, then send total note count to score object
        int count = 0;

        for (int i = 0; i < beatMapText.Length; i++) // Loop through beat map text
        {
            for (int k = 0; k < keys.Length; k++) // Check character for match with a key
            {
                if (beatMapText[i] == keys[k])
                {
                    // Add beat to beat array
                    beats[k].Add(i * tickLength);
                    count++;
                    break;
                }
            }
        }

        Score.instance.SetTotalNoteCount(count);

        /*
        // Print beat array to console
        for (int i = 0; i < 4; i++)
        {
            string testString = "";

            for (int j = 0; j < beats[i].Count; j++)
            {
                if (j == 0)
                {
                    testString = beats[i][j].ToString();
                }
                else
                {
                    testString = testString + ", " + beats[i][j];
                }
            }

            Debug.Log(testString);
        }
        */
    }

    // Returns count of beats in specified beatline
    public float GetBeatLineLength(int beatLine)
    {
        if ((beatLine >= 0) && (beatLine < beats.Length))
        {
            return (beats[beatLine].Count);
        }
        else
        {
            Debug.LogError("Invalid beat line!");
            return (0);
        }        
    }

    // Removes first beat of specified beat line
    public void RemoveBeat(int beatLine)
    {
        if ((beatLine >= 0) && (beatLine < beats.Length))
        {
            if (beats[beatLine].Count > 0)
            {
                beats[beatLine].RemoveAt(0);
            }
            else
            {
                Debug.LogError("Empty beat line!");
            }
        }
        else
        {
            Debug.LogError("Invalid beat line!");
        }
    }

    // Retrieves the first beat of the given beat line
    public float GetBeat(int beatLine)
    {
        if ((beatLine >= 0) && (beatLine < beats.Length))
        {
            if (beats[beatLine].Count > 0)
            {
                return (beats[beatLine][0]);
            }
            else
            {
                Debug.LogError("Empty beat line!");
                return (0);
            }
        }
        else
        {
            Debug.LogError("Invalid beat line!");
            return (0);
        }   
    }
}
