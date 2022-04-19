using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class KeybindManager : MonoBehaviour
{

    private PlayerController _controller;
    private int _index;
    
    private void Awake()
    {
        _controller = new PlayerController();
        _index = 0;
        LoadPersonalBinding();
    }

    private void Start()
    {
        InputSystem.onDeviceChange += OnInputDeviceChange;
    }

    public PlayerController AccessController()
    {
        return _controller;
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
        Debug.Log(_index);
    }
    
    private int GetIndex()
    {
        return _index;
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
}
