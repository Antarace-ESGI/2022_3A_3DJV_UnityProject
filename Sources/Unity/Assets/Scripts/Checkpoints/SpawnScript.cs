using UnityEngine;

namespace Checkpoints
{
    [RequireComponent(typeof(CheckpointController))]
    public class SpawnScript : MonoBehaviour
    {
        private static int _spawnedPlayerCount;

        private void Start()
        {
            SpawnPlayer();
        }

        /// <summary>
        /// Teleports the player to spawn on first apparition
        /// </summary>
        private void SpawnPlayer()
        {
            var spawnPoints = CheckpointController.GetSpawnCheckpoint().GetComponentsInChildren<Transform>();
            var startTransform = spawnPoints[_spawnedPlayerCount++]; // Unsafe ?
            transform.position = startTransform.position;
            transform.rotation = startTransform.rotation;
        }
    }
}