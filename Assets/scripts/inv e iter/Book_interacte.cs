using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book_interacte : MonoBehaviour, IInteractable
{

    public InventoryManager inventoryManager;
    [SerializeField] GameObject CanvasShop;
    [SerializeField] GameObject item;
    [SerializeField] GameObject MainCamera;
    [SerializeField] GameObject ChessCamera;
    [SerializeField] GameObject pointer;
    [SerializeField] GameObject gamemaster;
    [SerializeField] GameObject chessboard;
  
    public void Interact()
    {
        if(item.name == "shop")
        {
            CanvasShop.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else if(item.name == "chessboard")
        {
            MainCamera.SetActive(false);
            MainCamera.SetActive(false);
            pointer.SetActive(false);
            ChessCamera.SetActive(true);
            gamemaster.SetActive(true);
            chessboard.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Debug.Log("interacting");
            inventoryManager.AddItem(gameObject.name);
            Destroy(gameObject, 0.1f);
        }
        
    }

    public void CloseChess()
    {
        MainCamera.SetActive(true);
        MainCamera.SetActive(true);
        pointer.SetActive(true);
        ChessCamera.SetActive(false);
        gamemaster.SetActive(false);
        chessboard.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
