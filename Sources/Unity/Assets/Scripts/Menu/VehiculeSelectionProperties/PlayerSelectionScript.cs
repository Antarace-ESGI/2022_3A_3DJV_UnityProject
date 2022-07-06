using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayerSelectionScript : MonoBehaviour
{
    private PlayerInputManager _playerInputManager;
    private readonly List<WaitForAll> _players = new List<WaitForAll>();

    void Start()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();

        // Join default player
        var gamepad = Gamepad.current.device;
        var input = _playerInputManager.JoinPlayer(pairWithDevice: gamepad);
        var player = input.gameObject.GetComponent<WaitForAll>();
        player.playerSelection = this;
        _players.Add(player);

        _playerInputManager.onPlayerJoined += PlayerInputManagerOnPlayerJoined;
        _playerInputManager.onPlayerLeft += PlayerInputManagerOnPlayerLeft;
    }

    private void PlayerInputManagerOnPlayerLeft(PlayerInput obj)
    {
        _players.Remove(obj.gameObject.GetComponent<WaitForAll>());
    }

    private void PlayerInputManagerOnPlayerJoined(PlayerInput obj)
    {
        var player = obj.gameObject.GetComponent<WaitForAll>();
        player.playerSelection = this;
        _players.Add(player);
    }

    public void CheckPlayerReady()
    {
        var playerCount = _playerInputManager.playerCount;
        var readyPlayers = _players.Count(player => player.IsReady);

        Debug.Log($"{readyPlayers} players out of {playerCount} are ready");

        if (readyPlayers == playerCount)
        {
            StartGame();
        }
    }

    private static void StartGame()
    {
        Debug.Log("Start level");

        var brain = FindObjectOfType<TrackArrayScript>();
        if (brain != null)
        {
            SceneManager.LoadScene(brain.GetIndexOfTrack());
        }
    }
}