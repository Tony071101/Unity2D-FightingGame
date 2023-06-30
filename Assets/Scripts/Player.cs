using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : PlayerBase
{
    [SerializeField] PlayerCombat combat;
    [SerializeField] Player2Combat combat2;
    [SerializeField] private Animator animator;
    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int maxBar = 100;
    [SerializeField] public int minBar = 0;
    [SerializeField] public int currentHealth;
    [SerializeField] public int currentBar;
    [SerializeField] Timer time;
    public HealthBar healthBar;
    public SkillBar skillBar;
    public GameObject retryUI;
    public Text player2Name;
    public bool block;
    public static string player2namestr;
    // private float moveX;
    // [SerializeField] private float Speed;
    // private bool jump = false;
    //Add cannot moving bool type when start later.
    void Start()
    {
        characterController2D = GetComponent<CharacterController2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentBar = maxBar;
        skillBar.SetMaxSKillBar(maxBar);

    }

    // private void Update()
    // {
    //     CheckBlocking();
    //     onMoving();
    // }

    private void FixedUpdate()
    {
        characterController2D.Move(moveX * Time.fixedDeltaTime, jump);
        jump = false;
    }

    protected override void CheckBlocking()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            block = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            block = false;
        }
    }

    protected override void onMoving()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            jump = true;
            animator.SetBool("Jumping", true);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            moveX = -1 * Speed;

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            moveX = 1 * Speed;

        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            moveX = 0 * Speed;

        }
        animator.SetFloat("Speed", Mathf.Abs(moveX));
    }

    public override void CheckLanding()
    {
        animator.SetBool("Jumping", false);
    }

    public void TakeDMG(int dmg)
    {
        if (block == true)
        {
            animator.SetTrigger("Blocking");
        }
        else
        {
            if (currentBar < maxBar)
            {
                currentBar += 20;
                skillBar.SetSkill(currentBar);
            }
            if (currentBar >= 50)
            {
                currentBar += 10;
                skillBar.SetSkill(currentBar);
            }
            currentHealth -= dmg;
            healthBar.SetHealth(currentHealth);
            animator.SetTrigger("Hurt");
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        retryUI.SetActive(true);
        animator.SetBool("Death", true);
        player2Name.text = "Player " + player2namestr + " win!";
        time.notdie = false;
        combat.cantAttack = false;
        combat2.cantAttack = false;
        this.enabled = false;
    }
}
