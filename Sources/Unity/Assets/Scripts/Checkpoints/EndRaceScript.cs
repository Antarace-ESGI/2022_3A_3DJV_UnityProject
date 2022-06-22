using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EndRaceScript : MonoBehaviour
{

    [SerializeField] private GameObject enablingPanel;
    [SerializeField] private Text text;
    [SerializeField] private Camera view;
    
    //End race
    private Dictionary<GameObject, bool> playingEntities = new Dictionary<GameObject, bool>();
    private PlayerInputManager _observer;
    
    //Leaderboard
    private static Dictionary<string, int> _rank = new Dictionary<string, int>();
    private GameObject _gameManager;

    private int runner = 0; 

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
            foreach (GameObject player in players)
            {
                playingEntities.Add(player,false);
            }   
        }
        else
        {
            _observer = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerInputManager>();
        }

        // General Game Manager
        _gameManager = GameObject.FindGameObjectWithTag("GameController");

    }
    
    public void OnEnable()
    {
        if (_observer != null)
        {
            _observer.onPlayerJoined += input =>
            {
                playingEntities.Add(input.gameObject,false);
            };
        }
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
            
            if(collision.CompareTag("AI"))
                Destroy(colEntity);

            if (collision.CompareTag("Player"))
            {
                colEntity.SetActive(false);
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
        }
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
