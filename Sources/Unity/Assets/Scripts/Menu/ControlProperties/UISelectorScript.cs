using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class UISelectorScript : MonoBehaviour
{
    // Just a script to set select button for the Gamepad option


    private void OnEnable()
    {
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(gameObject);
        }

        if (MultiplayerEventSystem.current != null)
        {
            MultiplayerEventSystem.current.SetSelectedGameObject(null);
            MultiplayerEventSystem.current.SetSelectedGameObject(gameObject);
        }
    }
}
