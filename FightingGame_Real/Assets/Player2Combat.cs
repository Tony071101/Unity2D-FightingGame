using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Combat : MonoBehaviour
{
    [SerializeField] Player2 player2;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayers;
    public int attackDmg;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public bool cantAttack;
    public bool canuse2;
    void Start()
    {
        cantAttack = true;
        canuse2 = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                if (cantAttack == true)
                {
                    Attack();
                    nextAttackTime = Time.time + 2f / attackRate;
                }
            }
                
        }
        if (player2.currentBar >= player2.maxBar)
        {
            canuse2 = true;
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                if (canuse2 == true && cantAttack == true)
                {
                    SkillAttack();
                    player2.currentBar = 0;
                    player2.skillBar.SetSkill(player2.currentBar);
                }
            }
        }
        else
        {
            canuse2 = false;
        }
    }
    public void Attack()
    {
        animator.SetTrigger("Attack");
        //Detect Enemies
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        //Dmg
        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<Player>().TakeDMG(attackDmg);
            if (player2.currentBar < player2.maxBar)
            {
                player2.currentBar += 10;
                player2.skillBar.SetSkill(player2.currentBar);
            }

        }
    }
    public void SkillAttack()
    {
        animator.SetTrigger("Attack2");
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<Player>().TakeDMG(attackDmg * 4);
            if (player2.currentBar < player2.maxBar)
            {
                player2.currentBar += 10;
                player2.skillBar.SetSkill(player2.currentBar);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
