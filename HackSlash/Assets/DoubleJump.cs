using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Gameplay;
using UnityEngine;
using Unity.FPS.Game;

public class DoubleJump : MonoBehaviour
{
    [Tooltip("Force applied upward when Double jumping")]
    public float DoubleJumpForce = 9f;

    int numAirJumpsSinceLastGrounded = 0;
    int maxAirJumps = 1;

    PlayerCharacterController m_PlayerCharacterController;
    PlayerInputHandler m_InputHandler;
    public bool IsPlayergrounded() => m_PlayerCharacterController.IsGrounded;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerCharacterController = GetComponent<PlayerCharacterController>();
        DebugUtility.HandleErrorIfNullGetComponent<PlayerCharacterController, DoubleJump>(m_PlayerCharacterController,
            this, gameObject);

        m_InputHandler = GetComponent<PlayerInputHandler>();
        DebugUtility.HandleErrorIfNullGetComponent<PlayerInputHandler, DoubleJump>(m_InputHandler, this, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Double Jump only if not grounded and jump has been pressed again once in-air
        if (IsPlayergrounded())
        {
            numAirJumpsSinceLastGrounded = 0;
        }
        else if (!m_PlayerCharacterController.HasJumpedThisFrame && m_InputHandler.GetJumpInputDown() && numAirJumpsSinceLastGrounded < maxAirJumps)
        {
            numAirJumpsSinceLastGrounded += 1;
            // apply the acceleration to character's velocity
            m_PlayerCharacterController.CharacterVelocity += Vector3.up * DoubleJumpForce;
        }
    }
}
