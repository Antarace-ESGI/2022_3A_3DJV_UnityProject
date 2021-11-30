using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float runSpeed;
    public Rigidbody playerRigidbody;

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
    
    //Prendre en compte la masse (Addforce)= Impulse
    //Ignorer la masse = VelocityChange
    
    //Ignorer la masse = Accélaration (Dans une vitesse)

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveLeft)
        {
            float axis = Input.GetAxis("Horizontal");
            playerRigidbody.AddTorque(Vector3.left * axis * 2000);

        }
        if (moveRight)
        {
            playerRigidbody.AddTorque(0.0f,-5.0f,0.0f);
            //playerRigidbody.AddTorque(Vector3.right * runSpeed,ForceMode.Force);
        }
        if (moveForward)
        {
            playerRigidbody.AddForce(Vector3.forward * runSpeed, ForceMode.Force);
        }
        if (moveBackward)
        {
            playerRigidbody.AddForce(Vector3.back * runSpeed, ForceMode.Force);
        }
    }
}
