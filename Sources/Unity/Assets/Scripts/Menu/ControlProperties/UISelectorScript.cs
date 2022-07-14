using UnityEngine;
using UnityEngine.EventSystems;

public class UISelectorScript : MonoBehaviour
{
    // Just a script to set select button for the Gamepad option

    private GameObject go;

    public AudioClip MenuMoveSound;
    public AudioSource audioSource;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(this.gameObject);
        go = EventSystem.current.currentSelectedGameObject;
    }

    private void Update(){


      if(EventSystem.current.currentSelectedGameObject != go){
        Debug.Log("DIFFERENT");
        audioSource.clip = MenuMoveSound;
        audioSource.Play();
        go = EventSystem.current.currentSelectedGameObject;
      }else{
      //  Debug.Log("MEME");
      }

    }
}
