using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onPress : MonoBehaviour
{
    public KeyCode keyToPress;
    public AudioClip sound;
    public float moveAmount = 0.03f; // Amount to move on the Y axis
    public Color pressedColor = Color.green; // Color when the key is pressed

    private AudioSource audioSource;
    private bool isKeyPressed = false;
    private Color originalColor;
    private Vector3 originalPosition;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Store original color and position
        originalColor = GetComponent<Renderer>().material.color;
        originalPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            isKeyPressed = true;
            PlaySound();
            // Change color to pressedColor
            GetComponent<Renderer>().material.color = pressedColor;
        }

        if (Input.GetKeyUp(keyToPress))
        {
            isKeyPressed = false;
            // Revert color to original
            GetComponent<Renderer>().material.color = originalColor;
        }

        // Move the object based on key held status
        if (isKeyPressed)
        {
            // Move the object down on the Y axis
            transform.position += Vector3.down * moveAmount;
        }
        else
        {
            // Move the object back to its original position
            transform.position = Vector3.Lerp(transform.position, originalPosition, Time.deltaTime * 5f);
        }
    }

    void PlaySound()
    {
        if (sound)
        {
            audioSource.PlayOneShot(sound);
            Debug.Log("Sound Played");
        }
        else
        {
            Debug.LogWarning("AudioClip is not assigned.");
        }
    }
}
