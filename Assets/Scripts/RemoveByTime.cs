using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveByTime : MonoBehaviour {
    public float timeLimit = 2.0f;
    public float timeEllapsed = 0.0f;

    void OnEnable(){
        this.timeEllapsed = 0.0f;
    }

    void Update(){
        this.timeEllapsed += Time.deltaTime;

        if (timeEllapsed > timeLimit){
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        this.gameObject.SetActive(false);
    }
}