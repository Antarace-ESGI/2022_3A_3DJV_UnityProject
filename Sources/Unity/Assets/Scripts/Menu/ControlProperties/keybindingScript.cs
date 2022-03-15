using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class keybindingScript : MonoBehaviour
{
    private PlayerController _controller;
    
    [SerializeField]
    private int bindingIndex = 0;

    private String actionName;
    private InputActionReference inputActionReference;
    
    public Text actionText;
    public Button rebindButton;
    public Text rebindText;
    
    private void Awake()
    {
        _controller = new PlayerController();
    }
    
    private void OnValidate()
    {
        if (inputActionReference.action != null)
        {
            actionName = inputActionReference.action.name;
        }
        
        if (actionText != null)
        {
            actionText.text = actionName;
        }

        if (rebindText != null)
        {
            rebindText.text = inputActionReference.action.GetBindingDisplayString(bindingIndex);
        }
    }
}
