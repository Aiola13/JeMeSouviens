using UnityEngine;
using System.Collections;

public class TouchLogicSwipe : MonoBehaviour {

	private Vector2 fp ;  // first finger position
	private Vector2 lp;  // last finger position
	
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
				
				if((fp.x - lp.x) > 80) { // left swipe
					OnSwipeLeft();
				}
				else if((fp.x - lp.x) < -80) { // right swipe
					OnSwipeRight();
				}
				else if((fp.y - lp.y) < -80 ) { // up swipe
					OnSwipeUp();
				}
				else if((fp.y - lp.y) > 80 ) { // down swipe
					OnSwipeDown();
				}
			}
		}
	}




	private void OnSwipeLeft() {
		print("left");
	}
	private void OnSwipeRight() {
		print("right");
	}
	private void OnSwipeUp() {
		print("up");
	}
	private void OnSwipeDown() {
		print("down");
	}

}
