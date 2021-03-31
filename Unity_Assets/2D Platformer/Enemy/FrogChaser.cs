using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogChaser : MonoBehaviour
{
    // reference to the transform of the player
    public Transform chaserTargetBack;

    public Transform chaserTargetFront;

    public Transform player;
    // set the speed for chaser
    public float speed;
    public float stopSpeed = 0f;
    bool facingleft = true;
    

    void Start()
    {
        
    }


    void Update()
    {


        if (transform.localScale.x > 0 && player.localScale.x > 0 && transform.position.x - player.position.x < 10)
        {
            chasePlayer();
        }
        if (transform.localScale.x < 0 && player.localScale.x < 0 && transform.position.x - player.position.x < 10)
        {
            chasePlayer();
        }
        else
        {
            if (transform.position.x - player.position.x < 10)
            {
                chaseBack();
            }
            if (transform.position.x - player.position.x >= 10)
            {
                speed = stopSpeed;
            }


        }
        void chaseBack()
        {
            
            Vector3 displacementFromTarget = chaserTargetBack.position - transform.position;

            Vector3 directionToTarget = displacementFromTarget.normalized;

            Vector3 velocityToTarget = directionToTarget * speed;

            Vector3 moveAmountToTarget = velocityToTarget * Time.deltaTime;

            moveAmountToTarget.y = 0f;
            float distanceToTarget = displacementFromTarget.magnitude;
            if (distanceToTarget > 1f)
            {
                transform.Translate(moveAmountToTarget);

            }

            Vector3 characterScale = transform.localScale;

            if (moveAmountToTarget.x > 0 && facingleft == true)
            {
                characterScale.x = characterScale.x * -1;
                facingleft = false;
            }
            if (moveAmountToTarget.x < 0 && facingleft == false)
            {
                characterScale.x = characterScale.x * -1;
                facingleft = true;
            }
            transform.localScale = characterScale;

        }

        void chasePlayer()
        {
            
            Vector3 displacementFromTarget = player.position - transform.position;

            Vector3 directionToTarget = displacementFromTarget.normalized;

            Vector3 velocityToTarget = directionToTarget * speed;

            Vector3 moveAmountToTarget = velocityToTarget * Time.deltaTime;

            moveAmountToTarget.y = 0;
            float distanceToTarget = displacementFromTarget.magnitude;
            if (distanceToTarget > 1f)
            {
                transform.Translate(moveAmountToTarget);
            }

            Vector3 characterScale = transform.localScale;

            if (moveAmountToTarget.x > 0 && facingleft == true)
            {
                characterScale.x = characterScale.x * -1;
                facingleft = false;
            }
            if (moveAmountToTarget.x < 0 && facingleft == false)
            {
                characterScale.x = characterScale.x * -1;
                facingleft = true;
            }
            transform.localScale = characterScale;
        }
    }
}
