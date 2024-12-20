using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public Text PrezzoTxt;
    public Text QuantitàTxt;
    public GameObject ShopManager;
    

   
    void Update()
    {
        PrezzoTxt.text =  ShopManager.GetComponent<ShopManagerScript>().shopOggetti[2,ItemID].ToString();
        QuantitàTxt.text= ShopManager.GetComponent<ShopManagerScript>().shopOggetti[3,ItemID].ToString();
    }
}
