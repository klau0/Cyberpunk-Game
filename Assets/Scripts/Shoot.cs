using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    public Rigidbody2D itemProto = null;
    public float speed = 1.0f;
    public Transform firePoint;
    public int itemPoolSize = 30;
    public List<Rigidbody2D> itemPool;
    private Animator anim;
    private float timeLimit = 2f;
    private float timer_anim = 0;
    private float timer_shoot = 0;
    public bool facingRight = false;

    void Start(){
        itemPool = new List<Rigidbody2D>();

        for (int i=0; i<itemPoolSize; i++){
            Rigidbody2D itemClone = Instantiate(itemProto);
            itemClone.gameObject.SetActive(false);

            itemPool.Add(itemClone);
        }

        anim = this.GetComponent<Animator>();
    }


    void Update(){
        timer_anim += Time.deltaTime;
        timer_shoot += Time.deltaTime;

        anim.SetBool("shoot", false);

        if(this.gameObject.tag == "Player"){

            if (Input.GetButtonDown("Fire1")){
                anim.SetBool("shoot", true);
                facingRight = this.gameObject.GetComponent<CharacterMovement>().facingRight;
                shoot(facingRight);
            }
        } else if (this.gameObject.tag == "Turret"){

            if (timer_anim >= 1f){
                anim.SetBool("shoot", true);
                timer_anim = 0;
            }
            if (timer_shoot >= timeLimit){
                shoot(facingRight);
                timer_shoot = 0;
            }
        }

    }

    void shoot(bool facingRight){
        Rigidbody2D itemClone = getItemFromPool();
        itemClone.transform.position = firePoint.position;

        itemClone.gameObject.SetActive(true);
        
        float direction = facingRight ? +1 : -1;

        Vector3 force = transform.right * speed * direction;
        itemClone.velocity = force;
    }

    private Rigidbody2D getItemFromPool(){
        
        foreach (Rigidbody2D item in itemPool){
            if (!item.gameObject.activeSelf){
                return item;
            }
        }

        return null;
    }
}