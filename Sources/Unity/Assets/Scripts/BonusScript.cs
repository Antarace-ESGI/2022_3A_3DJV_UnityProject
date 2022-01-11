using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusScript : MonoBehaviour
{

    public Vector3 originalPosition;
    private Vector3 storagePosition;
    
    private float timer = 5.0f;
    
    void Start()
    {
        //Modify if you want
        storagePosition = new Vector3(0.0f, -500.0f, 0.0f);
    }
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 6)
        {
            gameObject.transform.position = storagePosition;
        }
    }

    void Update()
    {
        if (gameObject.transform.position == storagePosition)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                gameObject.transform.position = originalPosition;
            }
        }    
    }
}
