using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : MonoBehaviour {
    public int life = 3;
    public Transform fallDeathCheck;
    private float blood_timer = 0;
    private float death_timer = 0;
    private float death_anim_time = 1f;
    public float bloodLimit = 2f;
    private bool hurt = false;
    private GameObject blood;
    private Camera cam;
    private Animator anim;
    public List<GameObject> hearts;

     void Start(){
        cam = Camera.main;
        blood = GameObject.FindGameObjectWithTag("Blood_BG");
        blood.SetActive(false);
        anim = this.GetComponent<Animator>();

        hearts = new List<GameObject>();

        for (int i = 0; i < life; i++) {
            string tag = "heart_" + (i+1);
            hearts.Add(GameObject.FindGameObjectWithTag(tag));
        }
     }

    void Update(){
        if (life <= 0){
            death_timer += Time.deltaTime;
        }

        if (transform.position.y < fallDeathCheck.position.y){
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<RespawnManager>().reset();
        }

        Vector3 new_pos = new Vector3(cam.transform.position.x, cam.transform.position.y, 0);
        blood.transform.position = new_pos;

        if (hurt){
            blood_timer += Time.deltaTime;
        }

        if (blood_timer >= bloodLimit){
            blood.SetActive(false);
            hurt  = false;
            blood_timer = 0;
        }

        if (death_timer >= death_anim_time){
            death_timer = 0;
            blood.SetActive(false);
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<RespawnManager>().reset();
            anim.SetBool("dead", false);
        }

        return;
    }

    public void getHurt(){
        hurt = true;
        blood_timer = 0;
        life--;

        for (int i = 0; i < hearts.Count; i++) {
            if (hearts[i].activeSelf == true) {
                hearts[i].SetActive(false);
                break;
            }
        }

        blood.SetActive(true);

        if (life <= 0) {
            hurt = false;
            anim.SetBool("dead", true);
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "Weapon") {
            getHurt();
        }

        // 3 Slime van
        for (int i = 0; i < 3; i++) {
            string tag = "Slime_" + (i+1);

            if (col.gameObject.tag == tag) {
                getHurt();
                break;
            }
        }
    }

}