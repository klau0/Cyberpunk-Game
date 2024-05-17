using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiling : MonoBehaviour {
    public const int LEFT = -1;
    public const int RIGHT = 1;
	public int offsetX = 2;
	public bool hasARightBuddy = false;
	public bool hasALeftBuddy = false;
    public bool reverseScale = false;
    private float spriteWidth = 0f;
	private Camera cam;
	private Transform myTransform;

    void Awake(){
        cam = Camera.main;
        myTransform = transform;
    }


    void Start(){
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
		spriteWidth = Mathf.Abs(sRenderer.sprite.bounds.size.x * myTransform.localScale.x);
    }

    void Update(){
        if (!hasALeftBuddy || !hasARightBuddy){
            // calculate the cameras extend (half the width) of what the camera can see in world coordinates
			float camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;

            // calculate the x position where the camera can see the edge of the sprite (element)
			float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
   			float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;

            // checking if we can see the edge of the element and then calling MakeNewBuddy if we can
            if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && !hasARightBuddy){
                makeNewBuddy(RIGHT);
				hasARightBuddy = true;
            } else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && !hasALeftBuddy){
                makeNewBuddy (LEFT);
				hasALeftBuddy = true;
            }
        }
    }
    
    void makeNewBuddy(int rightOrLeft){
		Vector3 newPosition = new Vector3 (myTransform.position.x + spriteWidth * rightOrLeft,
                                           myTransform.position.y, myTransform.position.z);

		Transform newBuddy = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;
        
        if (reverseScale == true) {
			newBuddy.localScale = new Vector3 (newBuddy.localScale.x*-1,
                                               newBuddy.localScale.y,
                                               newBuddy.localScale.z);
        }

        // --- parallaxhoz kell ---
        newBuddy.parent = myTransform.parent;

        if (rightOrLeft > 0){
			newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
		} else {
			newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
		}
    }
}