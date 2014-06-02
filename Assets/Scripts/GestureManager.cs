using UnityEngine;
using System.Collections;

public class GestureManager : MonoBehaviour {

	Gesture _gesture;
	bool canDraw = false;

	void Awake () {
		_gesture = GetComponent<Gesture>();
		_gesture.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetButtonUp("Jump")) {
			// disable script
		    if (canDraw) {
				_gesture.enabled = false;
				canDraw = false;
			}
			// enable script
			else {
				_gesture.enabled = true;
				canDraw = true;
			}
		}
	}
}
