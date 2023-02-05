using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;

public class KillDoor : MonoBehaviour
{

    public Health[] enemyHealth;
    private float EnemiesKilled = 0;
    public Animator animator;

    void Start()
    {
        foreach(Health h in enemyHealth)
        {
            h.OnDie += Open;
        }
    }

    public void Open()
    {
        EnemiesKilled++;

        if (EnemiesKilled >= enemyHealth.Length)
        {
            Debug.Log("ill open");
            animator.SetTrigger("Open");
        }
    }
        
        
}
