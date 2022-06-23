using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectTrackScript : MonoBehaviour
{
    [SerializeField] private int[] _tracklist;

    public void SetTrack()
    {
        TrackArrayScript brain = FindObjectOfType<TrackArrayScript>();

        if (brain)
        {
            brain.InitIndexes(_tracklist);
            brain.SetIndex(0);
        }
    }
    
}
