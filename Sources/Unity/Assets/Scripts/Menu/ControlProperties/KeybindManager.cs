using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeybindManager : MonoBehaviour
{

    private PlayerController _controller;

    public InputActionReference tmp;
    
    private void Awake()
    {
        _controller = new PlayerController();
        LoadPersonalBinding();
        //debug();
        Help();
    }

    public PlayerController AccessController()
    {
        return _controller;
    }

    // TODO : Check if there's a controller plug in the PC
    private int GetIndex()
    {
        return 0;
    }
    
    private void LoadPersonalBinding()
    {
        String path = $"{Application.dataPath}/{"keybind"}.txt";
        if (File.Exists(path))
        {
            Dictionary<string, string> keys = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(path));

            foreach (InputAction action in _controller)
            {
                action.ApplyBindingOverride(GetIndex(),keys[action.actionMap+action.name]);
            }
            
        }
    }

    
    // DEBUG
    
    private void debug()
    {
        foreach (var action in _controller)
        {
            Debug.Log(action.bindings[GetIndex()]);
        }
    }

    public void Help()
    {
         Debug.Log(_controller.asset[tmp.action.name].GetBindingDisplayString(GetIndex()));
    }
    
}
