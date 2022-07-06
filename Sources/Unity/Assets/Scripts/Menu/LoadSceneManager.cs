using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    void Start()
    {
        TrackArrayScript trackArrayScript = FindObjectOfType<TrackArrayScript>();
        if(trackArrayScript != null)
            SceneManager.LoadScene(trackArrayScript.GetFirstScene(), LoadSceneMode.Additive);
    }
}
