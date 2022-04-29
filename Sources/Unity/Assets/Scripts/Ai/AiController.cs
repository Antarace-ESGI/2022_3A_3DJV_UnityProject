using Checkpoints;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CheckpointController))]
[RequireComponent(typeof(ShipController))]
public class AiController : MonoBehaviour
{
    Rigidbody _ship;
    public int aiLife = 50;

    private ShipController _shipController;
    private CheckpointController _checkpointController;
    
    void Start()
    {
        _ship = GetComponent<Rigidbody>();
        _checkpointController = GetComponent<CheckpointController>();
        _shipController = GetComponent<ShipController>();
    }

    void Update()
    {
        InputUpdate();

        if (aiLife <= 0)
        {
            _checkpointController.RespawnEntity();
            aiLife = 50;
        }
    }
    
    void InputUpdate()
    {
        GetYawValue();
        _shipController.Move(Vector2.up);
    }

    private void GetYawValue()
    {
        float yaw = transform.InverseTransformPoint(_checkpointController.GetNextCheckpoint().transform.position).x;
        _shipController.SetYaw(yaw);
    }
}