using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class keybindingLabelUIScript : MonoBehaviour
{

    [SerializeField] private GameObject accessor;
    private PlayerController controller;
    
    // Public
    
    [Header("Input System")]
    public InputActionReference inputActionReference;
    
    [Header("OnStay")]
    public Text actionText;
    public Button rebindButton;
    
    [Header("OnRebind")]
    public Text rebindText;
    public GameObject rebindPanel;
    
    private void Start()
    {
        controller = accessor.GetComponent<KeybindManager>().AccessController();
        keybindingScript.init(accessor);
        DisplayBindingUI();
    }
    
    
    private void OnEnable()
    {
        keybindingScript.complete += DeselectPanel;
        keybindingScript.complete += UpdateUI;
    }

    private void OnDisable()
    {
        keybindingScript.cancel -= DeselectPanel;
        keybindingScript.cancel -= UpdateUI;
    }
    
    
    private void DeselectPanel()
    {
        rebindPanel.SetActive(false);
    }

    public void UpdateUI()
    {
        rebindButton.GetComponentInChildren<Text>().text = inputActionReference.action.GetBindingDisplayString(GetIndex());
    }

    public void UpdateUI(String str)
    {
        rebindButton.GetComponentInChildren<Text>().text = str;
    }

    public InputActionReference GetInputActionRef()
    {
        return inputActionReference;
    }

    private void DisplayBindingUI()
    {
        if (inputActionReference.action != null && controller != null)
        {
            actionText.text = inputActionReference.action.name;
            rebindButton.GetComponentInChildren<Text>().text =  controller.asset[inputActionReference.action.name].GetBindingDisplayString(GetIndex());
            rebindButton.onClick.AddListener(StartRebinding);
        }
    }

    private int GetIndex()
    {
        return 0;
    }

    private void StartRebinding()
    {
        rebindPanel.SetActive(true);
        rebindText.text = $"Press any key{controller.asset[inputActionReference.action.name].expectedControlType}";
        keybindingScript.StartRebinding(inputActionReference.action,GetIndex());
    }
    
}
