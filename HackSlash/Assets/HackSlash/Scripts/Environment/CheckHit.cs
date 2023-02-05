using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CheckHit : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField]
    float DamageAmount = 0.25f;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Damageable>().InflictDamage(DamageAmount, true , gameObject);
        }
    }

}
