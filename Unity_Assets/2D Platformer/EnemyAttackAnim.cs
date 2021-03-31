using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAnim : StateMachineBehaviour
{
    public float distance;
    public float attackDistance;
    public float attack;
    public int damage;
    public int health;
    public float randomChance;
    public float speed;
    public int player_Health;

    public GameObject player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        health = animator.GetComponent<EnemyStats>().enemyHealth;
        speed = animator.GetComponent<EnemyStats>().enemySpeed;
        attackDistance = animator.GetComponent<EnemyStats>().attackDistance;
        distance = animator.GetComponent<EnemyStats>().distanceFromPlayer;
        player_Health = animator.GetComponent<EnemyStats>().player_Health;



        if (distance < attackDistance)
        {
            animator.SetBool("enemyAttackConnect", true);
        }


        randomChance = Random.Range(0f, 1f);
        if(randomChance < 0.05f && health <= 1)
        {
            animator.SetBool("enemyWalkBack", true);
        }
        else
        {
            animator.SetBool("enemyAttack", false);
        }


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //speed = 0;
        animator.SetBool("enemyAttackConnect", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
