using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmp : MonoBehaviour
{

}
/*
 *public class keybindingMenuUIScript : MonoBehaviour
{
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

        Dictionary<string, string> bindings = new Dictionary<string, string>();

        foreach (InputAction action in _controller)
        {
            if(action.bindings[bindingIndex].overridePath != null)
                bindings.Add(action.actionMap+action.name,action.bindings[bindingIndex].overridePath);
            else
                bindings.Add(action.actionMap+action.name,action.bindings[bindingIndex].path);
        }
        
        // writing 

        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.BaseStream.Seek(0, SeekOrigin.Begin);
            string json = JsonConvert.SerializeObject(bindings);
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
    public void LoadPersonalBinding()
    {
        String path = $"{Application.dataPath}/{"keybind"}.txt";
        if (File.Exists(path))
        {
            Dictionary<string, string> keys = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(path));

            foreach (InputAction action in _controller)
            {
                action.ApplyBindingOverride(_index,keys[action.actionMap+action.name]);
            }
            
        }
    }
    
}

 * 
 */