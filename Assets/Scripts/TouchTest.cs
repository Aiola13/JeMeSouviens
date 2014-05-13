using UnityEngine;
using System.Collections;

public class TouchTest : TouchLogic {
	
	void OnTouchBegan () {
		Debug.Log("Touch began on:" + this.name);
		Camera.main.backgroundColor = Color.black;
	}

	void OnTouchEnd () {
		Debug.Log("Touch ended on:" + this.name);
		Camera.main.backgroundColor = Color.green;
	}
}
