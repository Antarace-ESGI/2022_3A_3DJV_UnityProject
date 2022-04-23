using UnityEngine;

[RequireComponent(typeof(ShipController))]
public class KeyboardController : MonoBehaviour
{
    private Vector3 _centerScreen;
    private float _qtrScreenH;
    private float _qtrScreenW;
    
    private float _yawDiff;

    // Crosshair
    public float mouseDeadZone = 0.05f;
    public RectTransform crosshair;
    public float maxRadius = 128f;

    // Jump mechanic
    private bool _isStuck;

    private ShipController _shipController;

    void Start()
    {
        _centerScreen = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        _qtrScreenH = Screen.height * 0.25f;
        _qtrScreenW = Screen.width * 0.25f;
        _shipController = GetComponent<ShipController>();
    }

    void Update()
    {
        InputUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Environment") && collision.GetContact(0).normal == Vector3.up)
        {
            _isStuck = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            _isStuck = false;
        }
    }

    private float GetYawValue()
    {
        float x = Input.GetAxis("Mouse X");

        _yawDiff = Mathf.Clamp(crosshair.position.x - _centerScreen.x + x, -maxRadius, maxRadius);
        crosshair.position = new Vector3(_yawDiff + _centerScreen.x, _centerScreen.y, _centerScreen.z);

        return (_yawDiff / _qtrScreenW);
    }

    private void InputUpdate()
    {
        _shipController.yaw = GetYawValue();

        _shipController.thrust.x = Input.GetAxis("Horizontal");
        _shipController.thrust.y = _shipController.GetThrustY();
        _shipController.thrust.z = Input.GetAxis("Vertical"); // Z is forward/Back

        if (_isStuck)
        {
            _shipController.thrust.y += 10f;
        }

        // Set Flags
        _shipController.adjustYaw = Mathf.Abs(_shipController.yaw) > mouseDeadZone;
        _shipController.adjustThrustX = Mathf.Abs(_shipController.thrust.x) > 0.1f;
        _shipController.adjustThrustY = _shipController.thrust.y != 0;
        _shipController.adjustThrustZ = Mathf.Abs(_shipController.thrust.z) > 0.1f;
    }
}