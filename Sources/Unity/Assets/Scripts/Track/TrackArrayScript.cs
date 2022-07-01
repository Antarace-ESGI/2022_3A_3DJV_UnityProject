using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class TrackArrayScript : MonoBehaviour
{
    
    // Track component
    private static TrackArrayScript Instance { get; set; }
    private static int[] _indexes;
    private int _index = 0;

    // Leaderboard component

    private static Dictionary<string, int> _leaderboard;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Track 

    public void InitIndexes(int[] t)
    {
        _indexes = new int[t.Length];
        _indexes = t;
    }

    public bool IsEndTrack()
    {
        if (_indexes != null)
        {
            if (_indexes.Length - 1 == _index)
                return true;
        }
        return false;
    }

    public void FlushIndexes()
    {
        if(_indexes != null)
            Array.Clear(_indexes,0,_indexes.Length);
    }

    public void AutoFlush()
    {
        if(IsEndTrack())
            FlushIndexes();
    }

    public int GetIndexOfTrack()
    {
        if (_index < _indexes.Length)
            return _indexes[_index];
        else
            return 0;
    }

    public int GetSize()
    {
        return _indexes.Length;
    }

    public int GetFirstScene()
    {
        return _indexes[0];
    }

    public int GetIndex()
    {
        return _index;
    }

    public void SetIndex(int index)
    {
        _index = index;
    }
    
    // Leaderboard

    public void SetGameLeaderboard(Dictionary<string,int> turn)
    {
        if (_leaderboard == null)
            _leaderboard = new Dictionary<string, int>();

        _leaderboard = turn;
    }

    [CanBeNull]
    public Dictionary<string,int> GetGameLeaderboard()
    {
        return _leaderboard;
    }

    private void OnDestroy()
    {
        FlushIndexes();
    }
}
