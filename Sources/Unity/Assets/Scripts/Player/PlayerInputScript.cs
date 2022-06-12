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

    private void Awake()
    {
        inputAsset = GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
    }

    private void Start()
    {
        _shipController = GetComponent<ShipController>();
    }

    private void OnEnable()
    {
        player.FindAction("Jump").started += Jump;
        player.FindAction("Use").started += UseBonus;
        player.FindAction("Boost").started += Boost;
        player.FindAction("Boost").canceled += Boost;
        player.FindAction("Shoot").started += Shoot;
        player.FindAction("Pause").started += Pause;
        player.FindAction("Movement").performed += Direction;
        player.FindAction("Movement").canceled += Direction;
        player.FindAction("Movement").started += Direction;
        player.FindAction("Rotate").performed += Rotation;
        player.FindAction("Rotate").canceled += Rotation;
        player.FindAction("Rotate").started += Rotation;

        player.Enable();
    }

    private void OnDisable()
    {
        player.FindAction("Jump").started -= Jump;
        player.FindAction("Use").started -= UseBonus;
        player.FindAction("Boost").started -= Boost;
        player.FindAction("Boost").canceled -= Boost;
        player.FindAction("Shoot").started -= Shoot;
        player.FindAction("Pause").started -= Pause;
        player.FindAction("Movement").performed -= Direction;
        player.FindAction("Movement").canceled -= Direction;
        player.FindAction("Movement").started -= Direction;
        player.FindAction("Rotate").performed -= Rotation;
        player.FindAction("Rotate").canceled -= Rotation;
        player.FindAction("Rotate").started -= Rotation;

        player.Disable();
    }

    private void OnDestroy()
    {
        player.Disable();
    }

    // Action function

    private void Rotation(InputAction.CallbackContext obj)
    {
        float x = obj.ReadValue<float>();
        _shipController.SetYaw(x);
    }

    private void Direction(InputAction.CallbackContext obj)
    {
        Vector2 vec = obj.ReadValue<Vector2>();
        _shipController.Move(vec);
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