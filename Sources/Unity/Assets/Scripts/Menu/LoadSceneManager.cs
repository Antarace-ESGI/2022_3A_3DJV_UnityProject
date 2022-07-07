using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInputManager))]
public class LoadSceneManager : MonoBehaviour
{
    private TrackArrayScript _trackArrayScript;
    private PlayerInputManager _inputManager;
    
    void Start()
    {
        _trackArrayScript = FindObjectOfType<TrackArrayScript>();
        _inputManager = GetComponent<PlayerInputManager>();

        StartLoading();
    }

    public void StartLoading()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        var asyncLoad = SceneManager.LoadSceneAsync(_trackArrayScript.GetIndexOfTrack(), LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        var players = SelectedVehiclesScript.GetAllPlayers();

        foreach (var player in players)
        {
            var playerIndex = player.Key;
            var playerDevice = player.Value;
            _inputManager.JoinPlayer(playerIndex, pairWithDevice: playerDevice);
            Debug.Log($"Joining player {playerIndex} with device {playerDevice.name}");
        }
    }
    
}