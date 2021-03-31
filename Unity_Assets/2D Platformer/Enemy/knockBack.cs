using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockBack : MonoBehaviour
{
    public Transform transformOfPlayer;
    public GameObject player;
    // set the speed for chaser
    public float speed = 10f;

    private void Start()
    {
        
        
    }

    void Update()
    {
        float randomPosition = Random.Range(1.5f, 2f);
        Vector3 chaserPostion = new Vector3(-randomPosition, 0f, 0f);
        if (player != null)
        {
            if (player.transform.localScale.x == 5)
            {
                gameObject.transform.position = player.transform.position - chaserPostion;
            }
            if (player.transform.localScale.x == -5)
            {
                gameObject.transform.position = player.transform.position + chaserPostion;
            }
       
            Vector3 displacementFromTarget = transformOfPlayer.position - transform.position;

            Vector3 directionToTarget = displacementFromTarget.normalized;


            Vector3 velocityToTarget = directionToTarget * speed;


            Vector3 moveAmountToTarget = velocityToTarget * Time.deltaTime;

            float distanceToTarget = displacementFromTarget.magnitude;
            if (distanceToTarget > 2f)
            {
                transform.Translate(moveAmountToTarget);

            }

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
