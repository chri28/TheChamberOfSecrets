using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DialogueMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI textComponent;

    [Header("Dialogue Settings")]
    public LocalizedString[] localizedLines; // Array di LocalizedString per i dialoghi
    public float textSpeed = 0.05f;

    private int index;
    private string currentLine;

    [SerializeField] GameObject canvas;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
            canvas.SetActive(false);

        }
    }
}
