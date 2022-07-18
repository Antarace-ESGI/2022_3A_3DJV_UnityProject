using System.Collections;
using Checkpoints;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

  public AudioClip RespawnSound;
  public AudioSource audioSource;

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
      //  yield return new WaitForSeconds(0.8f);
        audioSource.clip = RespawnSound;
        audioSource.Play();

    }
}
