using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    public int value = 10;
    private Animator anim;
    public bool firstCollision = true;

    void Start(){
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {

            if (firstCollision) {
                anim.SetBool("found", true);
                GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().addScore(value);
                firstCollision = false;
            }
           
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            anim.SetBool("found", false);
        }
    }
}
