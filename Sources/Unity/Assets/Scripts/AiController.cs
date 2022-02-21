using System;
using UnityEngine;
using Unity.Collections;

[RequireComponent(typeof(Rigidbody))]

public class AiController : MonoBehaviour
{
    Rigidbody _ship;

    public Transform[] checkpoints;
    private int _checkpointIndex = 0;
    private Transform _nextCheckpoint;

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
    public float throttle = 100f;
    [Range(0, 50)] public float throttleAmount = 0.25f;
    [Range(0, 500f)] public float maxThrottle = 4f;
    [Range(-500, 100f)] public float minThrottle = -2f;

    // FLIGHT CONTROL PARAMETERS
    [Range(0, 100f)] public float yawStrength = 1.5f;

    public bool flightAssist;

    // IMPULSE MODE
    float _impulseTimer;
    public bool impulseMode;
    public float impulseCoolDown = 3.0f;

    public float floatDistance = 2;


    void Start()
    {
        _centerScreen = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        _ship = GetComponent<Rigidbody>();
        _qtrScreenH = Screen.height * 0.25f;
        _qtrScreenW = Screen.width * 0.25f;

        _nextCheckpoint = checkpoints[_checkpointIndex];
    }


    void Update()
    {
        UpdateTimers();
        InputUpdate();

        if (flightAssist)
        {
            DampenTransform();
        }
    }


    void UpdateTimers()
    {
        _impulseTimer += Time.deltaTime;
    }


    void FixedUpdate()
    {
        // ADJUST YAW (LEFT/RIGHT/TURN/LOCAL Y)
        if (_adjustYaw)
            _ship.AddTorque(transform.up * (_yaw * yawStrength), ForceMode.Force);

        // ADJUST THRUST Z (FORWARD/BACK/LOCAL Z)
        if (_adjustThrustZ)
        {
            if (!impulseMode)
            {
                _ship.AddForce(transform.forward * (thrust.z * throttle), ForceMode.Force);
            }
            else if (_impulseTimer >= impulseCoolDown)
            {
                _ship.AddForce(transform.forward * (thrust.z * throttle), ForceMode.Impulse);
                _impulseTimer = 0.0f;
            }
        }

        // ADJUST THRUST X (LEFT/RIGHT/STRAFE/LOCAL X)
        if (_adjustThrustX)
        {
            if (!impulseMode)
            {
                _ship.AddForce(transform.right * (thrust.x * throttle), ForceMode.Force);
            }
            else if (_impulseTimer >= impulseCoolDown)
            {
                _ship.AddForce(transform.right * (thrust.x * throttle), ForceMode.Impulse);
                _impulseTimer = 0.0f;
            }
        }

        // ADJUST THRUST Y (UP/DOWN/ASCEND/DESCEND/LOCAL Y)
        if (_adjustThrustY)
        {
            if (!impulseMode)
            {
                _ship.AddForce(transform.up * (throttle * thrust.y), ForceMode.Force);
            }

            if (impulseMode && _impulseTimer >= impulseCoolDown)
            {
                _ship.AddForce(transform.up * (throttle * thrust.y), ForceMode.Impulse);
                _impulseTimer = 0.0f;
            }
        }
    }


    void InputUpdate()
    {
        mousePosition = Input.mousePosition;
        _yaw = GetYawValue();
        thrust.x = 0; // X is left/right
        thrust.y = GetThrustY();
        thrust.z = 1; // Z is forward/Back

        // Set Flags
        _adjustYaw = Mathf.Abs(_yaw) > 0.1f;
        _adjustThrustX = Mathf.Abs(thrust.x) > 0.1f;
        _adjustThrustY = thrust.y != 0;
        _adjustThrustZ = Mathf.Abs(thrust.z) > 0.1f;

        throttle = Mathf.Clamp(throttle, minThrottle, maxThrottle);
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

    
    float GetYawValue()
    {
        return transform.InverseTransformPoint(_nextCheckpoint.transform.position).x;
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