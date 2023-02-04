using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OurManager", menuName = "OurManager")] 
public class OurManager : ScriptableObject
{
    public GameObject player;
    public float StartTimeAmt;
    public float CurrentTimeLeft;


    private void OnEnable()
    {
        
    }


}
