using UnityEngine;
using System.Collections;

public class ActiviteMenu : MonoBehaviour {

	private float dist = 30;	// distance to register a swipe in any given direction
	private Vector2 fp ; 	 	// first finger position
	private Vector2 lp;  		// last finger position


	private float speed = 30.0f;         // The relative speed at which the camera will catch up.

	public int camFocusedOn = 0;		// the camera is focused on the first activity which is Crepe
	public bool isMoving = false;
	public bool canSwipe = true;			


	// crepe = 0.0f, peche = 20.0f, jardinage = 0.0f
	private float[] activitePos = new float[3] {0.0f, 20.0f, 40.0f};

	private Vector3 target = new Vector3(0, 0, -10);

	public GUITexture leftArrow;
	public GUITexture rightArrow;
	public GUITexture jouer;
	public GUITexture menu;


	void Update() {

		// we are to the leftmost, we dont display left arrow
		if (camFocusedOn == 0) {
			DisplayArrow(leftArrow, false);
		}
		// we are to the rightmost, we dont display right arrow
		else if (camFocusedOn == activitePos.Length - 1) {
			DisplayArrow(rightArrow, false);
		}
		// both arrows are displayed
		else {
			DisplayArrow(leftArrow, true);
			DisplayArrow(rightArrow, true);
		}


		if (isMoving) {
			transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
			if (transform.position.x == target.x) {
				canSwipe = true;
				isMoving = false;
			}
		}


		if (Input.touchCount == 1) {

			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Began) {
				fp = touch.position;
				lp = touch.position;
			}

			else if (touch.phase == TouchPhase.Moved) {
				lp = touch.position;
			}

			else if (touch.phase == TouchPhase.Ended) {

				if (!isMoving) {

					// touch the right arrow
					if (rightArrow.HitTest(touch.position)) {
						OnSwipeLeft();
					}
					// touch the left arrow
					else if (leftArrow.HitTest(touch.position)) {
						OnSwipeRight();
					}
					// touch the play button
					if (jouer.HitTest(touch.position)) {
						if (camFocusedOn == 0) 
							Application.LoadLevel("a_crepe");
						else if (camFocusedOn == 1) 
							Application.LoadLevel("a_peche");
						else if (camFocusedOn == 2) 
							Application.LoadLevel("a_jardin");
					}
					// touch the menu button
					else if (menu.HitTest(touch.position)) {
						Application.LoadLevel("menu");
					}

					/*
					Ray ray ;
					RaycastHit hit;
					ray = Camera.main.ScreenPointToRay(touch.position);

					if (Physics.Raycast(ray, out hit)) {

						print (hit.collider.gameObject.name);
						if (hit.collider.gameObject.name == "BackMenu") {
							print ("ss");
							//hit.collider.gameObject.renderer.material.color = Color.green;
							Application.LoadLevel("a_menu");
						}

						else if (hit.collider.gameObject.name == "Peche") {
							hit.collider.gameObject.renderer.material.color = Color.green;
							Application.LoadLevel("a_peche");
						}
						
						else if (hit.collider.gameObject.name == "Crepe") {
							hit.collider.gameObject.renderer.material.color = Color.green;
							Application.LoadLevel("a_crepe");
						} 
						
						else if (hit.collider.gameObject.name == "Jardinage") {
							hit.collider.gameObject.renderer.material.color = Color.green;
							Application.LoadLevel("a_jardin");
						}
					}
					*/
				}
					

				if (canSwipe) {
					// left swipe
					if((fp.x - lp.x) > dist) {
						OnSwipeLeft();
					}
					
					// right swipe
					else if((fp.x - lp.x) < -dist) { 
						OnSwipeRight();
					}

					/*
					// up swipe
					else if((fp.y - lp.y) < -dist) { 
						OnSwipeUp();
					}
					
					// down swipe
					else if((fp.y - lp.y) > dist) { 
						OnSwipeDown();
					}
					*/
				}
			}
		}
	}


	void OnSwipeLeft() {
		//print("left");
		if (camFocusedOn < activitePos.Length - 1) {
			camFocusedOn++;
			canSwipe = false;
			isMoving = true;
			Vector3 tar = new Vector3(activitePos[camFocusedOn], 0, -10);
			target = tar;
		}
	}
	void OnSwipeRight() {
		//print("right");
		if (camFocusedOn > 0) {
			camFocusedOn--;
			canSwipe = false;
			isMoving = true;
			Vector3 tar = new Vector3(activitePos[camFocusedOn], 0, -10);
			target = tar;
		}
	}

	/*
	void OnSwipeUp() {
		print("up");
	}
	void OnSwipeDown() {
		print("down");
	}
	*/

	// active/desactive et affiche/enleve la texture t
	public static void DisplayArrow(GUITexture t, bool status) {
		if (status) {
			t.guiTexture.enabled = true;
			t.gameObject.SetActive(true);
		}

		else {
			t.guiTexture.enabled = false;
			t.gameObject.SetActive(false);
		}
	}

	
}
