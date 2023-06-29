using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveASWD : MonoBehaviour
{
    public CharacterController2D controller2D;
    public float Speed = 40f;
    float MovementX = 0f;
    public Animator animator;
    public bool cantMove;
    bool jump = false;
    void Start()
    {
        cantMove = true;
    }

    void Update()
    {
        if (cantMove == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                jump = true;
                animator.SetBool("Jumping", true);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                MovementX = -1 * Speed;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                MovementX = 1 * Speed;
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                MovementX = 0 * Speed;
            }
            animator.SetFloat("Speed", Mathf.Abs(MovementX));
        }
    }
    void FixedUpdate()
    {
        controller2D.Move(MovementX * Time.fixedDeltaTime, jump);
        jump = false;
    }
    public void Landing()
    {
        animator.SetBool("Jumping", false);
    }
}
