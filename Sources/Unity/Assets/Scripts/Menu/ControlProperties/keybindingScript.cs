using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class keybindingScript : MonoBehaviour
{

    //The most important element => contain list of key to bind with action
    public static PlayerController controller;

    [SerializeField] private GameObject manager;
    
    // Event 

    public static event Action complete;
    public static event Action cancel;

    public static void init(GameObject manager)
    {
        controller = manager.GetComponent<KeybindManager>().AccessController();
    }
    
    public static void StartRebinding(InputAction action, int bindingIndex)
    {
        if (action.bindings[bindingIndex].isComposite)
        {
            if (action.bindings[bindingIndex + 1].isPartOfComposite && (bindingIndex + 1) < action.bindings.Count)
            {
                RebindingKey(action, (bindingIndex+1), true);
            }
        }
        else
        {
            RebindingKey(action, bindingIndex);
        }
        
    }

    private static bool DuplicateBinding(InputAction action, int index, bool composite = false)
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
    
    private static void RebindingKey(InputAction action, int index, bool composite = false)
    {
        action.Disable();
        
        var rebind = action.PerformInteractiveRebinding(index);
        
        //Rebinding operation 
        rebind
            .WithControlsHavingToMatchPath("<Keyboard>")
            .WithControlsHavingToMatchPath("<Mouse>")
            .WithCancelingThrough("<Keyboard>/escape")
            .OnComplete(operation =>
            {
                action.Enable();
                operation.Dispose();

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
                
                complete?.Invoke();
                
            })
            .OnCancel(operation =>
            {
                action.Enable();
                operation.Dispose();
                cancel?.Invoke();
            });
        
        rebind.Start();

    }
}