using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Gameplay;
using UnityEngine;
using Unity.FPS.Game;

public class Dash : MonoBehaviour
{
    [Tooltip("Reference to the main camera used for the player")]
    public Camera PlayerCamera;
    [Tooltip("Speed when Dashing")]
    public float DashSpeed = 15f;
    [Tooltip("time the dash lasts for in seconds")]
    public float dashTime = 0.9f;

    public float dashCooldown = 0.45f;
    public float dashStartTime = 0f;

    int numDashesSinceLastGrounded = 0;
    int maxNumAirDashes = 0;

    bool isDashing = false;

    PlayerInputHandler m_InputHandler;
    PlayerCharacterController m_PlayerCharacterController;
    CharacterController m_Controller;

    public bool IsPlayergrounded() => m_PlayerCharacterController.IsGrounded;

    // Start is called before the first frame update
    void Start()
    {
        m_InputHandler = GetComponent<PlayerInputHandler>();
        DebugUtility.HandleErrorIfNullGetComponent<PlayerInputHandler, Dash>(m_InputHandler, this,
            gameObject);

        m_PlayerCharacterController = GetComponent<PlayerCharacterController>();
        DebugUtility.HandleErrorIfNullGetComponent<PlayerCharacterController, Dash>(m_PlayerCharacterController,
            this, gameObject);

        m_Controller = GetComponent<CharacterController>();
        DebugUtility.HandleErrorIfNullGetComponent<CharacterController, Dash>(m_Controller,
            this, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Double Jump only if not grounded and jump has been pressed again once in-air
        if (IsPlayergrounded())
        {
            numDashesSinceLastGrounded = 0;
        }

        if (m_InputHandler.GetDashInputDown() && !isDashing && numDashesSinceLastGrounded <= maxNumAirDashes && Time.time >= dashStartTime + dashCooldown)
        {
            Debug.Log("DASH");
            StartCoroutine(Dashing());
        }
    }

    IEnumerator Dashing()
    {
        dashStartTime = Time.time;

        numDashesSinceLastGrounded += 1;

        isDashing = true;

        float startTime = Time.time;
        while (Time.time - startTime < dashTime) {
            m_Controller.Move(PlayerCamera.transform.forward * DashSpeed * Time.deltaTime);
            yield return null;
        }

        isDashing = false;
    }
}
