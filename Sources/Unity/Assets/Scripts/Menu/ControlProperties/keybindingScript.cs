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

        DEBUG(inputActionReference.action.GetBindingDisplayString(bindingIndex));
        rebindPanel.SetActive(true);
        rebindText.text = $"Press any key{inputActionReference.action.expectedControlType}";

        InputAction action = _controller.asset.FindAction(inputActionReference.action.name);

        action.Disable();
        
        var rebind = action.PerformInteractiveRebinding(bindingIndex);
        
        //Rebinding operation 
        rebind
            .WithControlsHavingToMatchPath("<Keyboard>")
            .WithCancelingThrough("<Keyboard>/escape")
            .OnComplete(operation =>
            {
                action.Enable();
                operation.Dispose();
                
                rebindPanel.SetActive(false);
                DEBUG("Rebind complete !");
                
                DEBUG(action.bindings[0].ToString());
                rebindButton.GetComponentInChildren<Text>().text = inputActionReference.action.GetBindingDisplayString(bindingIndex);
            })
            .OnCancel(operation =>
            {
                action.Enable();
                operation.Dispose();
                rebindPanel.SetActive(false);
            });
        
        rebind.Start();

        
    }
    
    // DEBUG

    private void DEBUG(string message)
    {
        Debug.Log(message);
    }
    
    
}
