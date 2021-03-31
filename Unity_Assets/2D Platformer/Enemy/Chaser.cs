using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{   
    // reference to the transform of the player
    public Transform transformOfPlayer;

    // set the speed for chaser
    public float speed = 6f;

    void Update()
    {
        //get the two vector and minus them
        Vector3 displacementFromTarget = transformOfPlayer.position - transform.position;

        // normalize the vector
        Vector3 directionToTarget = displacementFromTarget.normalized;

        // times the speed to find how fast it is going in that direction
        Vector3 velocityToTarget = directionToTarget * speed;

        // adjust the actual speed with Time.deltaTime
        Vector3 moveAmountToTarget = velocityToTarget * Time.deltaTime;
        moveAmountToTarget.y = 0;

        // make sure the chaser doesn't clip into player
        //find out the magnitude between the player and chaser
        //only if the actual distance is larger than ..., keep chasing
        float distanceToTarget = displacementFromTarget.magnitude;
        if (distanceToTarget > 2f)
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
