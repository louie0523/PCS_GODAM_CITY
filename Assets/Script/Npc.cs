using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    Animator animator;
    bool isAlive = true;
    public GameManager gameManager;


    private void Start()
    {
        animator = this.GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if((!other.CompareTag("Wall") || !other.CompareTag("Ground") || !other.CompareTag("Player"))&& isAlive)
        {
            animator.SetTrigger("Death");
            isAlive = false;
            gameManager.NpcCount++;
            Invoke("Destroy", 10f);
        }
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
