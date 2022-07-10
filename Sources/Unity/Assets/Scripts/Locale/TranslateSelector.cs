using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TranslateSelector : MonoBehaviour
{
    [Serializable]
    public class Translation
    {
        public Dictionary<string, string> en;
        public Dictionary<string, string> fr;

        public Translation(Dictionary<string, string> en, Dictionary<string, string> fr)
        {
            this.en = en;
            this.fr = fr;
        }
    }
    
    public string translationKey;

    private const string Language = "fr";
    private static Dictionary<string, Dictionary<string, string>> _translation;

    void Start()
    {
        var path =  $"{Application.dataPath}/translation.json";

        var rawJson = File.ReadAllText(path);

        _translation ??= JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(rawJson);

        GetComponent<Text>().text = _translation[Language][translationKey];
    }

    public static string GetTranslation(string key)
    {
        return _translation[Language][key];
    }
}
