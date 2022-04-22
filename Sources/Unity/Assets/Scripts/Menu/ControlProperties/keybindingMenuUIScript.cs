using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.InputSystem;

public class keybindingMenuUIScript : MonoBehaviour
{
    private PlayerController _controller;
    
    [SerializeField]private int _index = 0;
    [SerializeField] private GameObject[] labels;
    
    private void OnEnable()
    {
         _controller ??= new PlayerController();
         _controller = keybindingScript.controller;

         InputSystem.onDeviceChange += OnInputDeviceChange;

    }

    private InputAction InitInputAction(InputActionReference inputref)
    {
        if(inputref)
        {
            return _controller.asset.FindAction(inputref.action.name); 
        }
        return null;
    }
    
    private void OnInputDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Added:
                _index = 1;
                break;
            case InputDeviceChange.Disconnected:
                _index = 0;
                break;
        }
    }
    
    public void SaveBinding()
    {
        
        String path =  $"{Application.dataPath}/{"keybind"}.txt";

        Dictionary<string, string> bindings = new Dictionary<string, string>();

        foreach (InputAction action in _controller)
        {
            if(action.bindings[_index].overridePath != null)
                bindings.Add(action.actionMap+action.name,action.bindings[_index].overridePath);
            else
                bindings.Add(action.actionMap+action.name,action.bindings[_index].path);
        }
        
        // writing 

        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.BaseStream.Seek(0, SeekOrigin.Begin);
            string json = JsonConvert.SerializeObject(bindings);
            sw.Write(json);
        }
        
    }
    
    public void ResetOriginalBindingUI()
    {

        InputAction action;
        int bindingIndex = _index;

        foreach (GameObject label in labels)
        {
            var keyScript = label.GetComponent<keybindingScript>();

            action = InitInputAction(keyScript.GetInputReference());
            action.Disable();
            
            if (action.bindings[_index].isComposite)
            {
                for (int n = bindingIndex; n < action.bindings.Count && action.bindings[n].isPartOfComposite; n++)
                {
                    action.RemoveBindingOverride(n);
                }
            }
            else
            {
                action.RemoveBindingOverride(bindingIndex);
            }
            
            keyScript.UpdateUI(action.GetBindingDisplayString(bindingIndex));
            action.Enable();
        }
        
    }
}