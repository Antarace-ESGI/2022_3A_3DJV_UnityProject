using Checkpoints;
using UnityEngine;

[RequireComponent(typeof(CheckpointController))]
[RequireComponent(typeof(ShipController))]
public class AiController : MonoBehaviour
{
    public int aiLife = 50;

    private ShipController _shipController;
    private CheckpointController _checkpointController;
    private AINavigation _aiNavigation;
    
    void Start()
    {
        _checkpointController = GetComponent<CheckpointController>();
        _shipController = GetComponent<ShipController>();
        _aiNavigation = GetComponent<AINavigation>();
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
        float yaw = transform.InverseTransformPoint(_aiNavigation.GetNextNode().transform.position).x;
        //float yaw = transform.InverseTransformPoint(_checkpointController.GetNextCheckpoint().transform.position).x;
        _shipController.SetYaw(yaw);
    }
}