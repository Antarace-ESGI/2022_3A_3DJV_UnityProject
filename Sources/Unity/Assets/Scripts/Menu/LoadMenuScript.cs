using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenuScript : MonoBehaviour
{

    public string menuScene = "MenuScene";

    // Will be update later to unable only digit 
    void Start()
    {
        SceneManager.LoadScene(menuScene, LoadSceneMode.Additive);
    }
}
