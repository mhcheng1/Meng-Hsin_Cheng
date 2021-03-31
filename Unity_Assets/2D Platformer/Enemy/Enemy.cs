using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
using UnityEditor;

public class Enemy : MonoBehaviour
{

    public int health;
    public Animator camAnim;
    public Animator enimAnim;
    public GameObject knockBack;
    public EnemyAttack attack;
    
    // Update is called once per frame
    void Update()
    {
         
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        
        health -= damage;
        camAnim.SetTrigger("Shake");
        enimAnim.SetTrigger("EnemyHurt");
        attack.dazedTime = attack.startDazedTime;
        transform.position = knockBack.transform.position;

    }
}
