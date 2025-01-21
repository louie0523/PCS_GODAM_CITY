using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharType
{
    Player,
    Enemy,
}
public class AttackPoint : MonoBehaviour
{
    public CharType Chtype;
    public Player player;
    public Enemy enemy;
    public int damage;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(Chtype)
        {
            case CharType.Player:
                if (other.CompareTag("Enemy") && player.isAttacking)
                {
                    enemy = other.GetComponent<Enemy>();
                    player.isAttacking = false;
                    enemy.SetHp(damage);
                }
                break;
            case CharType.Enemy:
                if(other.CompareTag("Player") && enemy.isAttacking)
                {
                    enemy.isAttacking = false;
                    player.SetHp(damage);
                }
                break;
        }
    }
}
