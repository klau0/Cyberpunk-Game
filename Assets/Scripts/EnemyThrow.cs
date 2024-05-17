using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrow : MonoBehaviour
{
    public Transform player;
    public GameObject itemProto = null;
    public float speed = 1.0f;
    public Transform firePoint;
    public float firePeriod = 5.0f;
    public float timeToFire = 0;
    private float firePointDistance;
    public float range = 10f;
    public bool firing = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeToFire = firePeriod;
        firePointDistance = (this.transform.position - firePoint.position).magnitude;
    }

    void Update()
    {
        float playerDistance = (this.transform.position - player.transform.position).magnitude;

        if (playerDistance < range) // vagy if (firing)
        {
            timeToFire -= Time.deltaTime;
        } //else { timeToFire = firePeriod; }
        if (timeToFire < 0 )
        {
            ThrowProjetile();
            timeToFire = firePeriod;
        }
    }

    void ThrowProjetile()
    {
        GameObject newThrowable = Instantiate(itemProto);
        Vector3 direction = (player.transform.position - transform.position).normalized;
        newThrowable.transform.position = firePoint.position + direction * firePointDistance;

        newThrowable.GetComponent<Rigidbody>().velocity = direction * speed;
    }

    private void onTrigggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            firing = true;
        }
        Debug.Log("trigger enter");
    }

    private void onTrigggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            firing = false;
        }
        Debug.Log("trigger exit");
    }
}
