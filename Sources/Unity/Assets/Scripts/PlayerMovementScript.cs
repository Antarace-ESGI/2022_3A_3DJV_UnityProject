using System;
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

    private void controllerMovement()
    {
        if (Input.GetAxisRaw("Vertical") <= 1 && Input.GetAxisRaw("Vertical") > 0)
        {
            moveForward = true;
        }
        if (Input.GetAxisRaw("Vertical") >= -1 && Input.GetAxisRaw("Vertical") < 0)
        {
            moveBackward = true;
        }
        if (Input.GetAxisRaw("Vertical") == 0)
        {
            moveForward = false;
            moveBackward = false;
        }
        if (Input.GetAxisRaw("Horizontal") >= -1 && Input.GetAxisRaw("Horizontal") < 0)
        {
            moveLeft = true;
        }
        if (Input.GetAxisRaw("Horizontal") <= 1 && Input.GetAxisRaw("Horizontal") > 0)
        {
            moveRight = true;
        }
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            moveLeft = false;
            moveRight = false;
        }
    }

    private void keyboardMovement()
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
    
    void Update()
    {

        if (Input.GetJoystickNames().Length > 0)
        {
            controllerMovement();
        }
        else
        {
            keyboardMovement();
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
