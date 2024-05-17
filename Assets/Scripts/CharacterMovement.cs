using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    public float speed = 1f;
    public float jumpForce = 2f;
    public Vector3 jump;
    public bool facingRight = true;
    private Rigidbody2D rb;
    private Animator anim;

    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    private bool grounded;

    private bool canDoubleJump = false;

    private bool falling = false;

    void Start(){
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        jump = new Vector3(0f, 2f, 0f);
    }

    void Update(){
        // -1, 0, 1 ~ bal, semmi, jobb
        float horiz = Input.GetAxis("Horizontal");

        Vector2 old_v = rb.velocity;
        old_v.x = speed * horiz;
        rb.velocity = old_v;

        if (facingRight && horiz < 0){
            flip();
        } else if (!facingRight && horiz > 0){
            flip();
        }

        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("grounded", grounded);

        if (falling){
            anim.SetBool("doubleJump", false);
            falling = false;
        }

        if (Input.GetButtonDown("Jump")){
            if (grounded){
                anim.SetBool("doubleJump", false);

                Vector2 o_v = rb.velocity;
                o_v.y = 0;
                rb.velocity = o_v;

                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                canDoubleJump = true;
            } else if (canDoubleJump){
                anim.SetBool("doubleJump", true);
                
                Vector2 o_v = rb.velocity;
                o_v.y = 0;
                rb.velocity = o_v;

                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                canDoubleJump = false;
                falling = true;
            }
        }
    }



    private void flip(){
        facingRight = !facingRight;

        Vector3 theScale = this.transform.localScale;
        theScale.x *= -1;
        this.transform.localScale = theScale;
    }

}
