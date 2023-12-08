using UnityEngine;

public class pressKey : MonoBehaviour
{
    public KeyCode keyToHold = KeyCode.Space; // Change this to the desired key

    public float moveDistance = 0.00001f; // Adjust this value for the distance you want to move the object

    private bool isKeyPressed = false;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Store the initial position of the object
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the specified key is being held down
        if (Input.GetKey(keyToHold))
        {
            isKeyPressed = true;
        }
        else
        {
            isKeyPressed = false;

            // If the key is released, move the object back to its initial position
            transform.position = initialPosition;
        }

        // If the key is held down, move the object down by a bit along the Y-axis
        if (isKeyPressed)
        {
            // Move the object down by 'moveDistance' along the Y-axis
            transform.Translate(Vector3.down * moveDistance, Space.World);
        }
    }
}
