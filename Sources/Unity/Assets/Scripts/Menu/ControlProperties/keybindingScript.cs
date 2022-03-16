using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class keybindingScript : MonoBehaviour
{
    private PlayerController _controller;

    [SerializeField] [Range(0,1)] private int bindingIndex = 0; 
    [SerializeField] private InputBinding _inputBinding;
    
    
    public InputActionReference inputActionReference;

    public Text actionText;
    public Button rebindButton;
    public Text rebindText;
    public GameObject rebindPanel;
    
    private String actionName;
    
    private void Awake()
    {
        _controller = new PlayerController();
    }
    
    #if UNITY_EDITOR
    
    private void OnValidate()
    {
        DisplayBindingUI();
    }
    
    #endif
    
    public void DisplayBindingUI()
    {
        
        if (inputActionReference.action != null)
        {
            actionText.text = inputActionReference.action.name;
            _inputBinding = inputActionReference.action.bindings[bindingIndex];
        }

        rebindButton.GetComponentInChildren<Text>().text = inputActionReference.action.GetBindingDisplayString(bindingIndex);
        rebindButton.onClick.AddListener(RebindingKey);
        
    
    }
    
    public void RebindingKey()
    {
        rebindPanel.SetActive(true);
        rebindText.text = $"Press any key{inputActionReference.action.expectedControlType}";
        
        InputAction action = new InputAction(inputActionReference.action.name);
        action = inputActionReference.action;

        inputActionReference.action.Disable();
        
        var rebind = action.PerformInteractiveRebinding(bindingIndex);
        
        //Rebinding operation 
        rebind
            .WithControlsHavingToMatchPath("<Keyboard>")
            .WithControlsHavingToMatchPath("<Mouse>")
            .WithBindingGroup("Keyboard")
            .WithCancelingThrough("<Keyboard>/escape")
            .OnComplete(operation =>
            {
                action.Enable();
                rebindPanel.SetActive(false);
                operation.Dispose();
                Debug.Log("Rebind complete !");
                rebindButton.GetComponentInChildren<Text>().text = inputActionReference.action.GetBindingDisplayString(bindingIndex);
            })
            .OnCancel(operation =>
            {
                action.Enable();
                rebindPanel.SetActive(false);
                operation.Dispose();
            });
        
        rebind.Start();

        
    }
    
    
}
