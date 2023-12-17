using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNote : MonoBehaviour
{
    public GameObject notePrefab;
    public float speed = 2.0f;
    public float destroyCoordinate = -1.3f;
    public float scaleMultiplier = 1.0f;
    public Color parentColor;

    private Color originalColor;
    private Renderer parentRenderer;
    private int activeObjects = 0;
    private int spawnedCount = 0; // Track the count of spawned objects for this parent

    void Start()
    {
        parentRenderer = GetComponent<Renderer>();
        if (parentRenderer != null)
        {
            originalColor = parentRenderer.material.color;
        }

        StartCoroutine(SpawnObjectWithDelay());
    }

    IEnumerator SpawnObjectWithDelay()
    {
        while (true)
        {
            SpawnObject();
            yield return new WaitForSeconds(3.0f);
        }
    }

    void SpawnObject()
    {
        GameObject currentNote = Instantiate(notePrefab, transform.position, Quaternion.identity);
        currentNote.transform.localScale = notePrefab.transform.localScale * scaleMultiplier;
        currentNote.transform.position = transform.position;

        // Assign the name of the parent object to the spawned object
        spawnedCount++; // Increment the count for this spawned object
        currentNote.name = $"{gameObject.name}_{spawnedCount}"; // Append the count to the name

        activeObjects++; // Increment count of active objects
        StartCoroutine(MoveObject(currentNote));
    }

    IEnumerator MoveObject(GameObject obj)
    {
        while (obj.transform.position.z > destroyCoordinate)
        {
            obj.transform.Translate(Vector3.back * speed * Time.deltaTime);

            float distance = obj.transform.position.z - destroyCoordinate;

            if (parentRenderer != null)
            {
                float colorChangeFactor = 1.0f - Mathf.Clamp01(distance / 10f);
                parentRenderer.material.color = Color.Lerp(originalColor, parentColor, colorChangeFactor);
            }

            yield return null;
        }

        Destroy(obj);
        activeObjects--; // Decrement count of active objects
        if (activeObjects == 0)
        {
            ResetParentColor(); // Reset parent's color if no active objects remain
        }
    }

    void ResetParentColor()
    {
        if (parentRenderer != null)
        {
            parentRenderer.material.color = originalColor;
        }
    }
}
