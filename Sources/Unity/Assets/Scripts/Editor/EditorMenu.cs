using UnityEditor;
using UnityEngine;

public class EditorMenu : EditorWindow
{
    [MenuItem("Window/Generator")]
    public static void ExecButton()
    {
        GetWindow<EditorMenu>("Generateur");
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Generateur de Maps");

        // Variables :

        if (GUILayout.Button("Generate"))
        {
            Debug.Log("Afficher une map");
        }
        
        EditorGUILayout.EndVertical();
    }
}
