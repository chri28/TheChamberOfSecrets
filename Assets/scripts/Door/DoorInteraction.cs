using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    public Transform door; // Riferimento alla porta da aprire
    public float openAngle = 88f; // Angolo di apertura
    public float closeAngle = 0f; // Angolo di chiusura
    public float speed = 2f; // Velocità di apertura/chiusura

    private bool isOpen = false; // Stato della porta
    private bool inTriggerZone = false; // Controlla se il giocatore è nella zona di interazione
    private PlayerInventory playerInventory; // Riferimento all'inventario del giocatore

    [SerializeField] GameObject CanvasVictory;

    public AudioManager audioManager;
    private bool SoundPlayed = false;

    public LevelMenu levelMenu;

    void Start()
    {
        // Trova il componente PlayerInventory nel giocatore
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerInventory = player.GetComponent<PlayerInventory>();
        }
    }

    void Update()
    {
        // Controlla se il giocatore preme il tasto di interazione
        if (inTriggerZone && Input.GetKeyDown(KeyCode.Q))
        {
            if (playerInventory != null && playerInventory.HasKey) // Controlla se il giocatore ha la chiave
            {
                isOpen = !isOpen; // Cambia lo stato della porta
            }
            else
            {
                Debug.Log("La porta è chiusa a chiave! Trova la chiave per aprirla.");
            }
        }

        // Gestisce l'animazione di apertura/chiusura
        float targetAngle = isOpen ? openAngle : closeAngle;
        door.localRotation = Quaternion.Slerp(door.localRotation, Quaternion.Euler(0, targetAngle, 0), Time.deltaTime * speed);
        if (isOpen && !SoundPlayed)
        {
            audioManager.PlaySFX(audioManager.door);
            SoundPlayed = true;
            StartCoroutine(ExecuteAfterDelay(1.0f));
        }
            
    }

    IEnumerator ExecuteAfterDelay(float delay)
    {
        // Aspetta il numero di secondi specificato
        yield return new WaitForSeconds(delay);
        CanvasVictory.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;

        levelMenu.UnlockNextLevel(1);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }

    // Rileva se il giocatore entra nella zona di interazione
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assicurati che il giocatore abbia il tag "Player"
        {
            inTriggerZone = true;
        }
    }

    // Rileva se il giocatore esce dalla zona di interazione
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTriggerZone = false;
        }
    }
}