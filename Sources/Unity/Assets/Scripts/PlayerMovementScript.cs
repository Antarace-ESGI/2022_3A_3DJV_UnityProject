using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float runSpeed = 20;
    public float turnSpeed = 1;
    public Transform playerTransform;
    public Rigidbody playerRigidbody;
    public float floatDistance = 2;

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

    private bool _moveRight;
    private bool _moveLeft;
    private bool _moveForward;
    private bool _moveBackward;
    
    private void keyboardMovement()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _moveLeft = true;
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            _moveLeft = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _moveRight = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            _moveRight = false;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _moveForward = true;
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            _moveForward = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _moveBackward = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            _moveBackward = false;
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
        if (_moveLeft)
        {
            transform.Rotate(-Vector3.up, Time.fixedDeltaTime * rotationSpeed);
        }
        if (_moveForward)
        {
            transform.Rotate(Vector3.up, Time.fixedDeltaTime * rotationSpeed);
        }
        if (_moveBackward)
        {
            playerRigidbody.AddForce(playerTransform.forward * -runSpeed, ForceMode.Force);
        }
        
        // Make the body float above the ground
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, floatDistance, layerMask))
        {
            playerRigidbody.AddForce(transform.forward * speed, ForceMode.Force);
        }
        else
        {
            playerRigidbody.AddForce(-transform.forward * speed, ForceMode.Force);
        }
        
        // Try to make the body to stand up
        var rot = Quaternion.FromToRotation(transform.up, Vector3.up);
        playerRigidbody.AddTorque(new Vector3(rot.x, rot.y, rot.z) * 10);
    }
}
