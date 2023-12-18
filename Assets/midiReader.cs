using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class midiReader : MonoBehaviour
{
    void Start()
    {
        // Find all objects with the name "1wC" in the scene
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Tile"); // Replace "YourTag" with the tag of your objects

        foreach (GameObject obj in objects)
        {
            if (obj.name == "1wC")
            {
                // Check if the object has the SpawnNote script attached
                SpawnNote spawnNoteScript = obj.GetComponent<SpawnNote>();

                if (spawnNoteScript != null)
                {
                    spawnNoteScript.SpawnObject();
                    Debug.Log("SpawnNote script found and object spawned: " + obj.name);
                }
                else
                {
                    Debug.LogError("SpawnNote script not found on object: " + obj.name);
                }
            }
        }
    }
}
