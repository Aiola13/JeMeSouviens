using UnityEngine;
using System.Collections;

public class SwipeAndDragJardin : TouchLogic {

	public bool dragging = false;			// used to check if we are dragging a vegetable during plantation phase 1 or 2
	public bool swiping = false;			// used to check if we are swiping
	public Transform ObjectToDrag;
	public Transform ObjectSwiped;

	private Ray ray;
	private RaycastHit hit;


	void Update () {
		if (Input.touches.Length > 0) {

			/*

			#region Dragging
			if (dragging && Input.touchCount > 1) {
				OnDragEnded(); 
				return;
			}
			if(Input.GetTouch(0).phase == TouchPhase.Began) {
				OnDragBegan();
			}
			if (dragging && Input.GetTouch(0).phase == TouchPhase.Moved) {
				OnDragMoved();
				
			}
			if (dragging && (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)) {
				OnDragEnded();
			}
			#endregion

			#region swiping
			if (Input.touchCount > 1) {
				OnSwipeEnded(); 
				return;
			}
			if (Input.GetTouch(0).phase == TouchPhase.Began) {
				OnSwipeBegan();
			}
			if (Input.GetTouch(0).phase == TouchPhase.Moved) {
				OnSwipeMoved();
			}
			if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled) {
				OnSwipeEnded();
			}
			#endregion
			*/
		}
	}




	/*
	void OnSwipeBegan() {

		ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
		if(Physics.Raycast(ray, out hit)) {
			ObjectSwiped = hit.transform;
			print("hit " + ObjectSwiped.transform.name);
		}
	}

	void OnSwipeMoved() {

		ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
		if(Physics.Raycast(ray, out hit)) {
			ObjectSwiped = hit.transform;
			print("hit " + ObjectSwiped.transform.name);
		}
	}

	void OnSwipeEnded() {
		ObjectSwiped = null;
	}
*/
}