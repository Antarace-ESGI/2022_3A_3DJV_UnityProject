using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserSprayScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("AI"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.drag = rb.velocity.magnitude;
        } 
    }
}
