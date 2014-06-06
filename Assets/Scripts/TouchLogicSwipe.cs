using UnityEngine;
using System.Collections;

public class TouchLogicSwipe : MonoBehaviour {

	public float dist = 100;	// distance to register a swipe in any given direction
	private Vector2 fp ; 	 	// first finger position
	private Vector2 lp;  		// last finger position
	static public bool swipeLeft = false;
	static public bool swipeRight = false;
	static public bool swipeUp = false;
	static public bool swipeDown = false;
	
	void Update() {
		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
				fp = touch.position;
				lp = touch.position;
			}
			if (touch.phase == TouchPhase.Moved ) {
				lp = touch.position;
			}
			if(touch.phase == TouchPhase.Ended) { 
				
				if((fp.x - lp.x) > dist) { // left swipe
					OnSwipeLeft();
				}
				else if((fp.x - lp.x) < -dist) { // right swipe
					OnSwipeRight();
				}
				else if((fp.y - lp.y) < -dist) { // up swipe
					OnSwipeUp();
				}
				else if((fp.y - lp.y) > dist) { // down swipe
					OnSwipeDown();
				}
			}
		}
	}




	private void OnSwipeLeft() {
		print("left");
		swipeLeft = true;
		swipeRight = false;
		swipeUp = false;
		swipeDown = false;
	}
	private void OnSwipeRight() {
		print("right");
		swipeRight = true;
		swipeLeft = false;
		swipeUp = false;
		swipeDown = false;
	}
	private void OnSwipeUp() {
		print("up");
		swipeUp = true;
		swipeLeft = false;
		swipeRight = false;
		swipeDown = false;
	}
	private void OnSwipeDown() {
		print("down");
		swipeDown = true;
		swipeLeft = false;
		swipeUp = false;
		swipeRight = false;
	}

}
