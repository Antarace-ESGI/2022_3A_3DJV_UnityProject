using UnityEngine;

public class LanguageSelectionScript : MonoBehaviour
{
    [SerializeField] private string language;

    public void TranslateAll()
    {
        PlayerPrefs.SetString("Language", language);

        TranslateSelector.UpdateTranslation();
        TranslateSelector.RefreshEnabledTexts();
    }
}
