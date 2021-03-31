using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    // make a reference to the transform, or position, to the attack collider area
    public Transform attackPos;
    public float attackRange;

    //create enemy layermask so only the enemy is damaged
    public LayerMask whatIsEnemy;

    public int damage;
    public Animator playerAnim;

    public int health;
    public AudioSource hurtSound;
    void Update()
    {
        if (timeBtwAttack <= 0)
        {
            // if the mouse is not clicked, the timeBtwAttack will decrease every frame and become negative
            // which then the timeBtwAttack will be <= 0 and player can attack

            timeBtwAttack = startTimeBtwAttack;
            if (Input.GetKey(KeyCode.Mouse0))
            {
                // set a collider for the area the player can damage enemey
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);

                playerAnim.SetTrigger("Attack");

                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                    
                }
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }



    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    public void playerTakeDamage(int damage)
    {
        health -= damage;
        playerAnim.SetTrigger("playerDamaged");
        hurtSound.Play();
    }
}
