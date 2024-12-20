using UnityEngine;

public class KeyPickupSystem : MonoBehaviour
{
    private bool isInTriggerZone = false; // Controlla se il giocatore è vicino alla chiave
    private bool isCollected = false; // Stato della chiave
    public InventoryManager inventoryManager;

    private void Update()
    {
        // Se il giocatore è nella zona di interazione e preme E, raccogli la chiave
        if (isInTriggerZone && !isCollected && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Hai raccolto la chiave!");
            inventoryManager.AddItem(gameObject.name);

            isCollected = true;

            // Aggiunge la chiave al giocatore
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();
                if (playerInventory != null)
                {
                    playerInventory.HasKey = true;
                }
            }

            // Distrugge la chiave dopo averla raccolta
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Premi E per raccogliere la chiave.");
            isInTriggerZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTriggerZone = false;
        }
    }
}