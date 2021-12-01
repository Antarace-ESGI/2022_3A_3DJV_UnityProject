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

    private bool _moveRight;
    private bool _moveLeft;
    private bool _moveForward;
    private bool _moveBackward;
    
    void Update()
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
    
    //Prendre en compte la masse (Addforce)= Impulse
    //Ignorer la masse = VelocityChange
    
    //Ignorer la masse = Accélaration (Dans une vitesse)

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_moveLeft)
        {
            Vector3 force = playerTransform.right * -turnSpeed;
            Vector3 position = transform.position;
            position.z += .5f;
            
            playerRigidbody.AddForceAtPosition(force, position, ForceMode.Force);
        }
        if (_moveRight)
        {
            Vector3 force = playerTransform.right * turnSpeed;
            Vector3 position = transform.position;
            position.z += .5f;
            
            playerRigidbody.AddForceAtPosition(force, position, ForceMode.Force);
        }
        if (_moveForward)
        {
            playerRigidbody.AddForce(playerTransform.forward * runSpeed, ForceMode.Force);
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
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            
            playerRigidbody.AddForce(Vector3.up * runSpeed, ForceMode.Force);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
        }
        
        // Try to make the body to stand up
        var rot = Quaternion.FromToRotation(transform.up, Vector3.up);
        playerRigidbody.AddTorque(new Vector3(rot.x, rot.y, rot.z) * 10);
    }
}
