using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayerSelectionScript : MonoBehaviour
{
    void Start()
    {
        var playerInputManager = GetComponent<PlayerInputManager>();

        // Join default player
        var gamepad = Gamepad.current.device;
        playerInputManager.JoinPlayer(pairWithDevice: gamepad);
    }
}