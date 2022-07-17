using System;
using System.Collections.Generic;
using System.Linq;
using Checkpoints;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class EndRaceScript : MonoBehaviour
{
    // Panneau qui affiche partie temrinée avec le leaderboard
    [SerializeField] private GameObject enablingPanel;

    // Element texte contenant les positions des joueurs
    [SerializeField] private Text text;

    //Leaderboard
    private static Dictionary<string, int> _rank = new Dictionary<string, int>();
    private GameObject _gameManager;

    // Quantité de joueurs et IA qui ont passés la ligne d'arrivée (donc LoadSceneManager.playingEntities == true)
    private int _runner;

    // Quantité de joueurs qui ont passés la ligne d'arrivée
    private int _players;

    private void Start()
    {
        _runner = 0;
        _players = 0;
        // General Game Manager
        _gameManager = GameObject.FindGameObjectWithTag("GameController");
    }

    private void OnTriggerEnter(Collider collision)
    {
        var playingEntities = GetPlayerEntities();

        if ((!collision.CompareTag("Player") && !collision.CompareTag("AI")) ||
            playingEntities[collision.gameObject]) return;

        var colEntity = collision.gameObject;
        _runner++;
        playingEntities[colEntity] = true; // Le joueur a passé la ligne d'arrivée
        _rank[colEntity.name] = _runner;

        if (collision.CompareTag("AI"))
        {
            var controller = colEntity.GetComponent<AiController>();
            controller.canMove = false; // Disable AI's brain
        }

        // If it is a player, do some more stuff
        if (collision.CompareTag("Player"))
        {
            _players++;

            var playerInput = colEntity.GetComponent<PlayerInputScript>();
            playerInput.UnregisterEvents();
            playerInput.DisplayWaitingScreen();

            // Send score only if there is 1 human playing
            if (SelectedVehiclesScript.GetAllPlayers().Count == 1)
            {
                SubmitScore(colEntity);
            }
        }

        // Lock player
        var body = colEntity.GetComponentInChildren<Rigidbody>();
        body.constraints = RigidbodyConstraints.FreezeAll;
        colEntity.transform.position = new Vector3(1000, 1000, 1000);

        // All players and IA crossed the finish line
        if (_runner < GetPlayerEntities().Count) return;
        DestroyEveryone();
        EndRaceDisplay();
    }

    public bool AllPlayersHaveFinished()
    {
        var playerCount = SelectedVehiclesScript.GetAllPlayers().Count;
        return _players >= playerCount;
    }

    public static void DestroyEveryone()
    {
        foreach (var player in GetPlayerEntities().Keys)
        {
            var input = player.GetComponent<PlayerInputScript>();
            Destroy(input ? input.completePlayer : player);
        }
    }

    private void SubmitScore(GameObject player)
    {
        var duration = DateTime.Now.Subtract(LoadSceneManager.startTime);
        var vehicleIndex = player.GetComponent<VehicleLoader>().vehicleIndex;
        var vehicleName = Vehicle.Vehicles[vehicleIndex].name;
        StartCoroutine(SubmitEndRaceTime.SendTime(duration.Seconds, vehicleName, gameObject.scene.name));
    }

    private void EndRaceDisplay()
    {
        enablingPanel.SetActive(true);

        if (_gameManager != null)
        {
            var script = _gameManager.GetComponent<TrackArrayScript>();

            if (script != null)
            {
                SetGlobalLeaderboard(script, _rank);

                if (script.IsEndTrack())
                {
                    GetGlobalLeaderboard(script);
                }
            }
        }

        DisplayLeaderboard();
    }

    public void SetRank()
    {
        int position = _rank.Count + 1;

        //Set value in temporary Ai
        foreach (var player in GetPlayerEntities())
        {
            if (_rank.ContainsKey(player.Key.name)) continue;

            _rank[player.Key.name] = position;
            position++;
        }

        EndRaceDisplay();
    }

    // TODO: Move method to LoadSceneManager as static method
    public static Dictionary<GameObject, bool> GetPlayerEntities()
    {
        return LoadSceneManager.playingEntities;
    }

    private void DisplayLeaderboard()
    {
        String rankString = "";
        if (text)
        {
            foreach (var rank in _rank)
            {
                rankString += $"{rank.Value}e - {rank.Key}\n";
            }

            text.text = rankString;
        }
    }

    private void GetGlobalLeaderboard(TrackArrayScript script)
    {
        Dictionary<string, int> gameLeaderboard = script.GetGameLeaderboard();
        int size = script.GetSize();
        if (gameLeaderboard != null && _rank != null)
        {
            foreach (KeyValuePair<string, int> val in gameLeaderboard.ToList())
            {
                if (_rank.ContainsKey(val.Key))
                {
                    _rank[val.Key] = val.Value / size;
                }
            }
        }
    }

    private void SetGlobalLeaderboard(TrackArrayScript script, Dictionary<string, int> rank)
    {
        if (_gameManager != null)
        {
            Dictionary<string, int> gameLeaderboard = script.GetGameLeaderboard();
            if (gameLeaderboard != null && rank != null)
            {
                foreach (KeyValuePair<string, int> val in gameLeaderboard.ToList())
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