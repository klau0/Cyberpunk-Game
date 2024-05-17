using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public float speed = 1.0f;
    public Transform target;
    public Vector3 offset;
    private float halfHeight;
    private float halfWidth;
    public Transform camLimit;
    public Transform camLimitEnd;

    void Start(){
        // kamera és target helye közti eltérés
        offset = target.position - transform.position;

        Camera cam = Camera.main;
        //Debug.Log("cam.orthographicSize: " + cam.orthographicSize);
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        //Debug.Log("width: " + width);

        halfHeight = height / 2;   // 20 / 2 = 10
        halfWidth = width / 2;     // 55.82 / 2 = 27.91

        // camera.x = 0.5 -> widthLimit = -27.41
        // camera.y = 1.3 -> heightLimit = -8.7
    }

    void Update(){
        if (target){
            // itt kéne lennie a targetnak
            Vector3 anchorPos = transform.position + offset;
            // mennyivel mozdult el a target az anchorPos-hoz képest
            Vector3 movement = target.position - anchorPos;

            // eltoljuk a kamerát is annyival amennyivel a target mozdult (Time.deltaTime = 2 frame között eltelt idő, ezzel nem lesz hardverfüggő)
            Vector3 newCamPos = transform.position + movement * speed * Time.deltaTime;

            float heightCheck = newCamPos.y - halfHeight;
            float widthCheckOnStart = newCamPos.x - halfWidth;
            float widthCheckOnFinish = newCamPos.x + halfWidth;

            if (heightCheck < camLimit.position.y){
                newCamPos.y = camLimit.position.y + halfHeight;
            }
            if (widthCheckOnStart < camLimit.position.x){
                newCamPos.x = camLimit.position.x + halfWidth;
            }
            if(widthCheckOnFinish > camLimitEnd.position.x) {
                newCamPos.x = camLimitEnd.position.x - halfWidth;
            }

            transform.position = newCamPos;
        }
    }
}
