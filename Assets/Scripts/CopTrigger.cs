using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CopTrigger : MonoBehaviour {

    public Rigidbody2D itemProto = null;
    public float speed = 15f;
    public Transform firePoint;
    public int itemPoolSize = 30;
    public List<Rigidbody2D> itemPool;
    private Animator anim;
    private float anim_start_timer = 1f;
    private bool canStartTimingEndOfAnim = false;
    private float anim_end_timer = 0;
    private float firePeriod = 1f;
    public bool playerOnLSide = false;
    public bool playerOnRSide = false;
    public float otherPosX;

    void Start(){
        itemPool = new List<Rigidbody2D>();

        for (int i = 0; i<itemPoolSize; i++) {
            Rigidbody2D itemClone = Instantiate(itemProto);
            itemClone.gameObject.SetActive(false);

            itemPool.Add(itemClone);
        }

        anim = this.GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {

            otherPosX = other.gameObject.transform.position.x;
            float thisPosX = this.transform.position.x;

            playerOnLSide = otherPosX < thisPosX;
            playerOnRSide = otherPosX > thisPosX;

            timing(canStartTimingEndOfAnim, ref anim_end_timer);

            if (anim_end_timer >= 0.5f) {
                anim_end_timer = 0;
                canStartTimingEndOfAnim = false;
                this.GetComponent<CopMovement>().shooting = false;
                anim.SetBool("shoot", false);
                anim_start_timer = 0;
            }

            timing(true, ref anim_start_timer);

            if (anim_start_timer >= firePeriod) {
                anim_start_timer = 0;
                this.GetComponent<CopMovement>().shooting = true;
                anim.SetBool("shoot", true);
                shoot();
                canStartTimingEndOfAnim = true;
            }
        }
    }

    private void timing(bool condition, ref float timer) {
        if (condition) {
            timer += Time.deltaTime;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {

            this.GetComponent<CopMovement>().shooting = false;
            anim.SetBool("shoot", false);
            anim.SetBool("idle", false);
            playerOnLSide = false;
            playerOnRSide = false;
        }
    }

    void shoot(){
        Rigidbody2D itemClone = getItemFromPool();
        itemClone.transform.position = firePoint.position;

        itemClone.gameObject.SetActive(true);

        float direction = this.GetComponent<CopMovement>().facingRight ? +1 : -1;

        Vector3 force = transform.right * speed * direction;
        itemClone.velocity = force;
    }

    private Rigidbody2D getItemFromPool(){

        foreach (Rigidbody2D item in itemPool) {
            if (!item.gameObject.activeSelf) {
                return item;
            }
        }

        return null;
    }
}
