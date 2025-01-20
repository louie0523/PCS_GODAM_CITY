using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    CharacterController characterController;
    public int speed = 4;
    public float rotationSpeed = 3;
    public int Hp = 10;

    bool walk = false;

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
        Walk();
        Rotation();
        Attack();
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
        if(Input.GetKey(KeyCode.Mouse0))
        {
            animator.SetTrigger("Attack");
        }
    }
}
