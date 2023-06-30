using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player2 : PlayerBase
{
    [SerializeField] Player2Combat combat2;
    [SerializeField] PlayerCombat combat;
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
    public Text playerName;
    public bool block;
    public static string playernamestr;
    void Start()
    {
        characterController2D = GetComponent<CharacterController2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentBar = maxBar;
        skillBar.SetMaxSKillBar(maxBar);

    }

    private void FixedUpdate()
    {
        characterController2D.Move(moveX * Time.fixedDeltaTime, jump);
        jump = false;
    }

    protected override void onMoving()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump = true;
            animator.SetBool("Jump", true);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveX = -1 * Speed;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveX = 1 * Speed;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            moveX = 0 * Speed;
        }
        animator.SetFloat("Speed", Mathf.Abs(moveX));
    }

    protected override void CheckBlocking()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            block = true;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            block = false;
        }
    }

    public override void CheckLanding()
    {
        animator.SetBool("Jump", false);
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
        playerName.text = "Player " + playernamestr + " win!";
        time.notdie = false;
        combat2.cantAttack = false;
        combat.cantAttack = false;
        this.enabled = false;
    }
}
