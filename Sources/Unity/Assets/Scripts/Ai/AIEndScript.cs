using Checkpoints;
using UnityEngine;

public class AIEndScript : MonoBehaviour
{
    void Start()
    {

        CheckpointController checkpointController = GetComponent<CheckpointController>();
        if (checkpointController != null)
        {
            GameObject end = checkpointController.EndCheckpoint();
            EndRaceScript endRaceScript = end.GetComponent<EndRaceScript>();
            if (endRaceScript != null)
            {
                endRaceScript.AddAI(gameObject);
            }
        }
    }
}
