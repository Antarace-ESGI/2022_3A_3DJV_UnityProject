using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class UISelectorScript : MonoBehaviour
{
    // Just a script to set select button for the Gamepad option

    private GameObject go;

    public AudioClip MenuMoveSound;
    public AudioSource audioSource;

    private void OnEnable()
    {
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(gameObject);
            go = EventSystem.current.currentSelectedGameObject;
        }

        if (MultiplayerEventSystem.current != null)
        {
            MultiplayerEventSystem.current.SetSelectedGameObject(null);
            MultiplayerEventSystem.current.SetSelectedGameObject(gameObject);
            go = MultiplayerEventSystem.current.currentSelectedGameObject;
        }
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != go && audioSource != null)
        {
            audioSource.clip = MenuMoveSound;
            audioSource.Play();
            go = EventSystem.current.currentSelectedGameObject;
        }
        else
        {
            //  Debug.Log("MEME");
        }
    }
}