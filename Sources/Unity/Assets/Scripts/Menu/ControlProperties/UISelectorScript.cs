using UnityEngine;
using UnityEngine.EventSystems;

public class UISelectorScript : MonoBehaviour
{
    // Just a script to set select button for the Gamepad option


    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }
}
