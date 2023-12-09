using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNote : MonoBehaviour
{
    public GameObject notePrefab; // Assign the prefab for the object to be spawned
    public float speed = 2.0f; // Movement speed of the spawned object
    public float destroyCoordinate = -2.3f; // Z-coordinate to delete the object
    public float scaleMultiplier = 1.0f; // Inspector value to adjust the scale of the prefab
    public KeyCode spawnKey; // Keycode to spawn the object, default is Z

    // Update is called once per frame
    void Update()
    {
        // Check for key press to spawn the object
        if (Input.GetKeyDown(spawnKey))
        {
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        GameObject newNote = Instantiate(notePrefab, transform.position, Quaternion.identity);

        // Adjust the scale of the spawned object by the Inspector value
        newNote.transform.localScale = notePrefab.transform.localScale * scaleMultiplier;

        // Set the spawned object's position to match the parent's position
        newNote.transform.position = transform.position;

        StartCoroutine(MoveObject(newNote));
    }

    IEnumerator MoveObject(GameObject obj)
    {
        while (obj.transform.position.z > destroyCoordinate)
        {
            obj.transform.Translate(Vector3.back * speed * Time.deltaTime);
            yield return null;
        }

        Destroy(obj);
    }
}
