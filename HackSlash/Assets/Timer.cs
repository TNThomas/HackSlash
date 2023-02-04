using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private OurManager gameManager;
    [SerializeField]
    private Damageable playerDamage;

    void Start()
    {
        playerDamage = gameManager.player.GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
            playerDamage.InflictDamage(Time.deltaTime, true, null);
    }

}
