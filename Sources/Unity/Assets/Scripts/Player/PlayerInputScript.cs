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

    public RectTransform crosshair;
    public float maxRadius = 128f;

    private Vector3 _centerScreen;
    private float _qtrScreenW;

    private bool test = false;

    private void Start()
    {
        _shipController = GetComponent<ShipController>();

        _centerScreen = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        _qtrScreenW = Screen.width * 0.25f;

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

    private void Update()
    {
        GetYawValue();
    }

    private void GetYawValue()
    {
        float x = Input.GetAxis("Mouse X");

        _yawDiff = Mathf.Clamp(crosshair.position.x - _centerScreen.x + x, -maxRadius, maxRadius);
        crosshair.position = new Vector3(_yawDiff + _centerScreen.x, _centerScreen.y, _centerScreen.z);

        _shipController.SetYaw(_yawDiff / _qtrScreenW);
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

    public void Direction(Vector2 dir)
    {
        _shipController.Move(dir);
    }

    public void UseBonus()
    {
        gameObject.GetComponent<PlayerStatsScript>().unableBonusUse();
    }

    public void Shoot(/*InputAction.CallbackContext context*/)
    {
        _blaster.GetComponent<PlayerBlaster>().Shoot();
        // test = context.ReadValue<bool>();
        // test = context.action.triggered;
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
