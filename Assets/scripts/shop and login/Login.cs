using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
//using System.Text.Json;
//using System.Globalization;
//using System;
//using UnityEngine.SceneManagement;
//using Unity.VisualScripting;
using System.Collections.Generic;

public class Login : MonoBehaviour
{
    [SerializeField] private string autenticazioneAccount = "http://127.0.0.1:13765/account";
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;
     //SerializeField ->attributi utilizzato per rendere una variabile privata
    //TMP_InputField -> componente di TMPro 

    [SerializeField] private TextMeshProUGUI alertText;
    [SerializeField] private Button LoginButton;
    InventoryItem newItem = new InventoryItem();
    public void OnLoginClick()
    {
        alertText.text = "Sto accedendo...";
        LoginButton.interactable = false;
        StartCoroutine(TryLogin());
    }

   private IEnumerator TryLogin() // questa è una coroutine                              // il metodo UnityWebRequest.Get potrebbe richiedere tempo per completarsi, e usando una coroutine si evita che l'interfaccia utente si blocchi durante l'attesa della risposta dal server.
   {   
        string username = usernameInputField.text; //prende il testo su unity e lo memorizza in questa variabile
        string password = passwordInputField.text; //prende il testo su unity e lo memorizza in questa variabile
        // Piccolo controllo sulla lunghezza della password
        if(password.Length < 3 || password.Length> 24)
        {
            alertText.text = "Lenght Password";
            LoginButton.interactable = true;
            yield break;
        }
        UnityWebRequest request = UnityWebRequest.Get($"{autenticazioneAccount}?rUsername={username}&rPassword={password}"); // richiesta http get(come se la stessimo facendo da browser)
        var handler = request.SendWebRequest(); //serve per inviare la richiesta e restituisce un handler per controllarla
        float startTime= 0.0f;
        while(!handler.isDone)
        {
            startTime+=Time.deltaTime;
            if(startTime>10.0f)
            {
                break;
            }
            yield return null;
        }// questo while serve per farsi che quando si invia una richiesta GET per utenticare l'utente , si interrompe se non succede niente entro 10 secondi
        
        if(request.result == UnityWebRequest.Result.Success) // verrà creato un altro if perchè non è detto che avere come risultato SUCCESSO ci dia conferma che siamo connessi al database
        {
            if(request.downloadHandler.text!="Password errata")
            {   
                string responseText = request.downloadHandler.text; // mi "scarica" il json
                Memorizza(responseText);
                alertText.text="benvenuto"; // mette lo stato BENVENUTO come stato
                LoginButton.interactable = false;
            }else{
                alertText.text="Credenziali non valide"; 
                LoginButton.interactable = true;
            }
        }
        else{
            alertText.text="Si è verificato un errore";
            LoginButton.interactable = true;
        }
        yield return null;
   }

   public void Memorizza(string responseText)
   {
         // Deserializza il JSON nella classe UserData
        UserData userData = JsonUtility.FromJson<UserData>(responseText); // creo un oggetto di tipo userdata2 per memorizzare i campi forniti dal json
        // Memorizzo  i dati in questa classe statica
        GlobalUserData.Username=userData.username;
        GlobalUserData.Coins=userData.gameData.coins;

        GlobalUserData.Inventory = new List<InventoryItem>();
            foreach (var item in userData.gameData.inventory)
            {
                GlobalUserData.Inventory.Add(new InventoryItem(item.itemName, item.quantity));
            }
   }
}

