using UnityEngine;
using System.Collections;

public class SwipeJardin : MonoBehaviour {

	public float distCam;
	public Transform ObjectSwiped;

	private Ray ray;
	private RaycastHit hit;


	void Update () {
		if (Input.touches.Length > 0) {

			if (Input.touchCount > 1) {
				OnDragEnded(); 
				return;
			}
			if (Input.GetTouch(0).phase == TouchPhase.Began) {
				OnDragBegan();
			}
			if (Input.GetTouch(0).phase == TouchPhase.Moved) {
				OnDragMoved();
			}
			if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled) {
				OnDragEnded();
			}



		}
	}

	
	void OnDragBegan() {
		// && (hit.collider.gameObject.layer == draggable)

		ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
		if(Physics.Raycast(ray, out hit)) {
			ObjectSwiped = hit.transform;
			print("hit " + ObjectSwiped.transform.name);
			//distCam = hit.transform.position.z - Camera.main.transform.position.z;
			//startPos = new Vector3(pos.x, pos.y, distCam);
			//startPos = Camera.main.ScreenToWorldPoint(startPos);
		}
	}

	void OnDragMoved() {
		//Vector3 dragPos = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, distCam);
		//dragPos = Camera.main.ScreenToWorldPoint(dragPos);
		//ObjectSwiped.position = dragPos;

		ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
		if(Physics.Raycast(ray, out hit)) {
			ObjectSwiped = hit.transform;
			print("hit " + ObjectSwiped.transform.name);
			//distCam = hit.transform.position.z - Camera.main.transform.position.z;
			//startPos = new Vector3(pos.x, pos.y, distCam);
			//startPos = Camera.main.ScreenToWorldPoint(startPos);
		}
	}

	void OnDragEnded() {
		ObjectSwiped = null;
	}

}