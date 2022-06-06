using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    private PlayerController _controls;

    // Accessor
    [SerializeField] private GameObject _blaster;
    [SerializeField] private GameObject _uiHUD;

    private ShipController _shipController;
    private Canvas _pauseMenu;
    
    private float _yawDiff;

    private void Start()
    {
        _shipController = GetComponent<ShipController>();

        // Search for the pause canvas
        
        GameObject garbage = GameObject.FindGameObjectWithTag("Pause");
        if(garbage)
            _pauseMenu = garbage.GetComponent<Canvas>();
        Debug.Log(_pauseMenu);
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
        if(_controls != null)
            _controls.Player.Disable();
    }

    private void OnDestroy()
    {
        if(_controls != null)
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
        if (_pauseMenu)
        {
            if (_pauseMenu.enabled)
            {
                _controls.Player.Shoot.Enable();
                Cursor.lockState = CursorLockMode.Confined;
                _uiHUD.SetActive(true);
                Time.timeScale = 1.0f;
                _pauseMenu.enabled = false;
            }
            else
            {
                _controls.Player.Shoot.Disable();
                Cursor.lockState = CursorLockMode.None;
                _uiHUD.SetActive(false);
                Time.timeScale = 0.0f;
                _pauseMenu.enabled = true;
            }
        }
    }
}
