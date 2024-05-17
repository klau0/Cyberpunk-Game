using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHurt : MonoBehaviour
{
    public int value = 2;
    public int life = 2;
    private Animator anim;
    private float death_timer = 0;
    private float hurt_timer = 0;
    private float hurt_anim_time = 0.66667f;
    private float death_anim_time = 1f;
    private bool hurt = false;

    void Start() {
        anim = this.GetComponent<Animator>();
    }

    void Update() {
        if (life <= 0) {
            death_timer += Time.deltaTime;
        }

        if (hurt) {
            hurt_timer += Time.deltaTime;
        }

        if (hurt_timer >= hurt_anim_time) {
            hurt = false;
            hurt_timer = 0;
            anim.SetBool("hurt", false);
        }

        if (death_timer >= death_anim_time){
            death_timer = 0;
            anim.SetBool("dead", false);
            this.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag == "Weapon"){
            life--;
            anim.SetBool("hurt", true);
            hurt = true;

            if (life <= 0){
                anim.SetBool("dead", true);
                GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().addScore(value);
            }
        }
    }

}
