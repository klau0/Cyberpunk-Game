using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour {

    public bool facingRight = false;
    public float speed = 1f;
    private Rigidbody2D rb;
    private int direction = -1;
    private int life;

    void Start(){
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update(){
        life = this.gameObject.GetComponent<SlimeHurt>().life;

        if (life <= 0) {
            rb.velocity = Vector2.zero;
        } else {
            Vector2 old_v = rb.velocity;
            old_v.x = speed * direction;
            rb.velocity = old_v;
        }

        if (facingRight && direction < 0) {
            flip();
        } else if (!facingRight && direction > 0) {
            flip();
        }
    }

    private void flip(){
        facingRight = !facingRight;

        Vector3 theScale = this.transform.localScale;
        theScale.x *= -1;
        this.transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D other){
        // hitting wall
        if (other.gameObject.layer == 3) {
            direction *= -1;
        }
    }
}
