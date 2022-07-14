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

        private void OnEnable()
        {
            _currentCheckpoint = _nextCheckpoint = _checkpoints[checkpointIndex];
            Debug.Log("Loaded current and next checkpoints");
        }

        /// <summary>
        /// Reloads the checkpoint array, must be called once on every level change.
        /// </summary>
        public static void LoadCheckpoints()
        {
            _checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
            Debug.Log($"Found {_checkpoints.Length} checkpoints!");
        }

        public static GameObject GetSpawnCheckpoint()
        {
            return _checkpoints[0];
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

        public GameObject EndCheckpoint()
        {
            GameObject end = _checkpoints[_checkpoints.Length-1];
            return end;
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

        public float GetTotalProgression()
        {
            var nextCheckpointPosition = _nextCheckpoint.transform.position;
            var distanceBetweenCheckpoint = Vector3.Distance(_currentCheckpoint.transform.position, nextCheckpointPosition);
            var distanceToNextCheckpoint = Vector3.Distance(transform.position, nextCheckpointPosition);

            var progressionBetweenCheckpoints = distanceToNextCheckpoint / distanceBetweenCheckpoint;
            var progression = 1 - checkpointIndex / (_checkpoints?.Length ?? 1.0f);

            return progression * 10 + progressionBetweenCheckpoints;
        }
    }
}