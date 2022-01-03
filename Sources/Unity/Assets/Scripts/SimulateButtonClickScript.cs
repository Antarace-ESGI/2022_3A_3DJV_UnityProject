using UnityEngine;
using UnityEngine.UI;

public class SimulateButtonClickScript : MonoBehaviour
{
    
    //temporary
    public Button button;
    
    void Update()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            if (Input.GetButtonDown("CrossButton"))
            {
                
                button.onClick.Invoke();

            }
        }
    }
}
