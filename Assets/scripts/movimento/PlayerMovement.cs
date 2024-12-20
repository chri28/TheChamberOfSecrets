using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;

    public AudioManager audioManager;

    private bool isWalking = false; // Flag per tracciare se il giocatore sta camminando
    private bool isPlayingFootstep = false;

    // Update is called once per frame
    void Update()
    {
        // Verifica se il giocatore è a terra
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Se il giocatore è a terra, ferma la velocità verticale
        }

        // Ottieni gli input di movimento orizzontale (x) e verticale (z)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Crea il movimento del giocatore utilizzando le assi X e Z
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        // Gestione suono dei passi
        if (x != 0 || z != 0) // Il giocatore si sta muovendo
        {
            if (!isPlayingFootstep)
            {
                isPlayingFootstep = true;
                audioManager.PlaySFX(audioManager.footstep); // Riproduci il suono dei passi
                StartCoroutine(ResetFootstepFlag());
            }
        }

        // Verifica se il giocatore può saltare (è a terra)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Calcolo della velocità per il salto
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime; // Aggiungi la gravità al movimento verticale

        controller.Move(velocity * Time.deltaTime); // Muovi il giocatore
    }

    IEnumerator ResetFootstepFlag()
    {
        yield return new WaitForSeconds(audioManager.footstep.length);
        isPlayingFootstep = false;
    }
}
