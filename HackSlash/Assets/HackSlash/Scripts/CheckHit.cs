using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CheckHit : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField]
    float DamageAmount = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag== "Player")
        {
            other.GetComponent<Damageable>().InflictDamage(DamageAmount, true , gameObject);
        }
    }

}
