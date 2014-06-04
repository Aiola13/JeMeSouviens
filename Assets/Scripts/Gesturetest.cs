using UnityEngine;
using System.Collections;

public class Gesturetest : MonoBehaviour {

	public bool drawww = false;
	
	// Update is called once per frame
	void Update () {

		if(Gesture.canDraw)
			drawww = true;
		else
			drawww = false;



		if (Input.GetButtonUp("Jump")) {
			Gesture.NewSymbol();
			Gesture.canDraw = true;
		}
	}
}
