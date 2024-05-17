using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour {

    public int value = 2;
    public int life = 2;

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "Weapon"){
            life--;

            if (life <= 0){
                GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().addScore(value);
                this.gameObject.SetActive(false);
            }
        }
    }
    
}