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

    private void Start()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();

        // Join default player
        var gamepad = Gamepad.current.device;
        var input = _playerInputManager.JoinPlayer(pairWithDevice: gamepad);
        PlayerInputManagerOnPlayerJoined(input);

        _playerInputManager.onPlayerJoined += PlayerInputManagerOnPlayerJoined;
        _playerInputManager.onPlayerLeft += PlayerInputManagerOnPlayerLeft;
    }

    private void PlayerInputManagerOnPlayerLeft(PlayerInput input)
    {
        _players.Remove(input.gameObject.GetComponent<WaitForAll>());
    }

    private void PlayerInputManagerOnPlayerJoined(PlayerInput input)
    {
        var player = input.gameObject.GetComponent<WaitForAll>();
        player.playerSelection = this;
        player.input = input;
        SelectedVehiclesScript.SetDeviceForPlayer(input.playerIndex, input.devices[0]); // Unsafe?
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
        SceneManager.LoadScene(5); // Go to TrackScene
    }
}