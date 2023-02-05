using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;
using System.Threading;

[RequireComponent(typeof(Collider), typeof(Elevator))]
public class ReachGoalObjective : Objective
{
    private Elevator m_Elevator;

    private void Awake()
    {
        m_Elevator = GetComponent<Elevator>();
        DebugUtility.HandleErrorIfNullGetComponent<Elevator, ReachGoalObjective>(m_Elevator, this, gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            m_Elevator.CloseDoor();

            // Complete the objective
            CompleteObjective("::PRIVELEGES ELEVATED::", string.Empty, "Objective complete : " + Title);

            m_Elevator.StartMoveToGoal(transform.position.y + 100f, 0.5f);
        }
    }
}
