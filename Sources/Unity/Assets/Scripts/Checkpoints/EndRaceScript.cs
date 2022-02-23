using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EndRaceScript : MonoBehaviour
{

    public GameObject enablingPanel;
    public GameObject disablingPanel;

    private GameObject[] playingEntities;
    private int runner = 0;

    private void Start()
    {
        GameObject[] ai = GameObject.FindGameObjectsWithTag("AI");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        playingEntities = players.Concat(ai).ToArray();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("AI"))
        {
            runner++;
            
            if(collision.CompareTag("AI"))
                Destroy(collision.gameObject);
            
            //Wait for every participant to finish the race
            if (runner == playingEntities.Length)
            {
                Time.timeScale = 0.0f;
                enablingPanel.SetActive(true);
                disablingPanel.SetActive(false);
            }
        }
    }
    
}
