using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    //[SerializeField] private GameManagerSO gameManager;
    private ActorsManager m_ActorsManager;
    private GameFlowManager m_GameFlowManager;
    private Damageable playerDamage;

    void Start()
    {
        m_ActorsManager = FindObjectOfType<ActorsManager>();
        DebugUtility.HandleErrorIfNullFindObject<ActorsManager, GameTimer>(m_ActorsManager, this);

        m_GameFlowManager = FindObjectOfType<GameFlowManager>();
        DebugUtility.HandleErrorIfNullFindObject<GameFlowManager, GameTimer>(m_GameFlowManager, this);

        playerDamage = m_ActorsManager.Player.GetComponent<Damageable>();
        DebugUtility.HandleErrorIfNullGetComponent<Damageable, GameTimer>(playerDamage, this, m_ActorsManager.Player);
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_GameFlowManager.GameIsEnding && !m_GameFlowManager.GameIsStarting)
        {
            playerDamage.InflictDamage(Time.deltaTime, true, null);
        }
    }

}
