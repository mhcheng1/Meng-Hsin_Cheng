using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAttack : MonoBehaviour
{
    public GameObject player;
    private PlayerAttack playerAttack;
    public int damage = 1;
    private void Start()
    {
        playerAttack = player.GetComponent<PlayerAttack>();
    }

    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerAttack.playerTakeDamage(damage);
            Debug.Log("player damaged");
        }
    }

    void Update()
    {
    }
}
