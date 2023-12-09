using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressKey : MonoBehaviour
{
    public GameObject keyA;
    public GameObject keyB;
    public GameObject keyC;

    public AudioClip soundA;
    public AudioClip soundB;
    public AudioClip soundC;

    private Dictionary<KeyCode, GameObject> keysPressed = new Dictionary<KeyCode, GameObject>();

    void Update()
    {
        CheckKeyPress(KeyCode.A, keyA, soundA);
        CheckKeyPress(KeyCode.B, keyB, soundB);
        CheckKeyPress(KeyCode.C, keyC, soundC);

        CheckKeyRelease(KeyCode.A);
        CheckKeyRelease(KeyCode.B);
        CheckKeyRelease(KeyCode.C);
    }

    void CheckKeyPress(KeyCode keyCode, GameObject keyObject, AudioClip sound)
    {
        if (Input.GetKeyDown(keyCode) && !keysPressed.ContainsKey(keyCode))
        {
            keysPressed[keyCode] = keyObject;
            StartCoroutine(TriggerKeyPress(keyObject, sound));
        }
    }

    void CheckKeyRelease(KeyCode keyCode)
    {
        if (Input.GetKeyUp(keyCode) && keysPressed.ContainsKey(keyCode))
        {
            GameObject keyObject = keysPressed[keyCode];
            keysPressed.Remove(keyCode);
            StopCoroutine(TriggerKeyPress(keyObject, null));
        }
    }

    IEnumerator TriggerKeyPress(GameObject key, AudioClip sound)
    {

        Vector3 originalScale = key.transform.localScale;
        Color originalColor = key.GetComponent<Renderer>().material.color;

        while (true)
        {
            key.transform.localScale *= 0.9f;
            key.GetComponent<Renderer>().material.color = Color.gray;

            if (sound != null)
            {
                AudioSource.PlayClipAtPoint(sound, key.transform.position);
            }

            yield return null;

            if (!keysPressed.ContainsValue(key))
            {
                break;
            }
        }

        key.transform.localScale = originalScale;
        key.GetComponent<Renderer>().material.color = originalColor;
    }
}
