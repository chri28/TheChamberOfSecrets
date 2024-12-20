using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChessUIManager : MonoBehaviour
{
    [SerializeField] private GameObject UIParent;
    [SerializeField] private Text ResultText;
    [SerializeField] private TextMeshProUGUI language;
    [SerializeField] private Button button;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject DialogueVictory;


    public void HideUI()
    {
        UIParent.SetActive(false);

    }

    public void OnGameFinished(string winner)
    {
        UIParent.SetActive(true);
        if(winner == "White")
        {
            if(language.text == "Chiudi")
                ResultText.text = string.Format("Scacco Matto");
            else
                ResultText.text = string.Format("Check Mate");

            button.interactable = false;
            key.SetActive(true);
            DialogueVictory.SetActive(true);
        }
    }

}
