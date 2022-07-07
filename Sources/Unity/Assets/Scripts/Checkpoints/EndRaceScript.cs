using System;
using System.Collections.Generic;
using Checkpoints;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EndRaceScript : MonoBehaviour
{
    [SerializeField] private GameObject enablingPanel;
    [SerializeField] private GameObject waitingPanel;
    [SerializeField] private Text text;
    [SerializeField] private Camera view;
    
    //End race
    private Dictionary<GameObject, bool> playingEntities = new Dictionary<GameObject, bool>();

    //Leaderboard
    private static Dictionary<string, int> _rank = new Dictionary<string, int>();
    private GameObject _gameManager;

    private int runner = 0;
    private int _players = 0;

    [SerializeField] private PlayerInputManager playerInputManager;

    private void Start()
    {
        runner = 0;
        
        // Find Gameobject everywhere !! EVERYWHERE !!!!!!!!!!!!!!!!!!!
        
        foreach (GameObject ai in GameObject.FindGameObjectsWithTag("AI"))
        {
            playingEntities.Add(ai,false);
        }
        
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        
        if (players != null && players.Length > 0)
        {
            _players = players.Length;
            foreach (GameObject player in players)
            {
                playingEntities.Add(player,false);
            }   
        }

        var playerManager = GameObject.FindGameObjectWithTag("PlayerManager");
        if (playerManager != null)
        {
            playerInputManager = playerManager.GetComponent<PlayerInputManager>();
            playerInputManager.onPlayerJoined += OnPlayerJoined;
        }

        // General Game Manager
        _gameManager = GameObject.FindGameObjectWithTag("GameController");

    }

    private void OnPlayerJoined(PlayerInput obj)
    {
        playingEntities.Add(obj.gameObject, false);
        _players++;
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if ((collision.CompareTag("Player") || collision.CompareTag("AI")) 
            && playingEntities.Count > 0 && playingEntities[collision.gameObject] == false)
        {
            Debug.Log("ok");
            GameObject colEntity = collision.gameObject;
            runner++;

            playingEntities[colEntity] = true;
            _rank[colEntity.name] = runner;
            
            if(collision.CompareTag("AI"))
                Destroy(colEntity);

            if (collision.CompareTag("Player"))
            {
                colEntity.SetActive(false);
                
                if (GameObject.FindGameObjectWithTag("HUD"))
                {
                    GameObject.FindGameObjectWithTag("HUD").SetActive(false);
                }
                Cursor.lockState = CursorLockMode.None;
                waitingPanel.SetActive(true);

                // Send score
                var vehicleIndex = colEntity.GetComponent<VehicleLoader>().vehicleIndex;
                var vehicleName = Vehicle.Vehicles[vehicleIndex].name;
                //StartCoroutine(SubmitEndRaceTime.SendTime(0, vehicleName, gameObject.scene.name));
            }

            //Wait for every participant to finish the race
            if (runner == playingEntities.Count)
            {
                EndRaceDisplay();
            }
        }
    }

    private void EndRaceDisplay()
    {
        Cursor.lockState = CursorLockMode.None;
        if(waitingPanel.activeSelf)
            waitingPanel.SetActive(false);
        enablingPanel.SetActive(true);
        if (_gameManager != null)
        {
            TrackArrayScript script = _gameManager.GetComponent<TrackArrayScript>();

            if (script != null)
            {
                SetGlobalLeaderboard(script,_rank);
                    
                if(script.IsEndTrack())
                    GetGlobalLeaderboard(script);
            }
                    
        }
                
        DisplayLeaderboard();
    }

    public void SetRank()
    {
        int position = _rank.Count + 1;
        
        //Set value in temporary Ai
        foreach (KeyValuePair<GameObject,bool> player in playingEntities)
        {
            if (!_rank.ContainsKey(player.Key.name))
            {
                _rank[player.Key.name] = position;
                position++;
            }
        }
        EndRaceDisplay();
    }

    public Dictionary<GameObject,bool> GetPlayerEntities()
    {
        return playingEntities;
    }

    public int GetPlayersCount()
    {
        return _players;
    }

    private void DisplayLeaderboard()
    {
        String rankString = "";
        if (text)
        {
            foreach (KeyValuePair<string,int> rank in _rank)
            {
                rankString += $"{rank.Value}e - {rank.Key}\n";
            }

            text.text = rankString;   
        }
    }

    private void GetGlobalLeaderboard(TrackArrayScript script)
    {
        
        Dictionary<string,int> gameLeaderboard =  script.GetGameLeaderboard();
        int size = script.GetSize();
        if (gameLeaderboard != null && _rank != null)
        {
            foreach (KeyValuePair<string,int> val in gameLeaderboard)
            {
                if (_rank.ContainsKey(val.Key))
                {
                    _rank[val.Key] = val.Value / size;
                }
            }
        }
        
    }

    private void SetGlobalLeaderboard(TrackArrayScript script, Dictionary<string,int> rank)
    {
        if (_gameManager != null)
        {
            Dictionary<string,int> gameLeaderboard = script.GetGameLeaderboard();
            if (gameLeaderboard != null && _rank != null)
            {
                foreach (KeyValuePair<string,int> val in gameLeaderboard)
                {
                    if (_rank.ContainsKey(val.Key))
                    {
                        rank[val.Key] += val.Value;
                    }
                }
            }
            script.SetGameLeaderboard(rank);
        }
    }

}
