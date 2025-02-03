using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Npc;
    public Transform[] EnemySpwans;
    public Transform[] NpcSpwans;
    public bool isGunDrop = false;
    public int EnemyCount = 0;
    public bool isEnd = false;
    public int NpcCount = 0;
    bool isPause = false;
    public GameObject[] Ui;

    public TextMeshProUGUI[] CountText;

    private void Start()
    {
        for(int i = 0; i < EnemySpwans.Length; i++)
        {
            Instantiate(Enemy, EnemySpwans[i].transform.position, Enemy.transform.rotation);
            Instantiate(Npc, NpcSpwans[i].transform.position, Npc.transform.rotation);
        }
    }

    private void Update()
    {
        if(EnemyCount >= 4 && !isEnd && !isPause)
        {
            Clear();
        }
        if(NpcCount >= 4 && !isEnd && !isPause)
        {
            Lose();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Ui[0].activeSelf)
            {
                Ui[0].SetActive(true);
                Time.timeScale = 0f;
                isPause = true;
            } else
            {
                Ui[0].SetActive(false);
                Time.timeScale = 1f;
                isPause = false;
            }
        }
        CountText[2].text = "Alive Enemy : " + (4 - EnemyCount);
        CountText[3].text = "Alive Npc : " + (4 - NpcCount);

    }

    void Clear()
    {
        isEnd = true;
        Debug.Log("적을 모두 처치했습니다!");
        Ui[0].SetActive(true);
        Ui[1].SetActive(true);
        Ui[3].SetActive(false);
        CountText[0].text = "Alive Npc : " + (4 - NpcCount);
        Time.timeScale = 0f;
    }

    void Lose()
    {
        isEnd = true;
        Debug.Log("NPC가 모두 사망하였습니다!");
        Ui[0].SetActive(true);
        Ui[2].SetActive(true);
        Ui[3].SetActive(false);
        CountText[1].text = "Dead Enemy : " + EnemyCount;
        Time.timeScale = 0f;
    }
}
