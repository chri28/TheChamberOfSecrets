using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerData : MonoBehaviour
{
    private UserCoins player = new UserCoins();

    // Metodo che verrà chiamato quando si preme il bottone per aggiornare le monete
    public void OnUpdateCoinsClick()
    {
        // Avvia la coroutine per fare la richiesta di aggiornamento
        StartCoroutine(UpdateCoinsRequest());
    }

    // Coroutine per inviare la richiesta di aggiornamento delle monete
    private IEnumerator UpdateCoinsRequest()
    {
        // Popola l'oggetto 'player' con i dati che vuoi inviare
        player.username = GlobalUserData.Username;  // Assegna lo username globale
        player.gameData.coins=GlobalUserData.Coins;
        // Serializza l'oggetto in JSON
        string jsonBody = JsonUtility.ToJson(player);

        Debug.Log(jsonBody);

        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonBody);

        // Crea la richiesta POST
        using (UnityWebRequest www = new UnityWebRequest("http://127.0.0.1:13765/updateCoins", "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            // Invia la richiesta e attendi la risposta
            yield return www.SendWebRequest();

            // Gestisci la risposta
            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Dati inviati correttamente!");
            }
            else
            {
                Debug.LogError($"Errore nella richiesta: {www.error}");
            }
        }
    }
}
