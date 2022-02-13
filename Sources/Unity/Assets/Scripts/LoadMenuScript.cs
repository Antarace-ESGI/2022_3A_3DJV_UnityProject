using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenuScript : MonoBehaviour
{

    public string menuScene = "MenuScene";

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(menuScene, LoadSceneMode.Additive);
    }
}
