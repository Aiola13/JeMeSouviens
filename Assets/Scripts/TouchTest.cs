using UnityEngine;
using System.Collections;

public class TouchTest : TouchLogic {

	public Transform myCube;
	public float dir = 0.0f;
	public bool canTouch = true;


	public override void Update() {
		if (canTouch) {
		// we can touch the screen
			base.Update();
		}
	}

	public override void OnTouchBegan() {
		if (name == "Right Button") {
		// move right
			dir = 1.0f;
		}
		else if (name == "Left Button") {
		// move left
			dir = -1.0f;
		}
		else if (name == "Enable Touch") {
		// we enable/disable touches for all GUItexture except the one that allow us to switch between states
			DisableTouch ();
		}
	}

	public override void OnTouchEnded () {
		dir = 0.0f;
	}

	public override void OnTouchStationary () {
		myCube.Translate(10*Time.deltaTime*dir,0,0);
	}


	void DisableTouch () {
	// enable/disable touches for all GUItexture except the one that allow us to switch between states
		GUITexture[] guiTex = FindObjectsOfType(typeof(GUITexture)) as GUITexture[];
		foreach (GUITexture tex in guiTex) {
			if (tex.name != "Enable Touch") {
				tex.GetComponent<TouchTest>().canTouch = !(tex.GetComponent<TouchTest>().canTouch);
			}
		}
	}

}
