using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class keybindingScript : MonoBehaviour
{
    private PlayerController _controller;
    
    [SerializeField]
    private int bindingIndex = 0;

    private String actionName;
    [CanBeNull] public InputActionReference inputActionReference;

    public Text actionText;
    public Button rebindButton;
    public Text rebindText;
    
    private void Awake()
    {
        _controller = new PlayerController();
    }

    private void Start()
    {
        if (inputActionReference)
        {
            if (inputActionReference.action != null)
                actionText.text = inputActionReference.action.name;

            rebindText.text = inputActionReference.action.GetBindingDisplayString(bindingIndex);
            
            rebindButton.onClick.AddListener(RebindingKey);
            
        }
    }
    
    public void RebindingKey()
    {
        if (inputActionReference)
        {

            rebindText.text = $"Press any key{inputActionReference.action.expectedControlType}";
            
            inputActionReference.action.Disable();
            
            var rebind = inputActionReference.action.PerformInteractiveRebinding(bindingIndex);
            
            //Rebinding operation 

            rebind
                .WithControlsHavingToMatchPath("<Keyboard>")
                .WithBindingGroup("Keyboard")
                .WithCancelingThrough("<Keyboard>/escape")
                .OnComplete(operation =>
                {
                    inputActionReference.action.Enable();
                    operation.Dispose();
                })
                .OnCancel(operation =>
                {
                    inputActionReference.action.Enable();
                    operation.Dispose();
                })
                .Start();


        }
    }

    
}
