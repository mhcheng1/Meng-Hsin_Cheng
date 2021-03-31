using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
using UnityEditor;

public class FrogEnemy : MonoBehaviour
{

    public int health;
    public float speed;
    public Animator camAnim;
    float dazedTime;
    public float startDazedTime;
    
    

    
    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }


        if (dazedTime <= 0)
        {
            speed = 5;
        }
        else {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        dazedTime = startDazedTime;
        health -= damage;
        
        camAnim.SetTrigger("Shake");

    }
}
