using System.Collections.Generic;
using Checkpoints;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Leaderboard : MonoBehaviour
{
    public List<CheckpointController> players;
    public CheckpointController self; // The current player

    private Text _text;
    private float _lastExecution;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_lastExecution > 1)
        {
            _text.text = $"{CalculatePosition() + 1} / {players.Count}";
        }
        _lastExecution += Time.deltaTime;
    }

    int CalculatePosition()
    {
        players.Sort(Comparison);
        return players.FindIndex(controller => self == controller);
    }

    private int Comparison(CheckpointController x, CheckpointController y)
    {
        return x.GetTotalProgression() - y.GetTotalProgression();
    }
}
