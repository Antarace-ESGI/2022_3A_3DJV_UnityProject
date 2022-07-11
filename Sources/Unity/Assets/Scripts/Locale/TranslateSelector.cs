using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TreeEditor;
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

    private Dictionary<string, Dictionary<string, string>> translation;

    void Start()
    {
        var path =  $"{Application.dataPath}/translation.json";

        var rawJson = File.ReadAllText(path);
        
        translation ??= JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(rawJson);
        
        _translation = translation;

        string lang = PlayerPrefs.GetString("Language");
        if(lang == "")
            GetComponent<Text>().text = _translation[Language][translationKey];
        else 
            GetComponent<Text>().text = _translation[lang][translationKey];
    }
    
    private void OnEnable()
    {
        string translate = PlayerPrefs.GetString("Language");
        
        if (translate != "" && translation != null) 
            TranslateText(translate);
    }

    public void TranslateText(string language)
    {
        GetComponent<Text>().text = translation[language][translationKey];
    }

    public static string GetTranslation(string key)
    {
        return _translation[Language][key];
    }
}
