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

						// calls the function "OnTouchBegan"
						this.SendMessage("OnTouchBegan");
					}
					if(Input.GetTouch(i).phase == TouchPhase.Ended){
					// A finger was lifted from the screen

						// calls the function "OnTouchEnd"
						this.SendMessage("OnTouchEnd");
					}
					if(Input.GetTouch(i).phase == TouchPhase.Stationary){
					// A finger is touching the screen but hasn't moved
						
						// calls the function "OnTouchStationary"
						this.SendMessage("OnTouchStationary");
					}
					if(Input.GetTouch(i).phase == TouchPhase.Moved){
						// A finger moved on the screen.
						
						// calls the function "OnTouchMoved"
						this.SendMessage("OnTouchMoved");
					}
				}

				// outside of guiTexture

			}
		}

	}
}
