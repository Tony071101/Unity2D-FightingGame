using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player2 : MonoBehaviour
{
    [SerializeField] MoveArrows moveArrows;
    [SerializeField] Player2Combat combat2;
    [SerializeField] PlayerCombat combat;
    [SerializeField] MoveASWD moveASWD;
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
    public Text playerName;
    public bool block;
    public static string playernamestr;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentBar = maxBar;
        skillBar.SetMaxSKillBar(maxBar);
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            block = true;
        }
        if(Input.GetKeyUp(KeyCode.DownArrow))
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
        playerName.text = "Player " + playernamestr + " win!";
        time.notdie = false;
        moveArrows.cantMove2 = false;
        combat2.cantAttack = false;
        moveASWD.cantMove = false;
        combat.cantAttack = false;
        this.enabled = false;
    }
}
