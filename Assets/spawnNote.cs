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
    public KeyCode destructionKey = KeyCode.Z;

    private Material parentMaterial;
    private Color originalColor;
    private List<GameObject> activeNotes = new List<GameObject>();
    private int spawnedCount = 0;

    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            parentMaterial = rend.material;
            originalColor = parentMaterial.color;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(destructionKey))
        {
            DestroyClosestObject();
        }

        // Check if there are no active notes and the color has changed
        if (activeNotes.Count == 0 && parentMaterial.color != originalColor)
        {
            ResetParentColor();
        }
    }

    void DestroyClosestObject()
    {
        GameObject closestNote = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject note in activeNotes)
        {
            float distance = Mathf.Abs(note.transform.position.z - destroyCoordinate);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestNote = note;
            }
        }

        if (closestNote != null)
        {
            Debug.Log($"Destroying closest object: {closestNote.name}");

            activeNotes.Remove(closestNote);
            Destroy(closestNote);

            // No need to check for parentMaterial.color != originalColor here
            // Only reset color if there are no more active notes
            if (activeNotes.Count == 0)
            {
                ResetParentColor();
            }
        }
        else
        {
            Debug.Log("No object found to destroy.");
        }
    }

    public void SpawnObject()
    {
        GameObject currentNote = Instantiate(notePrefab, transform.position, Quaternion.identity);
        currentNote.transform.localScale = notePrefab.transform.localScale * scaleMultiplier;
        currentNote.transform.position = transform.position;

        spawnedCount++;
        currentNote.name = $"{gameObject.name}_{spawnedCount}";

        activeNotes.Add(currentNote);
        StartCoroutine(MoveObject(currentNote));
    }

    IEnumerator MoveObject(GameObject obj)
    {
        while (obj != null && obj.transform.position.z > destroyCoordinate)
        {
            obj.transform.Translate(Vector3.back * speed * Time.deltaTime);

            float distance = obj.transform.position.z - destroyCoordinate;

            if (parentMaterial != null)
            {
                float colorChangeFactor = 1.0f - Mathf.Clamp01(distance / 10f);
                parentMaterial.color = Color.Lerp(originalColor, parentColor, colorChangeFactor);
            }

            yield return null;
        }

        if (obj != null)
        {
            activeNotes.Remove(obj);
            Destroy(obj);

            // Reset color only if no active notes remain
            if (activeNotes.Count == 0)
            {
                ResetParentColor();
            }
        }
    }

    void ResetParentColor()
    {
        parentMaterial.color = originalColor;
    }
}
