using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Player player1;
    public Animator animator;
    public Transform attackPoint;
    private float attackRange = 0f;
    public LayerMask enemyLayers;
    public LayerMask playerLayers;
    public int attackDmg;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public bool cantAttack;
    public bool canuse;
    void Start()
    {
        cantAttack = true;
        canuse = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (cantAttack == true)
                {
                    Attack();
                    nextAttackTime = Time.time + 2f / attackRate;
                }
            }
        }
        if (player1.currentBar >= player1.maxBar)
        {
            canuse = true;
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (canuse == true && cantAttack == true)
                {
                    SkillAttack();
                    player1.currentBar = 0;
                    player1.skillBar.SetSkill(player1.currentBar);
                }
            }
        }
        else
        {
            canuse = false;
        }
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        //Detect Enemies
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        //Dmg
        foreach (Collider2D enenmy in hitEnemy)
        {
            enenmy.GetComponent<Enemy>().TakeDMG(attackDmg);
            if (player1.currentBar < player1.maxBar)
            {
                player1.currentBar += 10;
                player1.skillBar.SetSkill(player1.currentBar);
            }
        }
        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<Player2>().TakeDMG(attackDmg);
            if (player1.currentBar < player1.maxBar)
            {
                player1.currentBar += 10;
                player1.skillBar.SetSkill(player1.currentBar);
            }
        }

    }

    public void SkillAttack()
    {
        animator.SetTrigger("Attack2");
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        foreach (Collider2D enenmy in hitEnemy)
        {
            enenmy.GetComponent<Enemy>().TakeDMG(attackDmg * 4);
            if (player1.currentBar < player1.maxBar)
            {
                player1.currentBar += 10;
                player1.skillBar.SetSkill(player1.currentBar);
            }
        }
        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<Player2>().TakeDMG(attackDmg * 4);
            if (player1.currentBar < player1.maxBar)
            {
                player1.currentBar += 10;
                player1.skillBar.SetSkill(player1.currentBar);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
