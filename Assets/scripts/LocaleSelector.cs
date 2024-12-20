using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using TMPro;
public class LocaleSelector : MonoBehaviour
{
    private bool active = false;
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private GameObject settingsIta;
    [SerializeField] private GameObject settingsEng;
    [SerializeField] private GameObject levelIta;
    [SerializeField] private GameObject levelEng;
    [SerializeField] private GameObject pauseIta;
    [SerializeField] private GameObject pauseEng;
    [SerializeField] private GameObject shopIta;
    [SerializeField] private GameObject ShopEng;

    public void ChangeLocale()
    {
        int value = dropdown.value;
        if (active)
            return;
        StartCoroutine(SetLocale(value));

        if (value == 0)
        {
            settingsIta.SetActive(true);
            settingsEng.SetActive(false);
            levelIta.SetActive(true);
            levelEng.SetActive(false);
            pauseIta.SetActive(true);
            pauseEng.SetActive(false);
            shopIta.SetActive(true);
            ShopEng.SetActive(false);
        }
        else 
        {
            settingsIta.SetActive(false);
            settingsEng.SetActive(true);
            levelIta.SetActive(false);
            levelEng.SetActive(true);
            pauseIta.SetActive(false);
            pauseEng.SetActive(true);
            shopIta.SetActive(false);
            ShopEng.SetActive(true);
        }

    }

    IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        active = false;
    }
}
