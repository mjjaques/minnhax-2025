using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    public float arrowSpeed = 5.0f;
    private float length;
    private Vector3 position;
    private Vector3 arrowStartPosition;
    private float targetMinY;
    private float targetMaxY;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        length = spriteRenderer.bounds.size.y;
        

        position = transform.position;
        arrowStartPosition = transform.Find("Arrow").position;
        SpriteRenderer target = transform.Find("Target").GetComponent<SpriteRenderer>();
        targetMinY = target.bounds.min.y;
        targetMaxY = target.bounds.max.y;

        // Put the arrow at the bottom to start
        transform.Find("Arrow").position = new Vector3(arrowStartPosition.x, position.y - length / 2, arrowStartPosition.z);
        arrowStartPosition = transform.Find("Arrow").position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the target position using Mathf.PingPong
        // This creates a value that oscillates between 0 and 'length'
        float pingPongValue = Mathf.PingPong(Time.time * arrowSpeed, length);

        // Determine the new position along the y-axis (keeping x and z the same)
        Vector3 targetPosition = new Vector3(arrowStartPosition.x, arrowStartPosition.y + pingPongValue, arrowStartPosition.z);
        transform.Find("Arrow").position = targetPosition;

        // If user presses space bar, check if in the target zone
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float y = transform.Find("Arrow").position.y;
            if (y > targetMinY && y < targetMaxY)
            {
                Debug.Log("Yay!");
            }
            else
            {
                Debug.Log("Oh no!");
            }
        }
    }
}
