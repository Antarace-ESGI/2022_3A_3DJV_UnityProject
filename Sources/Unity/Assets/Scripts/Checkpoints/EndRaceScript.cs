using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EndRaceScript : MonoBehaviour
{

    [SerializeField] private GameObject enablingPanel;
    [SerializeField] private Text text;
    [SerializeField] private Camera view;
    
    //End race
    private Dictionary<GameObject, bool> playingEntities = new Dictionary<GameObject, bool>();
    
    //Leaderboard
    private static Dictionary<string, int> _rank = new Dictionary<string, int>();
    private GameObject _gameManager;

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
        
        _gameManager = GameObject.FindGameObjectWithTag("GameController");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if ((collision.CompareTag("Player") || collision.CompareTag("AI")) && playingEntities[collision.gameObject] == false)
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
                DisplayLeaderboard();

                if (_gameManager != null)
                {
                    TrackArrayScript script = _gameManager.GetComponent<TrackArrayScript>();
                    SetGlobalLeaderboard(script);
                }
                
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
        if (gameLeaderboard != null && _rank != null)
        {
            AverageRank(gameLeaderboard);
        }
        
    }

    private void SetGlobalLeaderboard(TrackArrayScript script)
    {
        if (_gameManager != null)
        {
            GetGlobalLeaderboard(script);
            script.SetGameLeaderboard(_rank);
        }
    }

    private void AverageRank(Dictionary<string,int> gameLeaderboard)
    {
        foreach (KeyValuePair<string,int> val in gameLeaderboard)
        {
            if (_rank.ContainsKey(val.Key))
            {
                _rank[val.Key] = (_rank[val.Key] + val.Value) / 2;
            }
        }
    }
    

}
