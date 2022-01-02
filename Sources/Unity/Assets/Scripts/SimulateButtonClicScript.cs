using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulateButtonClicScript : MonoBehaviour
{

    private Button button;
    
    void Update()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            if (Input.GetButtonDown("CrossButton"))
            {
                button = GetComponent<Button>();
                button.onClick.Invoke();
            }
        }
    }
}
