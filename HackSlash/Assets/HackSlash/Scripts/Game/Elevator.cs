using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Unity.FPS.Game;

[RequireComponent(typeof(Collider))]
public class Elevator : MonoBehaviour
{
    [Tooltip("Where should the elevator move to?")]
    [SerializeField] private float defaultYTarget = 0f;
    [Tooltip("What speed should the elevator move at?")]
    [SerializeField] private float speed = 1f;
    [Tooltip("Should the elevator begin moving on scene load?")]
    [SerializeField] private bool autoStart = false;
    [Tooltip("Should the elevator begin open or closed?")]
    [SerializeField] private bool doorStartsClosed = false;

    [Tooltip("Should the elevator begin moving when the player enters it?")]
    [SerializeField] private bool moveOnPlayerEntry = false;

    [Tooltip("If this is the starting elevator, on arrival, it will notify the GameFlowManager that the game is done starting.")]
    [SerializeField] private bool isStartingElevator = false;

    private Animator m_Animator;
    private GameFlowManager m_GameFlowManager;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        DebugUtility.HandleErrorIfNullGetComponent<Animator, ReachGoalObjective>(m_Animator, this, gameObject);
    }

    private void Start()
    {
        m_GameFlowManager = FindObjectOfType<GameFlowManager>();
        DebugUtility.HandleErrorIfNullFindObject<GameFlowManager, GameTimer>(m_GameFlowManager, this);

        if (doorStartsClosed) CloseDoor();
        else OpenDoor();

        if(autoStart)
        {
            //Move the elevator upwards towards goal position
            StartMoveToGoal(defaultYTarget);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (moveOnPlayerEntry && other.CompareTag("Player"))
        {
            CloseDoor();

            StartMoveToGoal(defaultYTarget, 0.5f);
        }
    }

    public void CloseDoor()
    {
        // Close the door
        if (m_Animator)
        {
            m_Animator.SetBool("IsOpen", false);
        }
    }

    public void OpenDoor()
    {
        // Open the door
        if (m_Animator)
        {
            m_Animator.SetBool("IsOpen", true);
        }
    }

    public void StartMoveToGoal(float goalYPosition, float moveDelay = 0f)
    {
        StartCoroutine(MoveTowardsGoal(goalYPosition, moveDelay));
    }

    IEnumerator MoveTowardsGoal(float goalYPosition, float moveDelay = 0f)
    {
        // Find out direction to move
        float direction = Mathf.Sign(goalYPosition - transform.localPosition.y);

        // Delay if there is one
        float count = 0f;
        while (count <= moveDelay)
        {
            count += Time.deltaTime;
            yield return null;
        }

        // Move towards goal while we haven't passed it
        while (Mathf.Sign(goalYPosition - transform.localPosition.y) == direction)
        {
            transform.localPosition += Vector3.up * Time.deltaTime * speed * direction;

            yield return null;
        }

        // Snap to goal height
        transform.localPosition = new Vector3(transform.localPosition.x, goalYPosition, transform.localPosition.z);

        // Open the door
        OpenDoor();

        // Tell the GameFlowManager that the game started
        if(isStartingElevator) m_GameFlowManager.GameStarted();
    }
}
