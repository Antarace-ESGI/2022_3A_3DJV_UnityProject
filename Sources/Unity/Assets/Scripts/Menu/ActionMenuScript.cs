using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActionMenuScript : MonoBehaviour
{
    
    public GameObject inPausePanel;
    public GameObject inGamePanel;

    public GameObject targetPanel;
    public int dest = 0;

    private void switchPanel(GameObject currentPanel, GameObject nextPanel)
    {
        currentPanel.SetActive(false);
        nextPanel.SetActive(true);
    }

    public void navigationButton()
    {
        switchPanel(transform.parent.gameObject, targetPanel);
    }
    
    public void enablePause()
    {
        Time.timeScale = 0.0f;
        switchPanel(inGamePanel,inPausePanel);
    }
    public void disablePause()
    {
        Time.timeScale = 1.0f;
        switchPanel(inPausePanel,inGamePanel);
    }
    public void exitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
    
    private void controllerActionButton()
    {
        if (Input.GetButtonDown("Start") && inGamePanel != null && inPausePanel != null)
        {
            if (inPausePanel.activeInHierarchy)
            {
                disablePause();
            }
            else
            {
                enablePause();
            }
            
        }
    }
    public void changeScene()
    {
        SceneManager.LoadScene(dest);
    }

    public void changeSceneWithIndex()
    {
        TrackArrayScript brain = FindObjectOfType<TrackArrayScript>();
        if (brain)
            SceneManager.LoadScene(brain.GetIndexOfTrack());
    }

    public void LoadAdditiveScene()
    {
        TrackArrayScript brain = FindObjectOfType<TrackArrayScript>();
        if (brain)
        {
            brain.SetIndex(brain.GetIndex()+1);
            int i = brain.GetIndexOfTrack();
            if(i == 0)
                gameObject.SetActive(false);
            else
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive);
                inGamePanel.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }

    public void Update()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            controllerActionButton();
        }
    }
}
