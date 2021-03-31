using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class EnemyStats : MonoBehaviour
{
    public int enemyHealth;
    public float enemySpeed;
    public float enemyWalkingSpeed;
    public int enemyDamage;
    private bool canAttack;

    public float distanceFromPlayer;
    public float runDistance;
    public float walkDistance;
    public float attackDistance;
    public int player_Health;

    public GameObject player;
    public GameObject block;
    public Animator enemAnim;
    public Animator playerAnim;

    public bool facingRight;

    private float timeBtwAttack;
    public float startTimeBtwAttack;

    Animation animaTion;
    void Start()
    {
        enemyWalkingSpeed = enemySpeed - 3;
        enemAnim = GetComponent<Animator>();
        playerAnim = player.GetComponent<Animator>();
    }

    void Update()
    {
        player_Health = player.GetComponent<PlayerAttacking>().health;

        distanceFromPlayer = Vector2.Distance(gameObject.transform.position, player.transform.position);
        
        //flipping
        if(transform.position.x - player.transform.position.x > 0 && facingRight == true)
        {
            flip();
            facingRight = false;
        }
        if (transform.position.x - player.transform.position.x < 0 && facingRight == false)
        {
            flip();
            facingRight = true;
        }

        if (enemyHealth <= 0)
        {
            enemAnim.SetTrigger("enemyDeath");
        }


        //attack
        canAttack = enemAnim.GetBool("enemyAttackConnect");
        if ( distanceFromPlayer < attackDistance && timeBtwAttack < 0)
        {
            timeBtwAttack = startTimeBtwAttack;
            enemAnim.SetBool("enemyAttack", true);
            
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        if (block.activeSelf == false && canAttack == true)
        {
            player.GetComponent<PlayerAttacking>().playerTakeDamage(enemyDamage);
        }

        

    }

    void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        enemAnim.SetBool("enemyHurt", true);

        if (transform.position.x - player.transform.position.x > 0) 
        {
            Vector2 actualTarget = new Vector2(transform.position.x + 2f, transform.position.y);
            transform.position = actualTarget;
        }
        if (transform.position.x - player.transform.position.x < 0)
        {
            Vector2 actualTarget = new Vector2(transform.position.x - 2f, transform.position.y);
            transform.position = actualTarget;
        }

            
    }

    //public void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        player.GetComponent<PlayerAttacking>().playerTakeDamage(1);
    //    }
    //}
}
