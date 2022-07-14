using System;
using System.Collections;
using Checkpoints;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInputManager))]
public class LoadSceneManager : MonoBehaviour
{
    public GameObject aiPrefab;
    public Text countdownText;

    private TrackArrayScript _trackArrayScript;
    private PlayerInputManager _inputManager;
    private const uint TotalPlayers = 4;

    public static DateTime startTime;

    private void Start()
    {
        _trackArrayScript = FindObjectOfType<TrackArrayScript>();
        _inputManager = GetComponent<PlayerInputManager>();

        StartLoading();
    }

    public void StartLoading()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    private IEnumerator LoadYourAsyncScene()
    {
        var asyncLoad = SceneManager.LoadSceneAsync(_trackArrayScript.GetIndexOfTrack(), LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        CheckpointController.LoadCheckpoints();
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        Time.timeScale = 0.0f;
        SpawnScript.ResetPlayerCount();

        var players = SelectedVehiclesScript.GetAllPlayers();

        foreach (var player in players)
        {
            var playerIndex = player.Key;
            var playerDevice = player.Value;
            _inputManager.JoinPlayer(playerIndex, pairWithDevice: playerDevice);
        }

        for (var i = players.Count; i < TotalPlayers; i++)
        {
            GameObject ai = Instantiate(aiPrefab);
            ai.name += i;
        }

        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        yield return new WaitForSecondsRealtime(1);
        countdownText.text = "4";
        yield return new WaitForSecondsRealtime(1);
        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(1);
        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(1);
        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(1);
        countdownText.text = "Go!";

        Time.timeScale = 1.0f;

        yield return new WaitForSecondsRealtime(1);
        countdownText.enabled = false;
        
        startTime = DateTime.Now;
    }
}