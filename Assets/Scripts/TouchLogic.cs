using UnityEngine;
using System.Collections;

public class TouchLogic : MonoBehaviour {

	public void CheckTouches (){

		// is there a touch on screen?
		if (Input.touches.Length <= 0) {
			// no touches on screen

		}
		else { 
			// there are touches on screen

			// loop through all touches
			for (int i = 0; i < Input.touchCount; i++) {
				if(this.guiTexture != null && this.guiTexture.HitTest(Input.GetTouch(i).position)) {
				// the current touch is hitting our guiTexture

					if(Input.GetTouch(i).phase == TouchPhase.Began){
					// A finger touched the screen
						OnTouchBegan();
					}
					if(Input.GetTouch(i).phase == TouchPhase.Ended){
					// A finger was lifted from the screen
						OnTouchEnd();
					}
					if(Input.GetTouch(i).phase == TouchPhase.Stationary){
					// A finger is touching the screen but hasn't moved
						OnTouchStationary();
					}
					if(Input.GetTouch(i).phase == TouchPhase.Moved){
						// A finger moved on the screen.
						OnTouchMoved();
					}
				}

				// outside of guiTexture

			}
		}
	}

	public virtual void OnTouchBegan() {
		print (name + " is not using OnToucBegan()");
	}

	public virtual void OnTouchEnd() {
		print (name + " is not using OnToucEnd()");
	}

	public virtual void OnTouchStationary() {
		print (name + " is not using OnTouchStationary()");
	}

	public virtual void OnTouchMoved() {
		print (name + " is not using OnTouchMoved()");
	}
}
