using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    private PlayerController _controls;
    
    private void Awake()
    {
        _controls = keybindingScript.controller;
        
        // Input callback
        
        _controls.Player.Use.performed += ctx => UseBonus();
        _controls.Player.Shoot.performed += ctx => Shoot();
        _controls.Player.Boost.performed += ctx => Boost();
        _controls.Player.Pause.performed += ctx => Pause();
        _controls.Player.Jump.performed += ctx => Jump();
        
        _controls.Player.Enable();
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
