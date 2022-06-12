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
    [SerializeField] private GameObject _uiHUD;

    private ShipController _shipController;

    private float _yawDiff;

    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction move;
    private InputAction rotate;



    private void Awake(){

      inputAsset = this.GetComponent<PlayerInput>().actions;
      player = inputAsset.FindActionMap("Player");
    }

    private void Start()
    {
        _shipController = GetComponent<ShipController>();

        // Load input map

                                                              //_controls ??= new PlayerController();

        // Input callback
        // Use the bonus


        // Shoot



        // Boost


    ///    _controls.Player.Boost.canceled += ctx => UnBoost();

    }

    // Enable or disable the input


    private void OnEnable(){

      player.FindAction("Jump").started += Jump;
      player.FindAction("Use").started += UseBonus;
      player.FindAction("Boost").started += Boost;
      player.FindAction("Shoot").started += Shoot;
      player.FindAction("Pause").started += Pause;

      move = player.FindAction("Move");
      rotate = player.FindAction("Rotate");

      player.Enable();
    }

    private void OnDisable()
    {

      player.FindAction("Jump").started -= Jump;
      player.FindAction("Use").started -= UseBonus;
      player.FindAction("Boost").started -= Boost;
      player.FindAction("Shoot").started -= Shoot;
      player.FindAction("Pause").started -= Pause;

      player.Disable();
    }

    private void OnDestroy()
    {
        player.Disable();
    }

    // Action function

    private void Rotation(float x)
    {
        _shipController.SetYaw(x);
    }

    public void Direction(Vector2 dir)
    {
        _shipController.Move(dir);
    }

    public void UseBonus(InputAction.CallbackContext obj)
    {
        gameObject.GetComponent<PlayerStatsScript>().unableBonusUse();
    }

    public void Shoot(InputAction.CallbackContext obj)
    {
        _blaster.GetComponent<PlayerBlaster>().Shoot();
    }

    public void Boost(InputAction.CallbackContext obj)
    {
        _shipController.ActiveBoost(true);
    }

    public void UnBoost(InputAction.CallbackContext obj)
    {
        _shipController.ActiveBoost(false);
    }

    public void Jump(InputAction.CallbackContext obj)
    {

    }

    private void Pause(InputAction.CallbackContext obj)
    {
        GameObject pause = GameObject.FindGameObjectWithTag("Pause");
        if (pause)
        {
            Canvas pauseCanvas = pause.GetComponent<Canvas>();
            if (pause.GetComponent<Canvas>().enabled)
            {
                _uiHUD.SetActive(true);
                Time.timeScale = 1.0f;
                pauseCanvas.enabled = false;
            }
            else
            {
                _uiHUD.SetActive(false);
                Time.timeScale = 0.0f;
                pauseCanvas.enabled = true;
            }
        }
    }
}
