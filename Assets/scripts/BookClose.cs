using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookClose : MonoBehaviour
{
    [SerializeField] GameObject pages;
    [SerializeField] GameObject hint;
    [SerializeField] GameObject commands;

    public void CloseBook()
    {
        pages.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        InventorySystem.Instance.isOpen = false;
    }

    public void CloseHint()
    {
        hint.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        InventorySystem.Instance.isOpen = false;
    }

    public void CloseCommands()
    {
        commands.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        InventorySystem.Instance.isOpen = false;
    }
}
