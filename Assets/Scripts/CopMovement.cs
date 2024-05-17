using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopMovement : MonoBehaviour {

    public bool facingRight = false;
    public float speed = 5f;
    public float jumpForce = 40f;
    private Rigidbody2D rb;
    public int direction = -1;
    public Transform rightRunLimit;
    public Transform leftRunLimit;
    public bool shooting = false;
    private Animator anim;
    private bool playerWasOnLeft = false;
    private bool playerWasOnRight = false;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update(){

        if (shooting) {
            rb.velocity = Vector3.zero;
        } else {
            Vector2 old_v = rb.velocity;
            old_v.x = speed * direction;
            rb.velocity = old_v;
        }

        bool playerOnL = GetComponent<CopTrigger>().playerOnLSide;
        bool playerOnR = GetComponent<CopTrigger>().playerOnRSide;

        if ( (playerWasOnRight && GetComponent<CopTrigger>().otherPosX < rightRunLimit.position.x)
            || (playerWasOnLeft && GetComponent<CopTrigger>().otherPosX > leftRunLimit.position.x) ){

            anim.SetBool("idle", false);
            playerWasOnLeft = false;
            playerWasOnRight = false;
        }

        // bal oldalt van a player, de az enemy jobbra néz VAGY jobb oldalt van a player, de az enemy balra néz
        if (playerOnL && facingRight || playerOnR && !facingRight) {
            direction *= -1;
        }

        if (transform.position.x < leftRunLimit.position.x) {

            if (playerOnL) {
                rb.velocity = new Vector2(0.000001f, 0);
                anim.SetBool("idle", true);
                playerWasOnLeft = true;

            } else {
                transform.position = new Vector3(leftRunLimit.position.x+1, transform.position.y, transform.position.z);
                direction *= -1;
            }
        } else if (transform.position.x > rightRunLimit.position.x) {

            if (playerOnR) {
                rb.velocity = new Vector2(0.000001f, 0);
                anim.SetBool("idle", true);
                playerWasOnRight = true;

            } else {
                transform.position = new Vector3(rightRunLimit.position.x-1, transform.position.y, transform.position.z);
                direction *= -1;
            }
        }

        if (facingRight && direction < 0) {
            flip();
        } else if (!facingRight && direction > 0) {
            flip();
        }
    }

    private void flip() {
        facingRight = !facingRight;

        Vector3 theScale = this.transform.localScale;
        theScale.x *= -1;
        this.transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if ( (other.gameObject.tag == "JumpRight" && facingRight) || (other.gameObject.tag == "JumpLeft" && !facingRight) ) {

            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
}
