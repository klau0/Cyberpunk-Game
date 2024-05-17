using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawner : Respawner {

    public int original_life;

    protected override void Start(){
        base.Start();
        original_life = this.GetComponent<PlayerHurt>().life;
    }

    public override void Respawn(){
        base.Respawn();
        GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().resetScore();
        this.GetComponent<PlayerHurt>().life = original_life;

        List<GameObject> hearts = GetComponent<PlayerHurt>().hearts;

        for (int i = 0; i < hearts.Count; i++) {
            hearts[i].SetActive(true);
        }

        GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");

        for (int i = 0; i < chests.Length; i++) {
            chests[i].GetComponent<Chest>().firstCollision = true;
        }

    }

}