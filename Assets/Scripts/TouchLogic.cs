﻿using UnityEngine;
using System.Collections;

public class TouchLogic : MonoBehaviour {

	//private GUIText guiTextPos;
	
	public static int lastTouch = 0;			// so other scripts can know what was the last touch on screen

	void Start () {
		//guiTextPos = GameObject.Find("Info_pos").guiText;
		//guiTextPos.text = "Screen width: " + Screen.width + "  Screen height: " + Screen.height;
	}
	
	public virtual void Update () {
		// is there a touch on screen?
		if (Input.touches.Length <= 0) {
		// no touches on screen
			OnNoTouches();
		}
		else { 
		// there are touches on screen
			// loop through all touches
			for (int i = 0; i < Input.touchCount; i++) {

				lastTouch = i;

				/* draw a ray from screen to scene
				ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
				Debug.DrawRay (Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), new Vector3(0,0,100), Color.green);

				if (Physics.Raycast(ray, out rayHitInfo)) {
					if(rayHitInfo.transform.gameObject)
						print(rayHitInfo.transform.gameObject.name);
				}
				*/

				/*
				if (guiTextPos) {
					Vector3 curPos = Input.GetTouch(i).position;
					Vector3 realCurPos = Camera.main.ScreenToWorldPoint(new Vector3(curPos.x, curPos.y, 10));
					guiTextPos.text =  "ScreenPos   x: " + curPos.x + " y: " + curPos.y + " z: " + curPos.z + 
									"\nlastTouch: " + lastTouch + " phase: " + Input.GetTouch(i).phase + 
									"\nWorldPos   x: " + realCurPos.x + " y: " + realCurPos.y + " z: " + realCurPos.z;
				}
				*/

				#region Touches on guiTextures
				if(this.guiTexture != null && this.guiTexture.HitTest(Input.GetTouch(i).position)) {
				// the current touch is hitting our guiTexture

					if(Input.GetTouch(i).phase == TouchPhase.Began){
					// A finger touched the screen
						OnTouchBegan();
					}
					if(Input.GetTouch(i).phase == TouchPhase.Ended){
					// A finger was lifted from the screen
						OnTouchEnded();
					}
					if(Input.GetTouch(i).phase == TouchPhase.Moved){
						// A finger moved on the screen.
						OnTouchMoved();
					}
					if(Input.GetTouch(i).phase == TouchPhase.Stationary){
					// A finger is touching the screen but hasn't moved
						OnTouchStationary();
					}
				}
				#endregion


				#region Touches outside of guiTextures
				if(Input.GetTouch(i).phase == TouchPhase.Began){
					// A finger touched somewhere on the screen
					OnTouchBeganAnywhere();
				}
				if(Input.GetTouch(i).phase == TouchPhase.Ended){
					// A finger was lifted from anywhere on the screen
					OnTouchEndedAnywhere();
				}
				if(Input.GetTouch(i).phase == TouchPhase.Moved){
					// A finger moved on the screen somewhere.
					OnTouchMovedAnywhere();
				}
				if(Input.GetTouch(i).phase == TouchPhase.Stationary){
					// A finger is touching the screen somewhere but hasn't moved
					OnTouchStationaryAnywhere();
				}
				#endregion
			}
		}
	}

	#region methods for Touches on guiTextures
	public virtual void OnNoTouches() {
		//print (name + " is not using OnNoTouch()");
	}
	public virtual void OnTouchBegan() {
		//print (name + " is not using OnTouchBegan()");
	}
	public virtual void OnTouchEnded() {
		//print (name + " is not using OnTouhchEnd()");
	}
	public virtual void OnTouchStationary() {
		//print (name + " is not using OnTouchStationary()");
	}
	public virtual void OnTouchMoved() {
		//print (name + " is not using OnTouchMoved()");
	}
	#endregion

	#region methods for Touches outside of guiTextures
	public virtual void OnTouchBeganAnywhere() {
		//print (name + " is not using OnTouchBeganAnywhere()");
	}
	public virtual void OnTouchEndedAnywhere() {
		//print (name + " is not using OnTouchEndAnywhere()");
	}
	public virtual void OnTouchMovedAnywhere() {
		//print (name + " is not using OnTouchMovedAnywhere()");
	}
	public virtual void OnTouchStationaryAnywhere() {
		//print (name + " is not using OnTouchStationaryAnywhere()");
	}
	#endregion
}
