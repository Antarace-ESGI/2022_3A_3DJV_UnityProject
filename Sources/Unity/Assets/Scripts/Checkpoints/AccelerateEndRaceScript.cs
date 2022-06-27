using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerateEndRaceScript : MonoBehaviour
{
    private EndRaceScript _end;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private GameObject actualPanel;

    private void Start()
    {
        _end = GameObject.FindObjectOfType<EndRaceScript>();
    }

    public void EndRace()
    {
        if (_end)
        {
            int? players = CheckPlayerState();
            if (players != null && players == _end.GetPlayersCount())
            {
                // Changing panel : 
                endPanel.SetActive(true);
                actualPanel.SetActive(false);
            
                // Set the ranking manually
                _end.SetRank();
            }
        }
    }

    private int? CheckPlayerState()
    {
        if (_end)
        {
            Dictionary<GameObject,bool> playingEntities =  _end.GetPlayerEntities();
            int playerCount =  0;

            foreach (GameObject player in playingEntities.Keys)
            {
                if (player.CompareTag("Player"))
                {
                    playerCount++;
                }
            }

            return playerCount;
        }

        return null;
    }
    
}
