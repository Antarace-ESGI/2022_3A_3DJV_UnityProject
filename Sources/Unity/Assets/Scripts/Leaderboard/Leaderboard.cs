using System;
using System.Collections.Generic;
using Checkpoints;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Leaderboard : MonoBehaviour
{
    public int checkpointAmount;
    public List<CheckpointController> players;
    public CheckpointController self; // The current player

    private Text _text;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Leaderboard started");
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = $"{CalculatePosition() + 1} / {players.Count}";
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
