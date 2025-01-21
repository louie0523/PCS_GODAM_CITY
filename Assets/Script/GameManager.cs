using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Npc;
    public Transform[] EnemySpwans;
    public Transform[] NpcSpwans;

    private void Start()
    {
        for(int i = 0; i < EnemySpwans.Length; i++)
        {
            Instantiate(Enemy, EnemySpwans[i].transform.position, Enemy.transform.rotation);
            Instantiate(Npc, NpcSpwans[i].transform.position, Npc.transform.rotation);
        }
    }
}
