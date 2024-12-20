using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;
//using System;
//using TMPro;
//using UnityEngine.Networking;
//using System.Text.Json;
//using System.Globalization;
//using Unity.VisualScripting;
using System.Linq;



public class ShopManagerScript : MonoBehaviour
{
    public int [,] shopOggetti = new int[4,4];
    public Text testoMoneta;
    public int moneta;
    InventoryItem newItem = new InventoryItem();

    [SerializeField] GameObject hint;
    [SerializeField] GameObject hintDialogue;
    public Timer timer;

    void Start()
    {    
        moneta = GlobalUserData.Coins;
        testoMoneta.text = ":" + moneta.ToString();
        //Id
        shopOggetti[1,1] = 1; //aiuto
        shopOggetti[1,2] = 2; //tempo
        //Prezzo
        shopOggetti[2,1] = 10; 
        shopOggetti[2,2] = 20;
        //Quantità
        shopOggetti[3,1] =verificaquantita(1);  
        shopOggetti[3,2] =verificaquantita(2);  
    }

    public void Compra()
    { 
       GlobalUserData.flag=true;
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        if(moneta >= shopOggetti[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            moneta = moneta - shopOggetti[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            GlobalUserData.Coins=moneta;
            
            if( ButtonRef.GetComponent<ButtonInfo>().ItemID == 1)
            {
                if (hint.activeSelf)
                {
                    hintDialogue.SetActive(true);
                    moneta = moneta + shopOggetti[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
                }
                else
                {
                    shopOggetti[3,1]=shopOggetti[3,1]+1;
                    hint.SetActive(true);
                }
                
                newItem = new InventoryItem("Aiuto", shopOggetti[3,1]); // Nome: aiuto
                var existingItem = GlobalUserData.Inventory.FirstOrDefault(item => item.itemName == newItem.itemName);
                if (existingItem != null)
                {
                    // Se l'elemento esiste, aumenta la quantità
                    existingItem.quantity = newItem.quantity;
                   // Debug.Log($"Aggiornato: {existingItem.itemName}, Nuova Quantità: {existingItem.quantity}");
                }
                else
                {
                    // Se non esiste, aggiungi il nuovo elemento
                    GlobalUserData.Inventory.Add(newItem);
                    //Debug.Log($"Aggiunto: {newItem.itemName}, Quantità: {newItem.quantity}");
                }    
                            
            }else{
                shopOggetti[3,2]=shopOggetti[3,2]+1;
                timer.SubtractMinute();
                
                newItem = new InventoryItem("Tempo", shopOggetti[3,2]); // Nome: tempo
                var existingItem = GlobalUserData.Inventory.FirstOrDefault(item => item.itemName == newItem.itemName);
                if (existingItem != null)
                {
                    // Se l'elemento esiste, aumenta la quantità
                    existingItem.quantity = newItem.quantity;
                   // Debug.Log($"Aggiornato: {existingItem.itemName}, Nuova Quantità: {existingItem.quantity}");
                }
                else
                {
                    // Se non esiste, aggiungi il nuovo elemento
                    GlobalUserData.Inventory.Add(newItem);
                    //Debug.Log($"Aggiunto: {newItem.itemName}, Quantità: {newItem.quantity}");
                }   
                
            }
            testoMoneta.text = ":"+ moneta.ToString();
            ButtonRef.GetComponent<ButtonInfo>().QuantitàTxt.text = shopOggetti[3, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();
        }
    }

    public int verificaquantita(int n)
    {   
        int quantity = 0;
        int i = 0;
        if(GlobalUserData.Inventory.Count !=0)
            {
                for(i=0;i<GlobalUserData.Inventory.Count;i++)
                {
                    var item= GlobalUserData.Inventory[i];
                    string name = item.itemName;
                    if(name == "Tempo" && n == 2)
                    {
                        quantity = item.quantity;
                    }
                    else if(name == "Aiuto" && n == 1)
                    {
                        quantity = item.quantity;
                    }
                }
            }
            else{}
        return quantity;
    }

    //-------------ScriptMenuManager---------------

    [SerializeField] GameObject CanvasShop;

    public void CloseMenuManager()
    {
        CanvasShop.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
}
