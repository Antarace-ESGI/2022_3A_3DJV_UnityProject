using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEditor;
using UnityEngine;

public class ElectricDragScript : MonoBehaviour
{
    
    [Header("Bonus parameters")]
    [SerializeField] private float timer = 5.0f;
    [SerializeField] private float delay = 0.5f;
    
    [Header("Position")] 
    [SerializeField] private float distance = 0.0f;
    [SerializeField] private float floatDistance = 2.0f;
    [SerializeField] private float timeOffset = 0.3f;

    [SerializeField] private GameObject player;
    private Vector3 velocity;
    
    
    public void SetPlayerTarget(GameObject player)
    {
        this.player = player;
    }
    
    
    private void Start()
    {
        float time = timer;
        StartCoroutine(DragPlayer(time));
        StartCoroutine(ResetVelocity(time + delay));
    }

    IEnumerator DragPlayer(float time)
    {
        yield return new WaitForSeconds(time);
        player.GetComponent<Rigidbody>().drag = player.GetComponent<Rigidbody>().velocity.magnitude;
    }

    IEnumerator ResetVelocity(float time)
    {
        yield return new WaitForSeconds(time);
        player.GetComponent<Rigidbody>().drag = 0.0f;
        Destroy(gameObject);
    }
    
    void LateUpdate()
    {
        Vector3 position = player.transform.position - player.transform.forward * distance;
        position.y += floatDistance;
        position.x -= 0.5f;

        transform.position = Vector3.SmoothDamp(transform.position, position, ref velocity, timeOffset);
        
    }
}
