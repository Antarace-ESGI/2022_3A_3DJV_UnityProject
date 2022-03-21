using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class keybindingMenuUIScript : MonoBehaviour
{
    [Serializable]
    private class Keybind
    {
        private InputActionMap _actionMap;
        private string _actionName;
        private string _actionPath;

        public void SetKeybind(InputActionMap action, string name, string path)
        {
            _actionMap = action;
            _actionName = name;
            _actionPath = path;
        }
        
    }
    
    
    private PlayerController _controller;
    
    private int _index = 0;
    
    public List<InputActionReference> keybindings = new List<InputActionReference>();

    private void Awake()
    {
         //_controller ??= new PlayerController();
         _controller = keybindingScript.controller;
    }

    private InputAction InitInputAction(InputActionReference inputref)
    {
        if(inputref)
        {
            return _controller.asset.FindAction(inputref.action.name); 
        }
        return null;
    }

    // TODO : Create a file to store every new binding on click event 
    public void SaveBinding()
    {
        InputAction action;
        int bindingIndex = 0;
        
        String path =  $"{Application.dataPath}/{"keybind"}.txt";

        List<Keybind> keys = new List<Keybind>();

        foreach (InputActionReference i in keybindings)
        {
            action = InitInputAction(i);
            Keybind key = new Keybind();
            key.SetKeybind(action.actionMap,action.name,action.bindings[bindingIndex].path);
            keys.Add(key);
        }
        
        // Start writing 
        
        String json = JsonUtility.ToJson(keys);
        File.WriteAllText(path,json);
        
    }

    // [WIP]
    
    public void ResetOriginalBinding()
    {

        InputAction action;
        int bindingIndex = 0;
            
        foreach (InputActionReference i in keybindings)
        {
            action = InitInputAction(i);
            action.Disable();
            
            if (bindingIndex < action.bindings.Count)
            {
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
            }
            
            action.Enable();
        }
    }

    
    // TODO : Load the personal binding of the user when the game launch
    private void LoadPersonalBinding()
    {
        
    }
}
