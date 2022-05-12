using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackArrayScript : MonoBehaviour
{
    private static TrackArrayScript Instance { get; set; }
    private static int[] _indexes;
    private int _index = 0;

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

    public void InitIndexes(int[] t)
    {
        _indexes = new int[t.Length];
        _indexes = t;
        Debug.Log(_indexes);
    }

    public void FlushIndexes()
    {
        Array.Clear(_indexes,0,_indexes.Length);
    }

    public int GetIndexOfTrack()
    {
        if (_index < _indexes.Length)
            return _indexes[_index];
        else
            return 0;
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
    
}
