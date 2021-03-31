using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : StateMachineBehaviour
{
    public float distance;
    public float attack;
    GameObject _player;
    public float walkSpeed;
    public float run;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attack = animator.GetComponent<EnemyStats>().attackDistance;
        _player = animator.GetComponent<EnemyStats>().player;
        walkSpeed = animator.GetComponent<EnemyStats>().enemyWalkingSpeed;
        run = animator.GetComponent<EnemyStats>().runDistance;
    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distance = animator.GetComponent<EnemyStats>().distanceFromPlayer;
        Vector2 actualTarget = new Vector2(_player.transform.position.x, animator.transform.position.y);
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, actualTarget, walkSpeed * Time.deltaTime);
        if (Mathf.Abs(distance) < attack | Mathf.Abs(distance) > run)
        {
            animator.SetBool("enemyWalk", false);
        }  
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
