using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Transform playerSpawn;

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerSpawn.position = transform.position;
        }
        else if (collision.CompareTag("AI"))
        {
            var controller = collision.GetComponent<AiController>();
            controller.IncrementCheckpoint(gameObject);
        }
    }
}