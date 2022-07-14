using System.Collections.Generic;
using Checkpoints;
using UnityEngine;

public class AccelerateEndRaceScript : MonoBehaviour
{
    private EndRaceScript _end;
    private GameObject _endPanel;
    
    [SerializeField] private GameObject actualPanel;
    [SerializeField] private GameObject selfPlayer;
    
    private void Start()
    {
        _end = FindObjectOfType<EndRaceScript>();
        _endPanel = selfPlayer.GetComponent<CheckpointController>().EndCheckpoint().GetComponent<EndRaceScript>()
            .GetEnablingPanel();
    }

    public void EndRace()
    {
        if (_end)
        {
            int? players = CheckPlayerState();
            Debug.Log(players);
            Debug.Log(_end.GetPlayersCount());
            if (players != null && players == _end.GetPlayersCount())
            {
                // Changing panel : 
                _endPanel.SetActive(true);
                actualPanel.SetActive(false);
                // Close All waitings panels;
                CloseAllWaiting();
                // Set the ranking manually
                _end.SetRank();

                var ais = GameObject.FindGameObjectsWithTag("AI");
                foreach (var ai in ais)
                {
                    Destroy(ai);
                }
            }
        }
    }

    private int? CheckPlayerState()
    {
        if (_end)
        {
            Dictionary<GameObject,bool> playingEntities =  _end.GetPlayerEntities();
            int playerCount =  0;
            
            foreach (KeyValuePair<GameObject,bool> player in  playingEntities)
            {
                if (player.Key != null && player.Key.CompareTag("Player") && player.Value)
                    playerCount++;
            }

            return playerCount;
        }

        return null;
    }

    private void CloseAllWaiting()
    {
        Dictionary<GameObject,bool> players =  _end.GetPlayerEntities();
        foreach (GameObject player in players.Keys)
        {
            if(player != selfPlayer && player.CompareTag("Player"))
                player.GetComponent<PlayerInputScript>().GetWaitingScreen().SetActive(false);
        }
    }
    
}
