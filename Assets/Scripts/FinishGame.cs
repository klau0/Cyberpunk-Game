using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour {

    private GameObject winSign;
    public bool win = false;

    void Start() {
        winSign = GameObject.FindGameObjectWithTag("Win");
        winSign.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            win = true;
            winSign.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
