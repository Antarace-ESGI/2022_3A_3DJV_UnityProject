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
        public GameObject[] checkpoints;

        public int _checkpointIndex = 0;
        private GameObject _nextCheckpoint, _currentCheckpoint;

        private void Start()
        {
            _currentCheckpoint = _nextCheckpoint = checkpoints[_checkpointIndex];
        }

        private void IncrementCheckpoint(GameObject currentCheckpoint)
        {
            if (currentCheckpoint.Equals(_nextCheckpoint))
            {
                _currentCheckpoint = checkpoints[_checkpointIndex];
                _checkpointIndex = Mathf.Min(_checkpointIndex + 1, checkpoints.Length - 1);
                _nextCheckpoint = checkpoints[_checkpointIndex];
            }
        }

        public void OnTriggerEnter(Collider collision)
        {
            IncrementCheckpoint(collision.gameObject);
        }

        public void DecrementCheckpoint()
        {
            _currentCheckpoint = checkpoints[_checkpointIndex];
            _checkpointIndex = Mathf.Max(_checkpointIndex - 1, 0);
            _nextCheckpoint = checkpoints[_checkpointIndex];
        }

        public GameObject GetCurrentCheckpoint()
        {
            Debug.Log($"{name}: {_currentCheckpoint.name}, {_nextCheckpoint.name}");
            return _currentCheckpoint;
        }

        public GameObject GetNextCheckpoint()
        {
            Debug.Log($"{name}: {_currentCheckpoint.name}, {_nextCheckpoint.name}");
            return _nextCheckpoint;
        }

        public void RespawnEntity()
        {
            transform.position = GetCurrentCheckpoint().transform.position + Vector3.up; // +1 up to make the ship float above ground
        }
    }
}