using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Animator animator;
    public Transform Player;
    public int hp = 3;
    public bool isStop = false;

    public float NavDisTance = 15f;
    public bool isAttacking = false;


    private void Awake()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        Player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if(isStop || Vector3.Distance(this.transform.position, Player.position) > NavDisTance)
        {
            navMeshAgent.isStopped = true;
            return;
        }
        if (Vector3.Distance(this.transform.position, Player.position) < navMeshAgent.stoppingDistance + 0.1f)
        {
            navMeshAgent.isStopped = true;
            animator.SetBool("Walk", false);
            StartCoroutine("Attack");
        } else
        {
            navMeshAgent.isStopped = false;
            animator.SetBool("Walk", true);
            navMeshAgent.destination = Player.position; // 적이 향해야할 위치
        }

        this.transform.LookAt(Player.position);
    }

    IEnumerator Attack()
    {
        if(!isStop)
        {
            yield return new WaitForSeconds(0.5f);
            isAttacking = true;
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(0.5f);
            isAttacking = false;
            if (Vector3.Distance(this.transform.position, Player.position) < navMeshAgent.stoppingDistance + 0.1f)
            {
                Attack();
            }
            else
            {
                navMeshAgent.isStopped = false;
            }
        }
    }

    public void SetHp(int damage)
    {
        if(!isStop)
        {
            hp -= damage;
            if (hp <= 0)
            {
                animator.SetTrigger("Death");
                isStop = true;
                Debug.Log("적이 사망하였습니다.");
                navMeshAgent.isStopped = true;
            }
        }
    }
}
