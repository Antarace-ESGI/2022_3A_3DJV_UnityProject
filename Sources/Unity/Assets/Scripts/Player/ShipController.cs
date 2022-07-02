using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    private Rigidbody _ship;

    private bool _adjustYaw;
    private bool _adjustThrustX;
    private bool _adjustThrustY;
    private bool _adjustThrustZ;

    public float mouseDeadZone = 0.05f;

    private float _yaw;

    public Vector3 thrust = Vector3.zero;

    // THROTTLE
    public float baseThrottle = 10f;
    public float boostMultiplier = 2f;
    public float boostDuration = 2f; // Boost duration in seconds
    private bool _boostActive;
    private float _throttle;
    private float _boostTime;

    // FLIGHT CONTROL PARAMETERS
    [Range(0, 100f)] public float yawStrength = 1.5f;

    public float floatDistance = 2;
    private bool _isStuck;

    private Vector2 axis;

    private void Start()
    {
        axis = Vector2.zero;

        _ship = GetComponent<Rigidbody>();
        _throttle = baseThrottle;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        InputUpdate();
        DampenTransform();

        if (_boostActive)
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

    private void FixedUpdate()
    {
        // ADJUST YAW (LEFT/RIGHT/TURN/LOCAL Y)
        if (_adjustYaw)
        {
            _ship.AddTorque(transform.up * (_yaw * yawStrength), ForceMode.Force);
        }

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Environment") &&
            collision.GetContact(0).normal == Vector3.up)
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

    private void InputUpdate()
    {
        thrust.x = axis.x;
        thrust.y = GetThrustY();
        thrust.z = axis.y;

        if (_isStuck)
        {
            thrust.y += 10f;
        }

        // Set Flags
        _adjustYaw = Mathf.Abs(_yaw) > mouseDeadZone;
        _adjustThrustX = Mathf.Abs(thrust.x) > 0.1f;
        _adjustThrustY = thrust.y != 0;
        _adjustThrustZ = Mathf.Abs(thrust.z) > 0.1f;
    }

    public void SetYaw(float yaw)
    {
        _yaw = yaw;
    }

    public void Move(Vector2 dir)
    {
        axis = dir;
    }

    public void ActiveBoost(bool active)
    {
        _boostActive = active;
    }

    private float GetThrustY()
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

    private void DampenTransform()
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