using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float moveSpeed = 2.0f;  // Adjust the movement speed.
    public float distance = 5.0f;  // Adjust the distance the cube moves.

    private Vector3 initialPosition;
    private float direction = 1;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // Move the cube left and right based on the direction and distance.
        Vector3 newPosition = transform.position;
        newPosition.x += direction * moveSpeed * Time.deltaTime;

        // If the cube has moved the specified distance, reverse its direction.
        if (Mathf.Abs(newPosition.x - initialPosition.x) >= distance)
        {
            direction *= -1;
        }

        transform.position = newPosition;
    }
}
