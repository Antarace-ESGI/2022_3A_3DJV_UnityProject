using Checkpoints;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CheckpointController))]
[RequireComponent(typeof(ShipController))]
[RequireComponent(typeof(Rigidbody))]
public class AiController : MonoBehaviour
{
    public int aiLife = 50;
    public bool canMove = true;

    private ShipController _shipController;
    private CheckpointController _checkpointController;
    private NavMeshAgent _agent;
    private Rigidbody _body;

    private void Start()
    {
        _checkpointController = GetComponent<CheckpointController>();
        _shipController = GetComponent<ShipController>();
        _body = GetComponent<Rigidbody>();

        _agent = GetComponent<NavMeshAgent>();
        _agent.updatePosition = false;
        _agent.updateRotation = false;
        _agent.isStopped = true;
    }

    private void Update()
    {
        if (!canMove) return;

        // Calculate paths
        var nextCheckpoint = _checkpointController.GetNextCheckpoint().transform.position;

        var target = _agent.steeringTarget;
        var desiredVelocity = _agent.desiredVelocity.normalized.magnitude;

        _agent.Warp(transform.position);
        _agent.velocity = _body.velocity;

        if (_agent.isOnNavMesh) {
            _agent.SetDestination(nextCheckpoint);
        }

        // Moving methods
        var yaw = transform.InverseTransformPoint(target).x;
        
        _shipController.SetYaw(yaw);
        _shipController.Move(Vector2.up * desiredVelocity);

        // Respawn on death
        if (aiLife <= 0)
        {
            _checkpointController.RespawnEntity();
            aiLife = 50;
        }
    }
}