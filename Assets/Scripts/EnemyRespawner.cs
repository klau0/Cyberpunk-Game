using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawner : Respawner {
    private int original_life;

    protected override void Start(){
        base.Start();

        original_life = this.GetComponent<EnemyDestroy>().life;
    }

    public override void Respawn(){
        base.Respawn();

        this.GetComponent<EnemyDestroy>().life = original_life;
    }
}