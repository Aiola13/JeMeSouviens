using UnityEngine;
using System.Collections;

public class TouchTest : TouchLogic {

	public Transform myCube;
	public float dir = 0.0f;
	public bool canTouch = true;

	void Update() {
		if (canTouch) {
		// we can touch the screen
			CheckTouches();
		}
	}

	void OnTouchBegan () {
		if (this.name == "Right Button") {
		// move right
			dir = 1.0f;
		}
		else if (this.name == "Left Button") {
		// move left
			dir = -1.0f;
		}
		else if (this.name == "Enable Touch") {
		// we enable/disable touches for all GUItexture except the one that allow us to switch between states
			GUITexture[] guiTex = FindObjectsOfType(typeof(GUITexture)) as GUITexture[];
			foreach (GUITexture tex in guiTex) {
				if (tex.name != "Enable Touch") {
					tex.GetComponent<TouchTest>().canTouch = !(tex.GetComponent<TouchTest>().canTouch);
				}
			}
			
		}

		Camera.main.backgroundColor = Color.red;
		myCube.Translate(10*Time.deltaTime*dir,0,0);
	}

	void OnTouchEnd () {
		Camera.main.backgroundColor = Color.blue;
		dir = 0.0f;
	}

	void OnTouchStationary () {
		myCube.Translate(10*Time.deltaTime*dir,0,0);
	}

	void OnTouchMoved () {

	} 
}
