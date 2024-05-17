using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    private GameObject pause;
    private GameObject last_tile;

    private void Start() {
        pause = GameObject.FindGameObjectWithTag("Pause");
        pause.SetActive(false);
        last_tile = GameObject.Find("Last_tile");
    }

    void Update(){

        if (Input.GetButtonDown("Cancel")) {
            SceneManager.LoadScene(1);
        }

        bool win = last_tile.GetComponent<FinishGame>().win;

        if (Input.GetButtonDown("Pause") && !win){
            if (Time.timeScale == 0) {
                Time.timeScale = 1;
                pause.SetActive(false);
            } else {
                Time.timeScale = 0;
                pause.SetActive(true);
            }
        }
    }

}
