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
    public float DashTime = 0.9f;
    PlayerInputHandler m_InputHandler;
    PlayerCharacterController m_PlayerCharacterController;
    CharacterController m_Controller;


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
         //m_InputHandler.GetDashInputDown();
        if (m_InputHandler.GetDashInputDown())
        {
            //Debug.Log("DASH" + PlayerCamera.transform.forward +"  "+PlayerCamera.transform.forward * DashDistance);
            //m_Controller.Move(PlayerCamera.transform.forward * DashDistance);
            StartCoroutine(Dashing());


        }
    }

    IEnumerator Dashing()
    {
        float Start = Time.time;
        while (Time.time-Start < DashTime) {
            Debug.Log(Time.time - Start);
            m_Controller.Move(PlayerCamera.transform.forward * DashSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
