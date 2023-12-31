using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidiReader : MonoBehaviour
{
    public TextAsset noteData; // Reference to your note_data.txt file
    public float timingMultiplier = 1.0f; // Adjustable timing multiplier in the Inspector

    void Start()
    {
        Dictionary<int, string> numberToKeyMap = new Dictionary<int, string>
        {
            // Add your MIDI note mapping here
            { 48, "1wC" },
            { 49, "2bCs" },
            { 50, "3wD" },
            { 51, "4bDs" },
            { 52, "5wE" },
            { 53, "6wF" },
            { 54, "7bFs" },
            { 55, "8wG" },
            { 56, "9bGs" },
            { 57, "10wA" },
            { 58, "11bAs" },
            { 59, "12wB" },
            { 60, "13wC" },
            { 61, "14bCs" },
            { 62, "15wD" },
            { 63, "16bDs" },
            { 64, "17wE" },
            { 65, "18wF" },
            { 66, "19bFs" },
            { 67, "20wG" },
            { 68, "21bGs" },
            { 69, "22wA" },
            { 70, "23bAs" },
            { 71, "24wB" },
            { 72, "25wC" },
            { 73, "26bCs" },
            { 74, "27wD" },
            { 75, "28bDs" },
            { 76, "29wE" },
            { 77, "30wF" },
            { 78, "31wFs" },
            { 79, "32wG" },
            { 80, "33bGs" },
            { 81, "34wA" },
            { 82, "35bAs" },
            { 83, "36wB" },
            { 84, "37wC" },
        };

        StartCoroutine(SpawnNotes(numberToKeyMap));
    }

    IEnumerator SpawnNotes(Dictionary<int, string> noteMap)
    {
        if (noteData != null)
        {
            string[] lines = noteData.text.Split('\n');
            float startTime = Time.time; // Record the start time

            foreach (string line in lines)
            {
                string[] data = line.Trim().Split(',');

                if (data.Length >= 2)
                {
                    int midiNote;
                    if (int.TryParse(data[0], out midiNote))
                    {
                        string keyName;
                        if (noteMap.TryGetValue(midiNote, out keyName))
                        {
                            GameObject obj = GameObject.Find(keyName);
                            if (obj != null)
                            {
                                SpawnNote spawnNoteScript = obj.GetComponent<SpawnNote>();

                                if (spawnNoteScript != null)
                                {
                                    float timing;
                                    if (float.TryParse(data[1], out timing))
                                    {
                                        // Adjust timing based on timingMultiplier
                                        timing *= timingMultiplier;

                                        // Calculate the time difference from the start time
                                        float elapsedTime = Time.time - startTime;

                                        // Wait for the specified time to spawn the note
                                        yield return new WaitForSeconds(timing - elapsedTime);

                                        spawnNoteScript.SpawnObject(); // Spawn the object
                                        Debug.Log("Spawned object for MIDI note " + midiNote + " at time " + Time.time);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
