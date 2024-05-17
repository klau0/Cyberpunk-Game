using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeRespawner : Respawner {
    private int original_life;

    protected override void Start() {
        base.Start();

        original_life = this.GetComponent<SlimeHurt>().life;
    }

    public override void Respawn() {
        base.Respawn();

        this.GetComponent<SlimeHurt>().life = original_life;
    }
}
