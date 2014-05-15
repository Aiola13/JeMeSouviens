using UnityEngine;
using System.Collections;

public class DragTest : MonoBehaviour, I3dObjectTouchable {

	public float camDepth;
	public bool isDragging = false;
	public int lastTouch = 0;			// we allow the drag only with the first finger touched

	public Vector3 curPos;
	public Vector3 startPos;
	public Vector3 endPos;

	private Transform toDrag;
	
	

	public void Update() {

		if (isDragging) {
			transform.position = new Vector3(curPos.x, curPos.y, transform.position.z);
		}
	}

	public void OnTouchBegan() {
		print (name + " begin in OnTouchBegan");
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(lastTouch).position);
		if (Physics.Raycast(ray, out hit) && (hit.collider.tag == "Draggable")){
			print("hhhh");
			
//			toDrag = hit.transform;
//				camDepth = hit.transform.position.z - Camera.main.transform.position.z;
//				v3 = new Vector3(pos.x, pos.y, camDepth);
//				v3 = Camera.main.ScreenToWorldPoint(v3);
//				offset = toDrag.position - v3;
//				dragging = true;
			
			startPos = new Vector3(hit.point.x, hit.point.y, transform.position.z);
			curPos = startPos;
			isDragging = true;
		}
	}
	
	public void OnTouchEnded() {
		print (name + " end in OnTouchEnded");

		if (isDragging)
			isDragging = false;
		
		/*
		isDragging = false;
		if (CheckPos()) {
			// the object was dragged to the right place
			
		}
		else {
			// the object was dragged to the wrong place, we reset its position
			ResetPosition();
		}
		*/
	}

	public void OnTouchMoved() {
		Vector3 touchPos = new Vector3(Input.GetTouch(lastTouch).position.x, Input.GetTouch(lastTouch).position.y, camDepth);
		curPos = Camera.main.ScreenToWorldPoint(touchPos);
	}

	public void OnTouchStationary() {}



	public void OnTouchEndedAnywhere() {
	/*
		print (name + " end in OnTouchEndedAnywhere");
		isDragging = false;

		if (CheckPos()) {
		// the object was dragged to the right place

		}
		else {
		// the object was dragged to the wrong place, we reset its position
			ResetPosition();
		}
	*/
	}

	public void OnTouchMovedAnywhere() {
		//Vector3 touchPos = new Vector3(Input.GetTouch(lastTouch).position.x, Input.GetTouch(lastTouch).position.y, camDepth);
		//curPos = Camera.main.ScreenToWorldPoint(touchPos);
	}

	public void OnTouchStationaryAnywhere() {}



	public void ResetPosition() {
		transform.position = startPos;
	}
	
	public bool CheckPos() {
		Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(lastTouch).position);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)){
			endPos = new Vector3(hit.point.x, hit.point.y, transform.position.z);
		}

		if (endPos.x > 7)
			return true;
		else
			return false;
	}
}
