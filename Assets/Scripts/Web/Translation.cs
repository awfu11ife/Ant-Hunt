using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translation : MonoBehaviour
{
    private const string DefaultLanguage = "English";

    private Dictionary<string, string> _languages = new Dictionary<string, string>()
    {
        { "ru", "Russian" },
        { "tr", "Turkish" },
        { "en", "English" }
    };


    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize();
        SetLanguage(YandexGamesSdk.Environment.i18n.lang);
    }

    private void SetLanguage(string languageFormat)
    {
        if (_languages.ContainsKey(languageFormat))
        {
            Lean.Localization.LeanLocalization.SetCurrentLanguageAll(_languages[languageFormat]);
        }
        else
        {
            Lean.Localization.LeanLocalization.SetCurrentLanguageAll(DefaultLanguage);
        }

    }
}
