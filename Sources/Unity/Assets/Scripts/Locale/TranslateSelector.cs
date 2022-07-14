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

    private static string _language = "fr";
    private static Dictionary<string, Dictionary<string, string>> _translation;
    private static bool _loaded;

    private void OnEnable()
    {
        LoadTranslations();
        UpdateTranslation();
        TranslateText();
    }

    public static void RefreshEnabledTexts()
    {
        var texts = FindObjectsOfType<TranslateSelector>();

        foreach (var text in texts)
        {
            text.TranslateText();
        }
    }

    private void TranslateText()
    {
        GetComponent<Text>().text = _translation[_language][translationKey];
    }

    public static string GetTranslation(string key)
    {
        return _translation[_language][key];
    }

    private static void LoadTranslations()
    {
        if (_loaded) return;

        var path = $"{Application.dataPath}/translation.json";
        var rawJson = File.ReadAllText(path);

        _translation ??= JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(rawJson);
        _loaded = true;
    }

    public static void UpdateTranslation()
    {
        if (PlayerPrefs.HasKey("Language"))
        {
            _language = PlayerPrefs.GetString("Language");
        }
    }
}