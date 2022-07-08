using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInputManager))]
public class LoadSceneManager : MonoBehaviour
{
    public GameObject aiPrefab;

    private TrackArrayScript _trackArrayScript;
    private PlayerInputManager _inputManager;
    private const uint TotalPlayers = 4;

    void Start()
    {
        _trackArrayScript = FindObjectOfType<TrackArrayScript>();
        _inputManager = GetComponent<PlayerInputManager>();

        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        var asyncLoad = SceneManager.LoadSceneAsync(_trackArrayScript.GetFirstScene(), LoadSceneMode.Additive);

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
        }

        for (var i = players.Count; i < TotalPlayers; i++)
        {
            Instantiate(aiPrefab);
        }
    }
}