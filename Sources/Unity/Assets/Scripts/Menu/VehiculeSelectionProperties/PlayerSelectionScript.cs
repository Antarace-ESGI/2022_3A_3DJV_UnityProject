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
        var gamepad = Gamepad.current?.device ?? Keyboard.current?.device ?? Mouse.current?.device;
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
        // Move player a bit from the other
        var inputGameObject = input.gameObject;
        inputGameObject.transform.position = new Vector3(input.playerIndex * 50, 0, 0);

        var player = inputGameObject.GetComponent<WaitForAll>();
        player.playerSelection = this;
        player.input = input;
        SelectedVehiclesScript.SetDeviceForPlayer(input.playerIndex, input.devices[0]); // Unsafe?
        _players.Add(player);
    }

    public void CheckPlayerReady()
    {
        var playerCount = _playerInputManager.playerCount;
        var readyPlayers = _players.Count(player => player.IsReady);

        if (readyPlayers == playerCount)
        {
            StartGame();
        }
    }

    private static void StartGame()
    {
        SceneManager.LoadScene(5); // Go to TrackScene
    }
}