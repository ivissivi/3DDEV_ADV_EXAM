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
    public Color parentColor; // Color for the parent object

    private GameObject currentNote; // Reference to the spawned object
    private Color originalColor; // Store the original color of the parent object

    void Start()
    {
        // Store the original color of the parent object
        Renderer parentRenderer = GetComponent<Renderer>();
        if (parentRenderer != null)
        {
            originalColor = parentRenderer.material.color;
        }
    }

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
        currentNote = Instantiate(notePrefab, transform.position, Quaternion.identity);

        // Adjust the scale of the spawned object by the Inspector value
        currentNote.transform.localScale = notePrefab.transform.localScale * scaleMultiplier;

        // Set the spawned object's position to match the parent's position
        currentNote.transform.position = transform.position;

        StartCoroutine(MoveObject(currentNote));
    }

    IEnumerator MoveObject(GameObject obj)
    {
        Renderer parentRenderer = GetComponent<Renderer>();

        while (obj.transform.position.z > destroyCoordinate)
        {
            obj.transform.Translate(Vector3.back * speed * Time.deltaTime);

            // Calculate distance to the destroy coordinate
            float distance = obj.transform.position.z - destroyCoordinate;

            // Calculate reversed color change based on distance
            if (parentRenderer != null)
            {
                float colorChangeFactor = 1.0f - Mathf.Clamp01(distance / 10f); // Reverse the color change factor
                parentRenderer.material.color = Color.Lerp(originalColor, parentColor, colorChangeFactor);
            }

            yield return null;
        }

        Destroy(obj);

        // Reset the parent's color to the original color when the object is destroyed
        if (parentRenderer != null)
        {
            parentRenderer.material.color = originalColor; // Reset to original color
        }
    }
}
