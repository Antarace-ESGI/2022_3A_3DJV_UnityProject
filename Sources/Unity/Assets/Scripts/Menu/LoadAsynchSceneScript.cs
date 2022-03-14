using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadAsynchSceneScript : MonoBehaviour
{

    public int indexScene;
    
    public Slider progressBar;
    public GameObject loadingMenu;
    
    public void LoadLevel()
    {
        if (indexScene >= 0)
        {
            StartCoroutine(AsyncLoadingProcess(indexScene));
        }
    }

    IEnumerator AsyncLoadingProcess(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        loadingMenu.SetActive(true);
        
        while (!operation.isDone)
        {
            progressBar.value = operation.progress;
            yield return null;
        }
    }
}
