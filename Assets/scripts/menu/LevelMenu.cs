using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;

    private void Awake()
    {
        // Ottiene il livello sbloccato dai PlayerPrefs o imposta a 1 di default
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Disabilita tutti i bottoni inizialmente
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        // Abilita i bottoni fino al livello sbloccato
        for (int i = 0; i < Mathf.Min(unlockedLevel, buttons.Length); i++)
        {
            buttons[i].interactable = true;
        }

    }

    public void OpenLevel(int level)
    {
        string levelName = "level" + level;
        SceneManager.LoadScene(levelName);
    }

    public void ResetLevels()
    {
        // Sblocca tutti i bottoni temporaneamente
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        // Resetta PlayerPrefs per impostare nuovamente solo il livello 1 sbloccato al riavvio
        PlayerPrefs.SetInt("UnlockedLevel", 1);
        PlayerPrefs.Save();
    }

    public void UnlockNextLevel(int currentLevel)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Se il livello successivo è maggiore di quello attualmente sbloccato, aggiorna PlayerPrefs
        if (currentLevel + 1 > unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", currentLevel + 1);
            PlayerPrefs.Save();
        }
    }
}
