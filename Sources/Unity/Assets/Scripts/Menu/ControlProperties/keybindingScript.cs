using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class keybindingScript : MonoBehaviour
{

    //The most important element => contain list of key to bind with action
    public static PlayerController controller;
    
    // Private
    
    private int bindingIndex = 0; // Must be contains between 0 && Number of binding expect for composite (Number of binding * 4)
    private String actionName;
    
    // Public
    
    public InputActionReference inputActionReference;
    
    [Header("UI")]
    public Text actionText;
    public Button rebindButton;
    public Text rebindText;
    public GameObject rebindPanel;
    
    private void Awake()
    {
        controller ??= new PlayerController();
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
        }
        rebindButton.GetComponentInChildren<Text>().text = inputActionReference.action.GetBindingDisplayString(bindingIndex);
        rebindButton.onClick.AddListener(StartRebinding);
    }


    public void StartRebinding()
    {
        InputAction action = controller.asset.FindAction(inputActionReference.action.name);
        
        if (action.bindings[bindingIndex].isComposite)
        {
            if (action.bindings[bindingIndex + 1].isPartOfComposite && (bindingIndex + 1) < action.bindings.Count)
            {
                RebindingKey(action, (bindingIndex+1), true);
            }
        }
        else
        {
            RebindingKey(action, bindingIndex);
        }
        
    }
    
    private void RebindingKey(InputAction action, int index, bool composite = false)
    {
        
        rebindPanel.SetActive(true);
        rebindText.text = $"Press any key{inputActionReference.action.expectedControlType}";
        
        action.Disable();
        
        var rebind = action.PerformInteractiveRebinding(index);
        
        //Rebinding operation 
        rebind
            .WithControlsHavingToMatchPath("<Keyboard>")
            .WithControlsHavingToMatchPath("<Mouse>")
            .WithCancelingThrough("<Keyboard>/escape")
            .OnComplete(operation =>
            {
                action.Enable();
                operation.Dispose();
                rebindPanel.SetActive(false);
                
                
                if (composite)
                {
                    var nextBindingIndex = index + 1;
                    if (nextBindingIndex < action.bindings.Count && action.bindings[nextBindingIndex].isPartOfComposite)
                        RebindingKey(action, nextBindingIndex, true);
                }
                
                rebindButton.GetComponentInChildren<Text>().text = action.GetBindingDisplayString(index);
            })
            .OnCancel(operation =>
            {
                action.Enable();
                operation.Dispose();
                rebindPanel.SetActive(false);
            });
        
        rebind.Start();

        
    }

    
    // TODO : Create a file to store every new binding on click event 
    private void SaveBinding()
    {
    }

    // TODO : Create a reset function
    
    private void ResetOriginalBinding()
    {
        
    }

    
    // TODO : Load the personal binding of the user when the game launch
    private void LoadPersonalBinding()
    {
        
    }
    
}
