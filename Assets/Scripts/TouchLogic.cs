using UnityEngine;
using System.Collections;

public class TouchLogic : MonoBehaviour {

	void Update (){

		// is there a touch on screen?
		if (Input.touches.Length <= 0) {
			// no touches on screen

		}
		else { 
			// there are touches on screen

			// loop through all touches
			for (int i = 0; i < Input.touchCount; i++) {
				if(this.guiTexture.HitTest(Input.GetTouch(i).position)) {
					// the current touch is hitting our guiTexture
					if(Input.GetTouch(i).phase == TouchPhase.Began){
						// a finger began touching the screen

						// calls the function "OnTouchBegan
						this.SendMessage("OnTouchBegan");
						//Debug.Log("Touch began on:" + this.name);
					}
					if(Input.GetTouch(i).phase == TouchPhase.Ended){
						// a finger stopped touching the screen

						// calls the function "OnTouchEnd
						this.SendMessage("OnTouchEnd");
						//Debug.Log("Touch ended on:" + this.name);
					}
				}

				// outside of guiTexture

			}
		}

	}
}
