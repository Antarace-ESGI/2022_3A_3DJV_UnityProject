using System;
using System.Collections.Generic;
using System.Linq;
using Checkpoints;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EndRaceScript : MonoBehaviour
{
    [SerializeField] private GameObject enablingPanel;
    [SerializeField] private Text text;

    //End race
    private Dictionary<GameObject, bool> playingEntities = new Dictionary<GameObject, bool>();
    private Dictionary<GameObject, GameObject> waitingPanels = new Dictionary<GameObject, GameObject>();

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
        GameObject nPlayer = obj.gameObject;
        nPlayer.gameObject.name = "Player" + _players;
        playingEntities.Add(nPlayer, false);
        waitingPanels.Add(obj.gameObject,obj.GetComponent<PlayerInputScript>().GetWaitingScreen());
        _players++;
    }

    public void AddAI(GameObject ai)
    {
        playingEntities.Add(ai,false);
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if ((collision.CompareTag("Player") || collision.CompareTag("AI")) 
            && playingEntities.Count > 0 && playingEntities[collision.gameObject] == false)
        {
            GameObject colEntity = collision.gameObject;
            runner++;

            playingEntities[colEntity] = true;
            _rank[colEntity.name] = runner;
            
            // If it is an AI, simply destroy it
            if (collision.CompareTag("AI"))
            {
                Destroy(colEntity);
            }

            // If it is a player, do some more stuff
            if (collision.CompareTag("Player"))
            {
                colEntity.SetActive(false);
                
                if (GameObject.FindGameObjectWithTag("HUD"))
                {
                    GameObject.FindGameObjectWithTag("HUD").SetActive(false);
                }
                Cursor.lockState = CursorLockMode.None;
                
                waitingPanels[colEntity].SetActive(true);

                // Send score only if there is 1 human playing
                if (_players == 1)
                {
                    var vehicleIndex = colEntity.GetComponent<VehicleLoader>().vehicleIndex;
                    var vehicleName = Vehicle.Vehicles[vehicleIndex].name;
                    StartCoroutine(SubmitEndRaceTime.SendTime(0, vehicleName, gameObject.scene.name));
                }
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
    
        // Disable all waiting screens
        foreach (KeyValuePair<GameObject,GameObject> panel in waitingPanels)
        {
            panel.Value.SetActive(false);
        }
        
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
            foreach (KeyValuePair<string,int> val in gameLeaderboard.ToList())
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
            if (gameLeaderboard != null && rank != null)
            {
                foreach (KeyValuePair<string,int> val in gameLeaderboard.ToList())
                {
                    if (rank.ContainsKey(val.Key))
                    {
                        rank[val.Key] += val.Value;
                    }
                }
            }
            script.SetGameLeaderboard(rank);
        }
    }

    public GameObject GetEnablingPanel()
    {
        return enablingPanel;
    }
}
