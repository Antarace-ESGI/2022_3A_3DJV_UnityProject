using Checkpoints;
using UnityEngine;

public class AccelerateEndRaceScript : MonoBehaviour
{
    private EndRaceScript _end;
    private GameObject _endPanel;

    [SerializeField] private GameObject actualPanel;
    [SerializeField] private GameObject selfPlayer;

    private void Start()
    {
        _end = FindObjectOfType<EndRaceScript>();
        var lastCheckpoint = selfPlayer.GetComponent<CheckpointController>().EndCheckpoint();
        Debug.Log($"last : {lastCheckpoint}");

        _endPanel = lastCheckpoint.GetComponent<EndRaceScript>().GetEnablingPanel();
        
        Debug.Log($"Accelerate : {_end} {_endPanel}");
    }

    public void EndRace()
    {
        if (!_end || !_end.AllPlayersHaveFinished()) return;
        Debug.Log("EndRace");

        // Changing panel : 
        _endPanel.SetActive(true);
        actualPanel.SetActive(false);
        
        // Close All waitings panels;
        CloseAllWaiting();
        
        // Set the ranking manually
        _end.SetRank();

        EndRaceScript.DestroyEveryone();
    }

    private void CloseAllWaiting()
    {
        var players = EndRaceScript.GetPlayerEntities();
        foreach (var player in players.Keys)
        {
            if (player != selfPlayer && player.CompareTag("Player"))
            {
                player.GetComponent<PlayerInputScript>().DisableWaitingScreen();
            }
        }
    }
}