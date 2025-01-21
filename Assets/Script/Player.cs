using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    CharacterController characterController;
    public int speed = 4;
    public float attackSpeed = 1f;
    public float rotationSpeed = 3;
    public int hp = 10;
    bool isStop = false;

    public GameObject[] Weapons;
    public Transform ShutPostion;
    public GameObject Bullet;
    bool isGun = false;
    bool isShut = false;
    bool walk = false;
    public bool isAttacking = false;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
        //Weapons[1].SetActive(true);
        //Weapons[0].SetActive(false);
        //isGun = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isStop)
        {
            Walk();
            Rotation();
            Attack();
            animator.SetFloat("AttackSpeed", attackSpeed);
        }
    }

    void Walk()
    {
        walk = false;
        if (Input.GetKey(KeyCode.W))
        {
            characterController.Move(this.transform.forward * speed * Time.deltaTime);
            walk = true;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            characterController.Move(-this.transform.forward * speed * Time.deltaTime);
            walk = true;
        }

        if(Input.GetKey(KeyCode.D))
        {
            characterController.Move(this.transform.right * speed * Time.deltaTime);
            walk = true;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            characterController.Move(-this.transform.right * speed * Time.deltaTime);
            walk = true;
        }
        
        if(!walk)
        {
            animator.SetBool("Walk", false);
        } else
        {
            animator.SetBool("Walk", true);
        }
    }

    void Rotation()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        if(GroupPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 PointToLook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(PointToLook.x, this.transform.position.y, PointToLook.z));
        }
    }

    void Attack()
    {
        if(!isGun)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                isAttacking = true;
                animator.SetTrigger("Attack");
                Invoke("WaitAttack", 0.5f / attackSpeed);
            }
        } else
        {
            if(!isShut && Input.GetKey(KeyCode.Mouse0))
            {
                isShut = true;
                animator.SetTrigger("GunAttack");
                Instantiate(Bullet, ShutPostion.position, this.transform.rotation);
                Invoke("GunWait", 0.5f / attackSpeed);
            }
        }
    }

    void GunWait()
    {
        isShut = false;
    }

    void WaitAttack()
    {
        isAttacking = false;
    }

    public void SetHp(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            isStop = true;
            animator.SetTrigger("Death");
            Debug.Log("플레이어가 사망하였습니다.");
        }
    }
}
