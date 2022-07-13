using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class EditorGenerator : EditorWindow
{
    
    struct GeneratorData
    {
        public int length;
        public int max;
        public int min;
        public int height;
    }

    private Object _lastGameObject;

    [MenuItem("Tools/Generator")]
    public static void ExecButton()
    {
        GetWindow<EditorGenerator>("Generateur");
    }

    private void OnGUI()
    {
        GeneratorData data = new GeneratorData();
        // Set default values:
        data.min = 0;
        data.max = 0;
        data.length = 1;
        DrawGUI(data);
    }

    private string _blenderExecutable;

    private void DrawGUI(GeneratorData data)
    {
        // Main structure : 
        EditorGUILayout.BeginVertical();
        
        // Length
        GUILayout.Label("Longueur du circuit : ");
        data.length = EditorGUILayout.IntField(data.length);
        
        // Min / Max
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Valeur min : ");
        
        data.min = EditorGUILayout.IntField(data.min);
        GUILayout.Label("Valeur max : ");
        
        data.max = EditorGUILayout.IntField(data.max);
        EditorGUILayout.EndHorizontal();
        // Height
        
        GUILayout.Label("Hauteur max du circuit");
        data.height = EditorGUILayout.IntField(data.height);

        EditorGUILayout.LabelField("Sélectionner Blender");
        if (GUILayout.Button("Sélectionner Blender"))
        {
            _blenderExecutable = EditorUtility.OpenFilePanel("Sélectionner Blender", "/", "exe");
        }
        GUILayout.Label(_blenderExecutable?.Length > 0 ? _blenderExecutable : "null");

        // Button
        EditorGUILayout.LabelField("Generateur de Maps");
        if (GUILayout.Button("Generate"))
        {
            LaunchProcess(data);
        }

        EditorGUILayout.EndVertical();
    }
    
    private void LaunchProcess(GeneratorData data)
    {
        const string fileName = "Assets/Editor/map.fbx";
        var pwd = Directory.GetCurrentDirectory() + "/" + fileName;

        Debug.Log(data.min.ToString());
        
        if (_blenderExecutable != "")
        {
            // Passer les chemins en absolu via os.system()
            var info = new ProcessStartInfo
            {
                FileName = _blenderExecutable,
                Arguments = "--background --python \"../Blender/random_map_generator.py\"",
                EnvironmentVariables =
                {
                    {"OUTPUT_PATH", pwd},
                    {"MIN_X",data.min.ToString()},
                    {"MIN_Y",data.max.ToString()},
                    {"TRACK_LENGTH",data.length.ToString()}
                },
                UseShellExecute = false,
            };

            var process = new Process
            {
                StartInfo = info,
            };

            process.Start();
            process.WaitForExit();
            process.Close();

            // Add game object to scene
            AssetDatabase.Refresh();
            
            var gameObject = AssetDatabase.LoadAssetAtPath(fileName, typeof(GameObject));
            _lastGameObject = Instantiate(gameObject);
        }
    }
}
