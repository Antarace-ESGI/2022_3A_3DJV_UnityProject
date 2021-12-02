using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public Rigidbody playerRigidbody;

    public float rotationSpeed = 20f;
    public float speed = 2f;

    private bool moveRight;
    private bool moveLeft;
    private bool moveForward;
    private bool moveBackward;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            moveLeft = true;
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            moveLeft = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            moveRight = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            moveRight = false;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            moveForward = true;
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            moveForward = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            moveBackward = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            moveBackward = false;
        }
    }
    void FixedUpdate()
    {
        if (moveLeft)
        {
            transform.Rotate(-Vector3.up, Time.fixedDeltaTime * rotationSpeed);
        }
        if (moveRight)
        {
            transform.Rotate(Vector3.up, Time.fixedDeltaTime * rotationSpeed);
        }
        if (moveForward)
        {
            playerRigidbody.AddForce(transform.forward * speed, ForceMode.Force);
        }
        if (moveBackward)
        {
            playerRigidbody.AddForce(-transform.forward * speed, ForceMode.Force);
        }
    }
}
