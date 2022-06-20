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
        var nextNode = _aiNavigation.GetNextNode();
        if (nextNode != null)
        {
            var nextPosition = nextNode.transform.position;
            var currentNode = _aiNavigation.GetCurrentNode();
            var currentNodePosition = currentNode.transform.position;

            var targetSpeed = currentNode.GetComponent<AINode>().targetSpeed;
            var nextTargetSpeed = nextNode.GetComponent<AINode>().targetSpeed;

            var yaw = transform.InverseTransformPoint(nextPosition).x;
            _shipController.SetYaw(yaw);

            var nodeDistance = Vector3.Distance(currentNodePosition, nextPosition);
            var progressDistance = Vector3.Distance(transform.position, nextPosition);
            var progress = progressDistance / nodeDistance;
            var interpolatedSpeed = Mathf.Lerp(nextTargetSpeed, targetSpeed, progress);

            var speed = Vector2.up * interpolatedSpeed;
            _shipController.Move(speed);   
        }
        if (aiLife <= 0)
        {
            _checkpointController.RespawnEntity();
            aiLife = 50;
        }
    }
}