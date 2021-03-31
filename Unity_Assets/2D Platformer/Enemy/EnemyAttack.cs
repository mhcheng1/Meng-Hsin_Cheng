using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject player;
    private PlayerAttack playerAttack;
    public int damage = 1;
    public Animator enemyAnim;
    public FrogChaser chaser;
    private float enemySpeed;
    public float dazedTime;
    public float startDazedTime = 0.8f;

    private void Start()
    {
        playerAttack = player.GetComponent<PlayerAttack>();
        
    }

    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Block")
        {
            enemyAnim.SetTrigger("EnemyBlocked");
            Debug.Log("Blocked");
            dazedTime = startDazedTime;

        }
        if (other.gameObject.tag == "Player")
        {
            enemyAnim.SetTrigger("EnemyAttack");
            playerAttack.playerTakeDamage(damage);
            
        }
        if (other.gameObject.tag == "Enemy")
        {
            chaser.speed = chaser.stopSpeed;
            dazedTime = startDazedTime;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            chaser.speed = Random.Range(8f, 10f);
        }
    }

    void Update()
    {
        if (transform.position.x - player.transform.position.x < 10)
        {
            if (dazedTime <= 0)
            {
                chaser.speed = Random.Range(8f, 10f);
            }
            else
            {
                chaser.speed = chaser.stopSpeed;
                dazedTime -= Time.deltaTime;
            }
        }
           

        enemySpeed = chaser.speed;
        enemyAnim.SetFloat("Speed", enemySpeed);
        
    }
}
