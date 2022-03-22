using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class keybindingMenuUIScript : MonoBehaviour
{
    [Serializable]
    public class Keybind
    {
        public InputActionMap actionMap;
        public string actionName;
        public string actionPath;

        public void SetKeybind(InputActionMap action, string name, string path)
        {
            actionMap = action;
            actionName = name;
            actionPath = path;
        }

        public override string ToString()
        {
            return $"{actionMap} -> {actionName} have path = {actionPath}";
        }
    }
    
    private PlayerController _controller;
    
    private int _index = 0;
    
    public List<InputActionReference> keybindings = new List<InputActionReference>();
    

    private void Awake()
    {
         _controller ??= new PlayerController();
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
        
        List<Keybind> keys = new List<Keybind>();
        
        String path =  $"{Application.dataPath}/{"keybind"}.txt";
        
        Keybind key = new Keybind();
        
        foreach (InputActionReference i in keybindings)
        {
            action = InitInputAction(i);
            key.SetKeybind(action.actionMap,action.name,action.bindings[bindingIndex].path);
            keys.Add(key);
        }
        
        // writing 
        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.BaseStream.Seek(0, SeekOrigin.Begin);
            String json = JsonUtility.ToJson(keys);
            keys = JsonUtility.FromJson<List<Keybind>>(json);
            
            
            
            Debug.Log(keys.Count);
            sw.Write(json);
        }
        
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
            
            action.Enable();
        }
    }

    
    // TODO : Load the personal binding of the user when the game launch
    private void LoadPersonalBinding()
    {
        
    }
    
}
