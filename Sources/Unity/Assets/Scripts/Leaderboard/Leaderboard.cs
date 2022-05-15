using System;
using System.Collections.Generic;
using System.Linq;
using Checkpoints;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Leaderboard : MonoBehaviour
{
    public CheckpointController self; // The current player

    private Text _text;
    private float _lastExecution;
    private readonly List<CheckpointController> _checkpointControllers = new List<CheckpointController>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] ais = GameObject.FindGameObjectsWithTag("AI");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (var entity in ais)
            _checkpointControllers.Add(entity.GetComponent<CheckpointController>());
        foreach (var entity in players)
            _checkpointControllers.Add(entity.GetComponent<CheckpointController>());
        
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_lastExecution > 1)
        {
            _text.text = $"{CalculatePosition() + 1} / {_checkpointControllers.Count}";
        }
        _lastExecution += Time.deltaTime;
    }

    int CalculatePosition()
    {
        _checkpointControllers.Sort(Comparison);
        return _checkpointControllers.FindIndex(controller => self == controller);
    }

    private int Comparison(CheckpointController x, CheckpointController y)
    {
        try
        {
            return x.GetTotalProgression() - y.GetTotalProgression();
        }
        catch (MissingReferenceException exception)
        {
            return 0;
        }
    }
}
