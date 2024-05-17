using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {

    public int score = 0;
    public TMP_Text scoreText = null;

    void Awake() {
        // hogy ha a men�be l�pek majd vissza �s �jrat�lt�dik a scene akkor 2 ScoreManager gameObject lesz
        // (DontDestroyOnLoad miatt) (a Men�h�z m�g kell a ScoreManager a High Score miatt),
        // ez�rt a kor�bbit ki kell t�r�lni
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
