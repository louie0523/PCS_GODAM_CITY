using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Postion : MonoBehaviour
{
    public enum PostionType
    {
        Speed,
        Hp,
        AttackSpeed,
        Gun,
    }

    public PostionType Ptype;

    public float Speed_Up = 4f;
    public int Hp_Up = 3;
    public float AttackSpeed = 1.0f;
    public GameManager gameManager;

    public Player player;

    public GameObject[] ItemObj;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();   
        int Ran;
       if(!gameManager.isGunDrop)
        {
            Ran = Random.Range(0, 4);
        } else
        {
            Ran = Random.Range(0, 3);
        }
        if(Ran == 0)
        {
            Ptype = PostionType.Speed;
        } else if (Ran == 1)
        {
            Ptype = PostionType.Hp;
        } else if (Ran == 2)
        {
            Ptype = PostionType.AttackSpeed;
        } else if (Ran == 3)
        {
            Ptype = PostionType.Gun;
            gameManager.isGunDrop = true;
        }

        ItemObj[Ran].SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            switch(Ptype)
            {
                case PostionType.Speed:
                    player.SpeedUp(Speed_Up);
                    break;
                case PostionType.Hp:
                    player.HpUp(Hp_Up);
                    break;
                case PostionType.AttackSpeed:
                    player.AttackSpeeddUp(AttackSpeed);
                    break;
                case PostionType.Gun:
                    player.GetGun();
                    break;
            }
            thisItemDestory();
        }

        
    }

    void thisItemDestory()
    {
        Destroy(gameObject);
    }


}
