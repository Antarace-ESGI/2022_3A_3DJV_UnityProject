using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionMenuScript : MonoBehaviour
{
    public GameObject currentPanel;
    public GameObject nextPanel;

    public AudioClip MenuSelectSound;
    public AudioSource audioSource;

    public int dest = 0;

    private void CurrentPanelClose()
    {
        currentPanel.SetActive(false);
    }

    private void CurrentPanelOpen()
    {
        currentPanel.SetActive(true);
    }

    private void NextPanelOpen()
    {
      audioSource.clip = MenuSelectSound;
      audioSource.Play();
      nextPanel.SetActive(true);
    }

    private void NextPanelClose()
    {
        nextPanel.SetActive(false);
    }

    public void navigationButton()
    {
        CurrentPanelClose();
        NextPanelOpen();
    }

    public void enablePause()
    {
        Time.timeScale = 0.0f;
        CurrentPanelClose();
        NextPanelOpen();
    }
    public void disablePause()
    {
        Time.timeScale = 1.0f;
        CurrentPanelClose();
        NextPanelOpen();
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
        GameObject inGamePanel = currentPanel;
        GameObject inPausePanel = nextPanel;
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

    public void changeScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void JustRemovePlayer(WaitForAll waitForAll)
    {
        waitForAll.IsReady = true;
    }

    public void LoadAdditiveScene()
    {
        GameObject inGamePanel = currentPanel;
        TrackArrayScript brain = FindObjectOfType<TrackArrayScript>();
        if (brain)
        {
            int actual = brain.GetIndexOfTrack();
            brain.SetIndex(brain.GetIndex()+1);
            int next = brain.GetIndexOfTrack();
            if(next == 0)
                gameObject.SetActive(false);
            else
            {
                SceneManager.UnloadSceneAsync(actual);
                inGamePanel.SetActive(false);
                FindObjectOfType<LoadSceneManager>().StartLoading();
            }
        }
        else
        {
            gameObject.SetActive(false);
            // Default redirect to menu
            changeScene(3);
        }

    }

    public void KillTrack()
    {
        TrackArrayScript track = FindObjectOfType<TrackArrayScript>();
        if(track)
            track.CleanTrackArray();
    }

    public void Update()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            controllerActionButton();

        }


    }
}
