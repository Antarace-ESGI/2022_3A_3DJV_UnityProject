using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public GameObject ReturnPause()
    {
        return panel;
    }

    public void PauseProcess(GameObject MainUI)
    {
        if (panel != null)
        {
            if (panel.activeInHierarchy)
            {
                Debug.Log("Pause");
                OffPause(MainUI);
            }
            else
            {
                Debug.Log("Game");
                OnPause(MainUI);
            }
        }
    }

    public void OnPause(GameObject MainUI)
    {
        MainUI.SetActive(false);
        Time.timeScale = 0.0f;
        panel.SetActive(true);
    }

    public void OffPause(GameObject MainUI)
    {
        MainUI.SetActive(true);
        Time.timeScale = 1.0f;
        panel.SetActive(false);
    }
    
}
