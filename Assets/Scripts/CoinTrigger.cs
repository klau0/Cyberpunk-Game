using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTrigger : MonoBehaviour {

    public int value = 1;

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player") {
            gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().addScore(value);
        }
    }
}
