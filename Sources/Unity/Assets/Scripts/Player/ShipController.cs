using System;
using UnityEngine;
using Unity.Collections;

[RequireComponent(typeof(Rigidbody))]

public class ShipController : MonoBehaviour
{
    Rigidbody _ship;

    float _qtrScreenH;
    float _qtrScreenW;

    bool _adjustYaw;
    bool _adjustThrustX;
    bool _adjustThrustY;
    bool _adjustThrustZ;

    [ReadOnly] public Vector3 mousePosition;
    public float mouseDeadZone = 10.0f;
    Vector3 _centerScreen;

    float _yaw;
    float _yawDiff;

    public Vector3 thrust = Vector3.zero;

    // THROTTLE
    public float baseThrottle = 10f;
    public float boostMultiplier = 2f;
    public float boostDuration = 2f; // Boost duration in seconds
    private float _throttle;
    private float _boostTime = 0f;

    public RectTransform crosshair;
    public float maxRadius = 128f;

    // FLIGHT CONTROL PARAMETERS
    [Range(0, 100f)] public float yawStrength = 1.5f;

    public float floatDistance = 2;

    void Start()
    {
        _centerScreen = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        _ship = GetComponent<Rigidbody>();
        _qtrScreenH = Screen.height * 0.25f;
        _qtrScreenW = Screen.width * 0.25f;
        _throttle = baseThrottle;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        InputUpdate();
        DampenTransform();
    }

    void FixedUpdate()
    {
        // ADJUST YAW (LEFT/RIGHT/TURN/LOCAL Y)
        if (_adjustYaw)
            _ship.AddTorque(transform.up * (_yaw * yawStrength), ForceMode.Force);

        // ADJUST THRUST Z (FORWARD/BACK/LOCAL Z)
        if (_adjustThrustZ)
        {
            _ship.AddForce(transform.forward * (thrust.z * _throttle), ForceMode.Force);
        }

        // ADJUST THRUST X (LEFT/RIGHT/STRAFE/LOCAL X)
        if (_adjustThrustX)
        {
            _ship.AddForce(transform.right * (thrust.x * _throttle), ForceMode.Force);
        }

        // ADJUST THRUST Y (UP/DOWN/ASCEND/DESCEND/LOCAL Y)
        if (_adjustThrustY)
        {
            _ship.AddForce(transform.up * (_throttle * thrust.y), ForceMode.Force);
        }
    }


    void InputUpdate()
    {
        mousePosition = Input.mousePosition;
        _yaw = GetYawValue();
        
        thrust.x = Input.GetAxis("Horizontal");
        thrust.y = GetThrustY();
        thrust.z = Input.GetAxis("Vertical"); // Z is forward/Back

        // Set Flags
        _adjustYaw = Mathf.Abs(_yaw) > 0.1f;
        _adjustThrustX = Mathf.Abs(thrust.x) > 0.1f;
        _adjustThrustY = thrust.y != 0;
        _adjustThrustZ = Mathf.Abs(thrust.z) > 0.1f;

        // Boost
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (_boostTime < boostDuration)
            {
                _boostTime += Time.deltaTime; // Consume boost
                _throttle = baseThrottle * boostMultiplier;
            }
            else
            {
                _throttle = baseThrottle; // Just set to default speed
            }
        }
        else
        {
            _boostTime = Mathf.Max(0, _boostTime - Time.deltaTime); // Recharge boost
            _throttle = baseThrottle;
        }
    }

    float GetYawValue()
    {
        float x = Input.GetAxis("Mouse X");

        _yawDiff = Mathf.Clamp(crosshair.position.x - _centerScreen.x + x, -maxRadius, maxRadius);
        crosshair.position = new Vector3(_yawDiff + _centerScreen.x, _centerScreen.y, _centerScreen.z);
        
        bool direction = _yawDiff > 0; // true for left, false for right
        _yawDiff = Mathf.Max(0, Mathf.Abs(_yawDiff) - mouseDeadZone);
        _yawDiff = direction ? _yawDiff : -_yawDiff;
        _yawDiff = Mathf.Clamp(_yawDiff, -_qtrScreenW, _qtrScreenW);

        return (_yawDiff / _qtrScreenW);
    }

    float GetThrustY()
    {
        // Make the body float above the ground
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position + transform.forward * transform.localScale.z,
                transform.TransformDirection(Vector3.down), out hit, floatDistance, layerMask))
        {
            return hit.distance / (floatDistance / 2);
        }

        return -1.0f;
    }

    void DampenTransform()
    {
        Vector3 nVeloc = new Vector3(
            Mathf.Lerp(_ship.velocity.x, 0, Time.deltaTime * 0.75f),
            Mathf.Lerp(_ship.velocity.y, 0, Time.deltaTime * 0.75f),
            Mathf.Lerp(_ship.velocity.z, 0, Time.deltaTime * 0.75f)
        );

        Vector3 nAVeloc = new Vector3(
            Mathf.Lerp(_ship.angularVelocity.x, 0, Time.deltaTime),
            Mathf.Lerp(_ship.angularVelocity.y, 0, Time.deltaTime),
            Mathf.Lerp(_ship.angularVelocity.z, 0, Time.deltaTime)
        );

        _ship.velocity = nVeloc;
        _ship.angularVelocity = nAVeloc;
    }
}