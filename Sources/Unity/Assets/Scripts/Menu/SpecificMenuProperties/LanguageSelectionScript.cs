using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSelectionScript : MonoBehaviour
{
    [SerializeField] private Text[] texts;
    [SerializeField] private string language;
    
    public void TranslateAll()
    {
        PlayerPrefs.SetString("Language", language);

        if (texts.Length > 0) 
            ListUpdate();

    }

    private void ListUpdate()
    {
        foreach (Text text in texts)
        {
            text.GetComponent<TranslateSelector>().TranslateText(language);
        }
    }
}
