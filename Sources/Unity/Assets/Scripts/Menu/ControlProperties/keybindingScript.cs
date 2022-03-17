using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class keybindingScript : MonoBehaviour
{
    public static PlayerController controller;

    [SerializeField] [Range(0,1)] private int bindingIndex = 0; 
    
    public InputActionReference inputActionReference;

    public Text actionText;
    public Button rebindButton;
    public Text rebindText;
    public GameObject rebindPanel;
    
    private String actionName;
    
    private void Awake()
    {
        if(controller == null)
            controller = new PlayerController();
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
                
                DEBUG("Rebind complete !");
                
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

    // DEBUG

    private void DEBUG(string message)
    {
        Debug.Log(message);
    }
    
    
}
