using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{

    public static PauseScript Instance { get; set; }

    public GameObject CanvasPauseMenuUI;

    public GameObject diary;
    public GameObject shop;
    public GameObject chess;
    public GameObject dialogue;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CanvasPauseMenuUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }

    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    public void Resume() 
    {
        CanvasPauseMenuUI.SetActive(false);
        if(!(shop.activeSelf || dialogue.activeSelf))
            Time.timeScale = 1;
        if(!(diary.activeSelf || shop.activeSelf || chess.activeSelf || dialogue.activeSelf || InventorySystem.Instance.isActiveAndEnabled))
            Cursor.lockState = CursorLockMode.Locked;
        if(!(diary.activeSelf && shop.activeSelf && chess.activeSelf && dialogue.activeSelf && InventorySystem.Instance.isActiveAndEnabled))
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

}
