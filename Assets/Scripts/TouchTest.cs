using UnityEngine;
using System.Collections;

public class TouchTest : TouchLogic {

	public Transform myCube;
	public float dir = 0.0f;

	void OnTouchBegan () {
		//Debug.Log("Touch began on:" + this.name);
		if (this.name == "Right Button") {
			dir = 1.0f;
		}
		else if (this.name == "Left Button") {
			dir = -1.0f;
		}
		Camera.main.backgroundColor = Color.red;
		myCube.Translate(10*Time.deltaTime*dir,0,0);
	}

	void OnTouchEnd () {
		//Debug.Log("Touch ended on:" + this.name);
		Camera.main.backgroundColor = Color.blue;
		dir = 0.0f;
	}

	void OnTouchStationary () {
		//Debug.Log("Touch stationary on:" + this.name);
		myCube.Translate(10*Time.deltaTime*dir,0,0);
	}
}
