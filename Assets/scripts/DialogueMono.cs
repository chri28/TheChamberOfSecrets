using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DialogueMono : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI textComponent;

    [Header("Dialogue Settings")]
    public LocalizedString[] localizedLines; // Array di LocalizedString per i dialoghi
    public float textSpeed = 0.05f;

    private int index;
    private string currentLine;
    public GameObject PauseMenu;
    [SerializeField] GameObject CanvasDialogue;
    [SerializeField] GameObject CanvasHint;
    [SerializeField] GameObject CanvasShop;
    [SerializeField] GameObject CanvasVictory;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !PauseMenu.activeSelf)
        {
            if (textComponent.text == currentLine)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = currentLine;
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(LoadAndDisplayLine());
    }

    IEnumerator LoadAndDisplayLine()
    {
        // Carica la stringa localizzata
        AsyncOperationHandle<string> handle = localizedLines[index].GetLocalizedStringAsync();
        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            currentLine = handle.Result;
            textComponent.text = string.Empty;
            StartCoroutine(Typeline());
        }
        else
        {
            Debug.LogError("Errore nel caricamento della stringa localizzata.");
        }
    }

    IEnumerator Typeline()
    {
        foreach (char c in currentLine.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < localizedLines.Length - 1)
        {
            index++;
            StartCoroutine(LoadAndDisplayLine());
        }
        else
        {
            // Fine dei dialoghi
            if (CanvasShop.activeSelf)
            {
                CanvasHint.SetActive(false);
            }
            if (!CanvasShop.activeSelf)
            {
                CanvasDialogue.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            }

            if (CanvasVictory.activeSelf)
            {
                CanvasVictory.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
            }
                

        }
    }
}
