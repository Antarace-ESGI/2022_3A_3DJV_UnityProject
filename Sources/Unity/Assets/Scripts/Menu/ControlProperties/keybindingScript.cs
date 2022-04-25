using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class keybindingScript : MonoBehaviour
{

    //The most important element => contain list of key to bind with action
    public static PlayerController controller;
    
    // Public
    [Header("Manager")]
    [SerializeField] private GameObject loader;
    
    [Header("Input")]
    [SerializeField] private InputActionReference inputActionReference;
    
    [Header("Neutral")]
    [SerializeField] private Text actionText;
    [SerializeField] private Button rebindButton;
    
    [Header("OnRebind")]
    [SerializeField] private Text rebindText;
    [SerializeField] private GameObject rebindPanel;
    
    [Header("Device")] 
    [SerializeField] [Range(0,1)] private int _index;
    
    private void Awake()
    {
        controller ??= new PlayerController();

        _index = 0;
        
        if (loader.GetComponent<KeybindManager>().GetController() != null)
            controller = loader.GetComponent<KeybindManager>().GetController();
        
    }

    private void OnEnable()
    {
        InputSystem.onDeviceChange += OnInputDeviceChange;
        if (Gamepad.current != null)
            OnGamepad();
        DisplayBindingUI();
    }
    
    private void OnInputDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Added:
                OnGamepad();
                UpdateUI();
                break;
            
            case InputDeviceChange.Disconnected:
                
                _index = 0;
                UpdateUI();
                
                break;
        }
    }

    private void OnGamepad()
    {
        if (controller.asset.FindAction(inputActionReference.action.name).bindings[0].isComposite)
            _index += controller.asset.FindAction(inputActionReference.action.name).bindings.Count - 1;
        else
            _index = 1;
    }

    // UI
    
    public void DisplayBindingUI()
    {
        if (inputActionReference.action != null)
        {
            actionText.text = inputActionReference.action.name;
            rebindButton.GetComponentInChildren<Text>().text = controller.asset.FindAction(inputActionReference.action.name).GetBindingDisplayString(_index);
            rebindButton.onClick.AddListener(StartRebinding);
        }
    }
    
    
    public void UpdateUI()
    {
        rebindButton.GetComponentInChildren<Text>().text = controller.asset.FindAction(inputActionReference.action.name).GetBindingDisplayString(_index);
    }
    
    public void UpdateUI(String str)
    {
        rebindButton.GetComponentInChildren<Text>().text = str;
    }
    
    // Logic part

    public InputActionReference GetInputReference()
    {
        return inputActionReference;
    }
    
    public void StartRebinding()
    {
        InputAction action = controller.asset.FindAction(inputActionReference.action.name);
        
        if (action.bindings[_index].isComposite)
        {
            if (action.bindings[_index + 1].isPartOfComposite && (_index + 1) < action.bindings.Count)
            {
                RebindingKey(action, (_index+1), true);
            }
        }
        else
        {
            RebindingKey(action, _index);
        }
        
    }

    private bool DuplicateBinding(InputAction action, int index, bool composite = false)
    {
        if (!composite)
        {
            foreach (InputAction effectiveAction in controller)
            {
                if(effectiveAction.bindings[index].effectivePath == action.bindings[index].effectivePath && effectiveAction != action)
                    return true;
            }
        }
        else
        {
            for (int i = 1; i < action.bindings.Count && action.bindings[i].isPartOfComposite; i++)
            {
                if (action.bindings[i].effectivePath == action.bindings[index].effectivePath)
                    return true;
            }
        }
        
        return false;
    }
    
    private void RebindingKey(InputAction action, int index, bool composite = false)
    {
        
        rebindPanel.SetActive(true);
        rebindText.text = $"Press any key{inputActionReference.action.expectedControlType}";
        
        action.Disable();
        
        var rebind = action.PerformInteractiveRebinding(index);

        // Initialize rebind parameters:
        
        if (index > 0)
        {
            rebind
                .WithControlsExcluding("<>Mouse")
                .WithControlsExcluding("<Keyboard>")
                .WithControlsHavingToMatchPath("<PS4 Controller>")
                .WithCancelingThrough("<PS4 Controller>/options");
        }
        else
        {
            rebind
                .WithControlsHavingToMatchPath("<Keyboard>")
                .WithControlsHavingToMatchPath("<Mouse>")
                .WithCancelingThrough("<Keyboard>/escape");
        }

        //Perform rebinding operation 
        
        rebind.OnComplete(operation =>
            {
                action.Enable();
                operation.Dispose();
                rebindPanel.SetActive(false);

                if (DuplicateBinding(action,index,composite))
                {
                    action.RemoveBindingOverride(index);
                    RebindingKey(action,index,composite);
                }
                
                
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
}