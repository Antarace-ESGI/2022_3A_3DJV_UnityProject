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
    [SerializeField] [Range(0,1)] private int _index;
    
    private void Awake()
    {
        _index = 0;
        if (Gamepad.current != null)
            _index = 1;
        LoadPersonalBinding();
    }

    private void OnEnable()
    {
        InputSystem.onDeviceChange += OnInputDeviceChange;
    }

    public PlayerController GetController()
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

    private void LoadPersonalBinding()
    {
        String path = $"{Application.dataPath}/{"keybind"}.txt";
        if (File.Exists(path))
        {
            _controller = new PlayerController();
            Dictionary<string, string> keys = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(path));
            
            foreach (InputAction action in _controller)
            {
                if(keys.ContainsKey(action.actionMap+action.name))
                    action.ApplyBindingOverride(_index,keys[action.actionMap+action.name]);
            }
            
        }
    }
}
