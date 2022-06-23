using System.Collections;
using Checkpoints;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        CheckpointController checkpointController = collision.gameObject.GetComponent<CheckpointController>();
        
        if (checkpointController != null)
        {
            StartCoroutine(ReplacePlayer(checkpointController));
        }
    }

    private IEnumerator ReplacePlayer(CheckpointController checkpointController)
    {
        yield return new WaitForSeconds(0.8f);
        checkpointController.RespawnEntity();
    }
}