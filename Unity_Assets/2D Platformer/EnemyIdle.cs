using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : StateMachineBehaviour
{
    public float distance;
    public float run;
    public float walk;
    public float attack;
    public float health;
    public float randomChance;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        run = animator.GetComponent<EnemyStats>().runDistance;
        walk = animator.GetComponent<EnemyStats>().walkDistance;
        attack = animator.GetComponent<EnemyStats>().attackDistance;
        randomChance = Random.Range(0f, 1f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distance = animator.GetComponent<EnemyStats>().distanceFromPlayer;
        health = animator.GetComponent<EnemyStats>().enemyHealth;

        if (distance > run)
        {
            animator.SetBool("enemyRun", true);
        }

        if ( distance < walk && distance > attack)
        {
            animator.SetBool("enemyWalk", true);
        }

        if ( health <= 1 && randomChance < 0.3)
        {
            animator.SetBool("enemyWalkBack", true);
        }
    }



    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
