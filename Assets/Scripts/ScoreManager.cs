using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {

    public int score = 0;
    public TMP_Text scoreText = null;

    void Awake() {
        // hogy ha a menübe lépek majd vissza és újratöltõdik a scene akkor 2 ScoreManager gameObject lesz
        // (DontDestroyOnLoad miatt) (a Menühöz még kell a ScoreManager a High Score miatt),
        // ezért a korábbit ki kell törölni
        GameObject[] objs = GameObject.FindGameObjectsWithTag("ScoreManager");

        if (objs.Length > 1) {

            for (int i = 0; i < objs.Length; i++) {
                if (objs[i].GetComponent<ScoreManager>().score == -1) {
                    Destroy(objs[i]);
                    break;
                }
            }
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start () {
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TMP_Text>();
    }

    public void addScore(int amount) {
        score += amount;
        scoreText.text = score + "";
    }

    public void resetScore() {
        score = 0;
        scoreText.text = score + "";
    }
}
