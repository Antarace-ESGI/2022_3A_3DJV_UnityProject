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
    private List<CheckpointController> _checkpointControllers;
    public int _rank;

    // Start is called before the first frame update
    void Start()
    {
        _checkpointControllers = FindObjectsOfType<CheckpointController>().ToList();

        _text = GetComponent<Text>();
        _rank = _checkpointControllers.Count;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_lastExecution > 1)
        {
            _rank = CalculatePosition() + 1;
            _text.text = $"{_rank} / {_checkpointControllers.Count}";
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
            return (int) ((x.GetTotalProgression() - y.GetTotalProgression()) * 100);
        }
        catch (MissingReferenceException)
        {
            return 0;
        }
    }

    public int GetRank()
    {
        return _rank;
    }
}
