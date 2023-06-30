using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] MoveASWD moveMent;
    [SerializeField] MoveArrows moveArrows;
    [SerializeField] PlayerCombat combat;
    [SerializeField] Player2Combat combat2;
    public Animator animator;
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
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentBar = maxBar;
        skillBar.SetMaxSKillBar(maxBar);
        retryUI.SetActive(false);
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            block = true;
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            block = false;
        }
    }
    public void TakeDMG(int dmg)
    {
        if(block == true)
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
        moveMent.cantMove = false;
        combat.cantAttack = false;
        moveArrows.cantMove2 = false;
        combat2.cantAttack = false;
        this.enabled = false;
    }
}
