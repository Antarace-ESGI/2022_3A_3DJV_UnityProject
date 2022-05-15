using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EndRaceScript : MonoBehaviour
{

    [SerializeField] private GameObject enablingPanel;
    [SerializeField] private Camera view;
    
    private Dictionary<GameObject, bool> playingEntities = new Dictionary<GameObject, bool>(); 

    private int runner = 0;

    private void Start()
    {
        runner = 0;
        
        foreach (GameObject ai in GameObject.FindGameObjectsWithTag("AI"))
        {
            playingEntities.Add(ai,false);
        }

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            playingEntities.Add(player,false);
        }
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if ((collision.CompareTag("Player") || collision.CompareTag("AI")) && playingEntities[collision.gameObject] == false) 
        {
            runner++;

            playingEntities[collision.gameObject] = true;
            
            if(collision.CompareTag("AI"))
                Destroy(collision.gameObject);

            if (collision.CompareTag("Player"))
            {
                collision.gameObject.SetActive(false);
                view.enabled = true;
                
                if (GameObject.FindGameObjectWithTag("HUD"))
                {
                    GameObject.FindGameObjectWithTag("HUD").SetActive(false);
                }
            }

            //Wait for every participant to finish the race
            if (runner == playingEntities.Count)
            {
                Cursor.lockState = CursorLockMode.None;
                enablingPanel.SetActive(true);
            }
        }
    }

}
