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
    
    [Serializable]
    public class Keybinds
    {
        public List<Keybind> bindings = new List<Keybind>();

        public override string ToString()
        {
            string str = "";

            for (int i = 0; i < bindings.Count; i++)
            {
                str += $"{bindings[i].ToString()}\n";
            }

            if (str.Length > 0)
                return str;

            return "No element in bindings";
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
    
    public void SaveBinding()
    {
        int bindingIndex = 0;
        
        String path =  $"{Application.dataPath}/{"keybind"}.txt";

        Keybinds keys = new Keybinds();
        
        foreach (InputAction action in _controller)
        {
            Keybind key = new Keybind();
            
            if(action.bindings[bindingIndex].overridePath != null)
                key.SetKeybind(action.actionMap,action.name,action.bindings[bindingIndex].overridePath);
            else
                key.SetKeybind(action.actionMap,action.name,action.bindings[bindingIndex].path);
            
            keys.bindings.Add(key);
        }

        // writing 
        
        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.BaseStream.Seek(0, SeekOrigin.Begin);
            String json = JsonUtility.ToJson(keys);
            sw.Write(json);
        }
        
    }
    
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
    public void LoadPersonalBinding()
    {
        String path = $"{Application.dataPath}/{"keybind"}.txt";
        if (File.Exists(path))
        {
            Keybinds keys = new Keybinds();
            keys = JsonUtility.FromJson<Keybinds>(File.ReadAllText(path));

            foreach (Keybind key in keys.bindings)
            {
                Debug.Log(key.ToString());
            }
        }
    }
    
}
