using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float atk_cd;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int dmg;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] PlayerCombat combat;
    [SerializeField] Timer time;
    public SkillBar skillBar;
    private Animator animator;
    private float cdTimer = Mathf.Infinity;
    public int maxHealth = 100;
    public int maxBar = 100;
    public int minBar = 0;
    int currentHealth;
    int currentBar;
    public HealthBar healthBar;
    private Player playerHealth;
    public GameObject retryUI;
    public Text text;
    public GameObject Player;
    public float speed;
    private float distance;
    public float distanceBetween;
    public bool cantMove;
    public bool cantAtttack;

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentBar = maxBar;
        skillBar.SetMaxSKillBar(maxBar);
        cantAtttack = true;
        cantMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        cdTimer += Time.deltaTime;
        if (cantAtttack)
        {
            if (PlayerInSight())
            {
                if (cdTimer >= atk_cd)
                {
                    cdTimer = 0;
                    animator.SetTrigger("Attack");
                }
                cantMove = false;
                animator.SetFloat("Speed", 0);
            }
            else
            {
                cantMove = true;
            }
        }
        if (cantMove == false)
        {
            animator.SetFloat("Speed", 0);
        }
        if (cantMove)
        {
            distance = Vector2.Distance(transform.position, Player.transform.position);
            Vector2 direction = Player.transform.position - transform.position;
            if (distance < distanceBetween)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
                animator.SetFloat("Speed", distance);
            }
        }
    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Player>();
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    private void DmgPlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.GetComponent<Player>().TakeDMG(dmg);
        }
    }
    public void TakeDMG(int dmg)
    {
        currentBar += 20;
        skillBar.SetSkill(currentBar);
        currentHealth -= dmg;
        healthBar.SetHealth(currentHealth);
        //Take hit animation
        animator.SetTrigger("Hurt");
        cantMove = false;
        animator.SetFloat("Speed", 0);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        //Debug.Log("Enemy Down");
        retryUI.SetActive(true);
        text.text = "Player 1 win!";
        //Dead animation
        animator.SetBool("Death", true);
        //Disable Enemy
        time.notdie = false;
        cantAtttack = false;
        cantMove = false;
        combat.cantAttack = false;
    }
}
