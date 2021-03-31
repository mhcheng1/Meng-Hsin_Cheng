using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public bool isGrounded = false;
    public Animator animator;
    public Rigidbody2D rb;

    void Update()
    {
        // calling jump function
        Jump();
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        animator.SetBool("Grounded", isGrounded);
        // horizontal movement
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;

        Vector3 characterScale = transform.localScale;
        if (Input.GetAxis("Horizontal") < 0)
        {
            characterScale.x = -5f;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            characterScale.x = 5f;
        }
        transform.localScale = characterScale;



    }
    
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

        }
    }



    public void OnLanding()
    {
        animator.SetBool("Grounded", true);
    }
}

