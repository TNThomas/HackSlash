using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;

public class KillDoor : MonoBehaviour
{

    public Health[] enemies;
    private float EnemiesKilled = 0;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Health h in enemies)
        {
            h.OnDie += Open;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        EnemiesKilled++;
        if (EnemiesKilled >= enemies.Length)
        {
            Debug.Log("ill open");
            animator.SetTrigger("Open");
        }
    }
        
        
}
