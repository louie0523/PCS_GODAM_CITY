using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    CharacterController characterController;
    public float speed = 4;
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

    public float[] currentTime;
    public TextMeshProUGUI[] StatusText;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();

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
        StatusText[0].text = "HP : " + hp;
        StatusText[1].text = "Speed : " + speed;
        StatusText[2].text = "Attack Speed : " + attackSpeed * 100 + "%";
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

    public void GetGun()
    {
        Debug.Log("무기가 총으로 업그레이드 됩니다.");
        Weapons[1].SetActive(true);
        Weapons[0].SetActive(false);
        isGun = true;
    }

    IEnumerator TimeSpeed()
    {
        while (currentTime[0] > 0)
        {
            currentTime[0] -= 1f;
            yield return new WaitForSeconds(1f);
            if(currentTime[0] == 0)
            {
                speed -= 4;
            }
        }
    }

    IEnumerator TimeAttackSpeed()
    {
        while (currentTime[1] > 0)
        {
            currentTime[1] -= 1f;
            yield return new WaitForSeconds(1f);
            if (currentTime[1] == 0)
            {
                attackSpeed--;
            }
        }
    }



    public void SpeedUp(float Up)
    {
        Debug.Log("스피드 업 포션을 획득했습니다.");
        speed += Up;
        currentTime[0] += 5f;
        StartCoroutine("TimeSpeed");
    }

    public void AttackSpeeddUp(float Up)
    {
        Debug.Log("공격 속도 포션을 획득했습니다.");
        attackSpeed += Up;
        currentTime[1] += 10f;
        StartCoroutine("TimeAttackSpeed");
    }

    public void HpUp(int Up)
    {
        hp += Up;
        Debug.Log("체력 포션을 획득했습니다.");
        if(hp > 10)
        {
            hp = 10;
        }
    }
}
