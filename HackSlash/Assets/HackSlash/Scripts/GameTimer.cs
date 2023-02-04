using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    //[SerializeField] private GameManagerSO gameManager;
    public ActorsManager m_ActorsManager;

    private Damageable playerDamage;

    void Start()
    {
        m_ActorsManager = FindObjectOfType<ActorsManager>();
        DebugUtility.HandleErrorIfNullFindObject<ActorsManager, GameTimer>(m_ActorsManager, null);

        playerDamage = m_ActorsManager.Player.GetComponent<Damageable>();
        DebugUtility.HandleErrorIfNullGetComponent<Damageable, GameTimer>(playerDamage, this, m_ActorsManager.Player);
    }

    // Update is called once per frame
    void Update()
    {
        playerDamage.InflictDamage(Time.deltaTime, true, null);
    }

}
