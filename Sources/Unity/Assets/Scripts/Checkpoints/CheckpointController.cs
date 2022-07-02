using Unity.Collections;
using UnityEngine;

namespace Checkpoints
{
    /// <summary>
    /// This class handles checkpoint increment by passing inside a checkpoint
    /// as well as the player entities' (Players and AIs) respawns.
    /// It also provides an interface for getting and setting checkpoints' related values.
    /// </summary>
    public class CheckpointController : MonoBehaviour
    {
        private static GameObject[] _checkpoints;

        // Real checkpoint
        [ReadOnly] public int checkpointIndex;
        private GameObject _nextCheckpoint, _currentCheckpoint;

        private void Start()
        {
            _checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
            _currentCheckpoint = _nextCheckpoint = _checkpoints[checkpointIndex];

            // Teleports the player to spawn on first apparition
            var startTransform = _checkpoints[0].transform;
            transform.position = startTransform.position;
            transform.rotation = startTransform.rotation;
        }

        private void IncrementCheckpoint(GameObject currentCheckpoint)
        {
            if (currentCheckpoint.Equals(_nextCheckpoint))
            {
                _currentCheckpoint = _checkpoints[checkpointIndex];
                checkpointIndex = Mathf.Min(checkpointIndex + 1, _checkpoints.Length - 1);
                _nextCheckpoint = _checkpoints[checkpointIndex];
            }
        }

        public void OnTriggerEnter(Collider collision)
        {
            IncrementCheckpoint(collision.gameObject);
        }

        public void DecrementCheckpoint()
        {
            _currentCheckpoint = _checkpoints[checkpointIndex];
            checkpointIndex = Mathf.Max(checkpointIndex - 1, 0);
            _nextCheckpoint = _checkpoints[checkpointIndex];
        }

        private GameObject GetCurrentCheckpoint()
        {
            return _currentCheckpoint;
        }

        public GameObject GetNextCheckpoint()
        {
            return _nextCheckpoint;
        }

        public void RespawnEntity()
        {
            transform.position = GetCurrentCheckpoint().transform.position + Vector3.up; // +1 up to make the ship float above ground
        }

        public int GetTotalProgression()
        {
            var nextCheckpointPosition = _nextCheckpoint.transform.position;
            var distanceBetweenCheckpoint = Vector3.Distance(_currentCheckpoint.transform.position, nextCheckpointPosition);
            var distanceToNextCheckpoint = Vector3.Distance(transform.position, nextCheckpointPosition);

            var progressionBetweenCheckpoints = distanceToNextCheckpoint / distanceBetweenCheckpoint;

            int progression = (int) (checkpointIndex * 100 + progressionBetweenCheckpoints * 100) / _checkpoints.Length * 100;

            return progression;
        }
    }
}