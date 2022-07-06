using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class LoadPlayerScript : MonoBehaviour
{
    private void Start()
    {
        var inputManager = GetComponent<PlayerInputManager>();
        var players = SelectedVehiclesScript.GetAllPlayers();

        foreach (var player in players)
        {
            var playerIndex = player.Key;
            var playerDevice = player.Value;
            inputManager.JoinPlayer(playerIndex, pairWithDevice: playerDevice);
            Debug.Log($"Joining player {playerIndex} with device {playerDevice.name}");
        }
    }
}