using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArrows : MonoBehaviour
{

    public CharacterController2D controller2D;
    public float Speed = 40f;
    float MovementX = 0f;
    public Animator animator;
    public bool cantMove2;
    bool jump = false;
    void Start()
    {
        cantMove2 = true;
    }

    void Update()
    {
        if (cantMove2 == true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                jump = true;
                animator.SetBool("Jump", true);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MovementX = -1 * Speed;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MovementX = 1 * Speed;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                MovementX = 0 * Speed;
            }
            animator.SetFloat("Speed", Mathf.Abs(MovementX));
        }
    }
    public void Landing()
    {
        animator.SetBool("Jump", false);
    }
    void FixedUpdate()
    {
        controller2D.Move(MovementX * Time.fixedDeltaTime, jump);
        jump = false;
    }
}
