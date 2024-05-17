using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    private ParticleSystem lightning_1;
    private ParticleSystem lightning_2;
    private System.Random random;
    private float timer_1 = 0;
    private float timer_2 = 0;
    private int time_1 = -1;
    private int time_2 = -1;
    private int highScore = 0;
    public TMP_Text highScoreText = null;
    private int score;

    void Start () {
        lightning_1 = GameObject.Find("Lightning_1").GetComponent<ParticleSystem>();
        lightning_2 = GameObject.Find("Lightning_2").GetComponent<ParticleSystem>();
        random = new System.Random();

        highScore = PlayerPrefs.GetInt("highScore", highScore);
        highScoreText.text = "High Score: " + highScore;
        score = 0;
    }

    private void Update() {
        timer_1 += Time.deltaTime;
        timer_2 += Time.deltaTime;

        if (timer_1 >= time_1) {
            timer_1 = 0;
            time_1 = random.Next(1, 4);

            lightning_1.Play(true);
        }

        if (timer_2 >= time_2) {
            timer_2 = 0;
            time_2 = random.Next(1, 4);

            lightning_2.Play(true);
        }

        // az if csak teszteléshez kell
        if (GameObject.FindGameObjectWithTag("ScoreManager") == null) {
            score = 0;
        } else {
            score = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().score;
        }

        if (score > highScore) {
            highScore = score;
            highScoreText.text = "High Score: " + score;

            PlayerPrefs.SetInt("highScore", highScore);
        }
    }

    public void newGame() {
        if (Time.timeScale == 0){
            Time.timeScale = 1;
        }
        SceneManager.LoadScene(0);
    }

    public void quit() {
        Application.Quit();
    }

    // ez alapján fogom tudni melyik ScoreManager-t kell kitörölni ha új játék indul
    void OnDestroy() {
        // az if csak teszteléshez kell
        if (GameObject.FindGameObjectWithTag("ScoreManager") != null) {
            GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().score = -1;
        }
    }

}
