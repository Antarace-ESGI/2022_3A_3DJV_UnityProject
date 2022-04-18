using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class ShipController : MonoBehaviour
{
    private Rigidbody _ship;

    internal bool adjustYaw;
    internal bool adjustThrustX;
    internal bool adjustThrustY;
    internal bool adjustThrustZ;

    // Boost
    private float _boostTime;
    public float boostMultiplier = 2f;
    public float boostDuration = 2f; // Boost duration in seconds

    internal float yaw;

    public Vector3 thrust = Vector3.zero;

    // THROTTLE
    public float baseThrottle = 10f;
    internal float throttle;

    // FLIGHT CONTROL PARAMETERS
    [Range(0, 100f)] public float yawStrength = 1.5f;

    public float floatDistance = 2;

    private void Start()
    {
        _ship = GetComponent<Rigidbody>();
        throttle = baseThrottle;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        DampenTransform();
    }

    private void FixedUpdate()
    {
        // ADJUST YAW (LEFT/RIGHT/TURN/LOCAL Y)
        if (adjustYaw)
        {
            _ship.AddTorque(transform.up * (yaw * yawStrength), ForceMode.Force);
        }

        // ADJUST THRUST Z (FORWARD/BACK/LOCAL Z)
        if (adjustThrustZ)
        {
            _ship.AddForce(transform.forward * (thrust.z * throttle), ForceMode.Force);
        }

        // ADJUST THRUST X (LEFT/RIGHT/STRAFE/LOCAL X)
        if (adjustThrustX)
        {
            _ship.AddForce(transform.right * (thrust.x * throttle), ForceMode.Force);
        }

        // ADJUST THRUST Y (UP/DOWN/ASCEND/DESCEND/LOCAL Y)
        if (adjustThrustY)
        {
            _ship.AddForce(transform.up * (throttle * thrust.y), ForceMode.Force);
        }
    }

    public void ActiveBoost(bool active)
    {
        if (active)
        {
            if (_boostTime < boostDuration)
            {
                _boostTime += Time.deltaTime; // Consume boost
                throttle = baseThrottle * boostMultiplier;
            }
            else
            {
                throttle = baseThrottle; // Just set to default speed
            }
        }
        else
        {
            _boostTime = Mathf.Max(0, _boostTime - Time.deltaTime); // Recharge boost
            throttle = baseThrottle;
        }
    }

    public float GetThrustY()
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