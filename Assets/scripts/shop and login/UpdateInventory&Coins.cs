using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class PlayerDataManager : MonoBehaviour
{
    private UserInventoryCoins player = new UserInventoryCoins();

    // Metodo che verrà chiamato quando si preme il bottone per aggiornare le monete
    public void OnUpdateCoinsClick()
    {
        Debug.Log(GlobalUserData.flag);
        // Avvia la coroutine per fare la richiesta di aggiornamento
        if(GlobalUserData.flag == true)
        {
            StartCoroutine(UpdateCoinsRequest());
        }
    }
    // Coroutine per inviare la richiesta di aggiornamento delle monete
    private IEnumerator UpdateCoinsRequest()
    {
        // Popola l'oggetto 'player' con i dati che vuoi inviare
        player.username = GlobalUserData.Username;  
        player.gameData.coins=GlobalUserData.Coins;
        foreach (var item in GlobalUserData.Inventory)
        {
            player.gameData.inventory.Add(new InventoryItem(item.itemName, item.quantity));
        }

        // Serializza l'oggetto in JSON
        string jsonBody = JsonUtility.ToJson(player);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonBody);

        // Crea la richiesta POST
        using (UnityWebRequest www = new UnityWebRequest("http://127.0.0.1:13765/updateInventoryCoins", "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            // Invia la richiesta e attendi la risposta
            yield return www.SendWebRequest();
        }
        yield return null;
    }
}
