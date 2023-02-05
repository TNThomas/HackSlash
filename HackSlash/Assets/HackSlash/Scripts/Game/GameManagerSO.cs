using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManagerSO", menuName = "GameManagerSO", order = 1)] 
public class GameManagerSO : ScriptableObject
{
    //public GameObject player;
    //public ActorsManager m_ActorsManager;
    public float StartTimeAmt;
    public float CurrentTimeLeft;


    private void Awake()
    {
        //m_ActorsManager = FindObjectOfType<ActorsManager>();
        //DebugUtility.HandleErrorIfNullFindObject<ActorsManager, Timer>(m_ActorsManager, null);

        //player = m_ActorsManager.Player;
        //if (player == null)
        //{
        //    Debug.LogError("Error: GameManagerSO expected to find a Player GameObject from the ActorsManager, but none were found.");
        //}
    }


}
