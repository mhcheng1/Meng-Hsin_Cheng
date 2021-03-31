using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkBack : StateMachineBehaviour
{
    public float cdTimer;
    public float cd;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cdTimer = cd;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (cdTimer < 0)
        {
            cdTimer = cd;
            animator.SetBool("enemyWalkBack", false);
        }
        else
        {
            Vector2 actualTarget = new Vector2(animator.transform.position.x + 5f, animator.transform.position.y);
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, actualTarget, 3 * Time.deltaTime);
            cdTimer -= Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("enemyAttack", false);
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
