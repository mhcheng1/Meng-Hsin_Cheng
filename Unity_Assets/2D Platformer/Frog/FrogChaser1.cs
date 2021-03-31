using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogChaser1 : MonoBehaviour
{
    // reference to the transform of the player
    public Transform chaserTargetBack;
    
    public Transform player;
    // set the speed for chaser
    public float speed = 5f;

    

    void Update()
    {
        chaseBack();
    }

    void chaseBack()
    {
        //get the two vector and minus them
        Vector3 displacementFromTarget = chaserTargetBack.position - transform.position;

        // normalize the vector
        Vector3 directionToTarget = displacementFromTarget.normalized;

        // times the speed to find how fast it is going in that direction
        Vector3 velocityToTarget = directionToTarget * speed;

        // adjust the actual speed with Time.deltaTime
        Vector3 moveAmountToTarget = velocityToTarget * Time.deltaTime;

        // make sure the chaser doesn't clip into player
        //find out the magnitude between the player and chaser
        //only if the actual distance is larger than ..., keep chasing
        float distanceToTarget = displacementFromTarget.magnitude;
        if (distanceToTarget > 1f)
        {
            transform.Translate(moveAmountToTarget);

        }

        Vector3 characterScale = transform.localScale;
        if (moveAmountToTarget.x > 0)
        {
            characterScale.x = -6;
        }
        if (moveAmountToTarget.x < 0)
        {
            characterScale.x = 6;
        }
        transform.localScale = characterScale;
    }
}
