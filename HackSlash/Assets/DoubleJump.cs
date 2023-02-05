using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Gameplay;
using UnityEngine;
using Unity.FPS.Game;

public class DoubleJump : MonoBehaviour
{
    [Tooltip("Force applied upward when Double jumping")]
    public float DoubleJumpForce = 9f;

    bool m_CanDoublejump;
    PlayerCharacterController m_PlayerCharacterController;
    PlayerInputHandler m_InputHandler;
    //float m_LastTimeOfUse;
    public bool IsPlayergrounded() => m_PlayerCharacterController.IsGrounded;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerCharacterController = GetComponent<PlayerCharacterController>();
        DebugUtility.HandleErrorIfNullGetComponent<PlayerCharacterController, Jetpack>(m_PlayerCharacterController,
            this, gameObject);

        m_InputHandler = GetComponent<PlayerInputHandler>();
        DebugUtility.HandleErrorIfNullGetComponent<PlayerInputHandler, Jetpack>(m_InputHandler, this, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Double Jump only if not grounded and jump has been pressed again once in-air
        if (IsPlayergrounded())
        {
            //m_CanDoublejump = false;
        }
        else if (!m_PlayerCharacterController.HasJumpedThisFrame && m_InputHandler.GetJumpInputDown())
        {
            //m_CanDoublejump = true;
            // apply the acceleration to character's velocity
            m_PlayerCharacterController.CharacterVelocity += Vector3.up * DoubleJumpForce;
        }
    }
}
