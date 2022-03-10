using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    private PlayerController _controls;
    
    private void Awake()
    {
        _controls = new PlayerController();
        
        // Input callback
        
        _controls.Player.Use.performed += ctx => UseBonus();
        _controls.Player.Shoot.performed += ctx => Shoot();
        _controls.Player.Boost.performed += ctx => Boost();
        _controls.Player.Pause.performed += ctx => Pause();
        _controls.Player.Jump.performed += ctx => Jump();
    }
    
    // Main function

    private void OnEnable()
    {
        _controls.Player.Enable();
        Debug.Log("Loaded");
    }
    
    private void OnDisable()
    {
        _controls.Player.Disable();
    }
    
    // Action function

    private void UseBonus()
    {
        Debug.Log("Pressed");
    }

    private void Shoot()
    {
        Debug.Log("Shoot");
    }

    private void Boost()
    {
        Debug.Log("Boost");
    }

    private void Jump()
    {
        Debug.Log("Jump");
    }

    private void Pause()
    {
        Debug.Log("Pause");
    }
}
