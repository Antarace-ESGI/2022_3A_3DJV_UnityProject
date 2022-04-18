using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    private PlayerController _controls;
    
    // Accessor

    [SerializeField] private GameObject _blaster;
    
    private void Start()
    {
        
        // Load input map
        
        _controls ??= new PlayerController();
        
        // Input callback
        // Use the bonus
        _controls.Player.Use.performed += ctx => UseBonus();
        
        // Shoot
        
        _controls.Player.Shoot.performed += ctx => Shoot();
        
        // Boost
        
        _controls.Player.Boost.performed += ctx => Boost();
        _controls.Player.Boost.canceled += ctx => UnBoost();
        
        // Enabling/Disabling pause
        
        _controls.Player.Pause.performed += ctx => Pause();
        
        // Jump
        
        _controls.Player.Jump.performed += ctx => Jump();
        
        // Movement
        
        _controls.Player.Movement.performed += ctx => Direction(ctx.ReadValue<Vector2>());
        _controls.Player.Movement.canceled += ctx => Direction(Vector2.zero);
        
        _controls.Player.Enable();
    }
    
    // Enable or disable the input 
    
    private void OnDisable()
    {
        _controls.Player.Disable();
    }

    private void OnDestroy()
    {
        _controls.Player.Disable();
    }

    // Action function

    private void Direction(Vector2 dir)
    {
        Debug.Log(dir);
        gameObject.GetComponent<ShipController>().Move(dir);
    }
    
    private void UseBonus()
    {
        gameObject.GetComponent<PlayerStatsScript>().unableBonusUse();
    }

    private void Shoot()
    {
        _blaster.GetComponent<PlayerBlaster>().Shoot();
    }

    private void Boost()
    {
        gameObject.GetComponent<ShipController>().ActiveBoost(true);
    }

    private void UnBoost()
    {
        gameObject.GetComponent<ShipController>().ActiveBoost(false);
    }
    
    private void Jump()
    {
        
    }

    private void Pause()
    {
        
    }
}
