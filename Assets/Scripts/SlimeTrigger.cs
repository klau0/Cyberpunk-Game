using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTrigger : MonoBehaviour {

    public float attack_speed = 2.5f;
    public float idle_speed = 1f;
    public string slime_tag = null;
    private GameObject slime;
    private Animator slimeAnim;

    void Start(){
        slime =  GameObject.FindGameObjectWithTag(slime_tag);
        slimeAnim = slime.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            slimeAnim.SetBool("attack", true);
            slime.GetComponent<SlimeMovement>().speed = attack_speed;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            slimeAnim.SetBool("attack", false);
            slime.GetComponent<SlimeMovement>().speed = idle_speed;
        }
    }

}
