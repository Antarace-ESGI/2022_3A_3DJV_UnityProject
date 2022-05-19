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

    private void Start()
    {
        _shipController = GetComponent<ShipController>();

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
        
        // Rotation

        _controls.Player.Rotate.performed += ctx => Rotation(ctx.ReadValue<float>());
        _controls.Player.Rotate.canceled += ctx => Rotation(0f);

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

    private void Rotation(float x)
    {
        _shipController.SetYaw(x);
    }

    public void Direction(Vector2 dir)
    {
        _shipController.Move(dir);
    }

    public void UseBonus()
    {
        gameObject.GetComponent<PlayerStatsScript>().unableBonusUse();
    }

    public void Shoot()
    {
        _blaster.GetComponent<PlayerBlaster>().Shoot();
    }

    public void Boost()
    {
        _shipController.ActiveBoost(true);
    }

    public void UnBoost()
    {
        _shipController.ActiveBoost(false);
    }

    public void Jump()
    {

    }

    private void Pause()
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
