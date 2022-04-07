using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class keybindingLabelUIScript : MonoBehaviour
{
   
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
    
    private void Awake()
    {
        controller ??= new PlayerController();
    }
    
    #if UNITY_EDITOR
        
        private void OnValidate()
        {
            DisplayBindingUI();
        }
        
    #endif

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

    private void UpdateUI()
    {
        rebindButton.GetComponentInChildren<Text>().text = inputActionReference.action.GetBindingDisplayString(GetIndex());
    }

    private void DisplayBindingUI()
    {
        if (inputActionReference.action != null)
        {
            actionText.text = inputActionReference.action.name;
        }
        
        rebindButton.GetComponentInChildren<Text>().text = inputActionReference.action.GetBindingDisplayString(GetIndex());
        rebindButton.onClick.AddListener(StartRebinding);
    }

    private int GetIndex()
    {
        return 0;
    }

    private void StartRebinding()
    {
        rebindPanel.SetActive(true);
        rebindText.text = $"Press any key{inputActionReference.action.expectedControlType}";
        keybindingScript.StartRebinding(inputActionReference.action,GetIndex());
    }
    
}
