using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class AiController : MonoBehaviour
{
    Rigidbody _ship;

    public GameObject[] checkpoints;
    private int _checkpointIndex = 0;
    private GameObject _nextCheckpoint;

    private GameObject _currentCheckpoint;

    [FormerlySerializedAs("AiLife")] public int aiLife = 50;

    private ShipController _shipController;
    
    void Start()
    {
        _ship = GetComponent<Rigidbody>();
        _nextCheckpoint = checkpoints[_checkpointIndex];
        _shipController = GetComponent<ShipController>();
    }

    void Update()
    {
        InputUpdate();

        if (aiLife <= 0)
        {
            _currentCheckpoint = checkpoints[_checkpointIndex - 1];
            _ship.gameObject.GetComponent<Transform>().position = _currentCheckpoint.transform.position;
            aiLife = 50;
        }
    }
    
    void InputUpdate()
    {
        _shipController.yaw = GetYawValue();
        _shipController.thrust.x = 0; // X is left/right
        _shipController.thrust.y = _shipController.GetThrustY();
        _shipController.thrust.z = 1; // Z is forward/Back

        // Set Flags
        _shipController.adjustYaw = Mathf.Abs(_shipController.yaw) > 0.1f;
        _shipController.adjustThrustX = Mathf.Abs(_shipController.thrust.x) > 0.1f;
        _shipController.adjustThrustY = _shipController.thrust.y != 0;
        _shipController.adjustThrustZ = Mathf.Abs(_shipController.thrust.z) > 0.1f;

        // _shipController.throttle = Mathf.Clamp(throttle, minThrottle, maxThrottle);
    }

    float GetYawValue()
    {
        return transform.InverseTransformPoint(_nextCheckpoint.transform.position).x;
    }

    public void IncrementCheckpoint(GameObject currentCheckpoint)
    {
        if (currentCheckpoint.Equals(_nextCheckpoint))
        {
            _checkpointIndex = Mathf.Min(_checkpointIndex + 1, checkpoints.Length - 1);
            _nextCheckpoint = checkpoints[_checkpointIndex];
        }
    }
}