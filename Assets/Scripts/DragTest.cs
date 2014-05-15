using UnityEngine;
using System.Collections;

public class DragTest : MonoBehaviour, I3dObjectTouchable {

	//Camera.main.backgroundColor = Color.red;

	public Vector3 curPos;
	public Vector3 startPos;
	public Vector3 endPos;

	public bool isDragging = false;

	private Ray ray;
	private RaycastHit hit = new RaycastHit(); 

	public void Update() {

		if (isDragging) {
			this.transform.position = new Vector3(curPos.x, curPos.y, this.transform.position.z);
		}
	}

	public void OnTouchBegan() {
		isDragging = true;
		ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
		if (Physics.Raycast(ray, out hit)){
			startPos = new Vector3(hit.point.x, hit.point.y, this.transform.position.z);
			curPos = startPos;
		}
	}
	
	public void OnTouchEnded() {
		isDragging = false;

		if (CheckPos()) {
			// the object was dragged to the right place
			
		}
		else {
			// the object was dragged to the wrong place, we reset its position
			ResetPosition();
		}
	}

	public void OnTouchMoved() {
		Vector3 touchPos = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 10);
		curPos = Camera.main.ScreenToWorldPoint(touchPos);
	}

	public void OnTouchStationary() {}

	public void OnTouchEndedAnywhere() {
		isDragging = false;

		if (CheckPos()) {
		// the object was dragged to the right place

		}
		else {
		// the object was dragged to the wrong place, we reset its position
			ResetPosition();
		}
	}

	public void OnTouchMovedAnywhere() {
		Vector3 touchPos = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 10);
		curPos = Camera.main.ScreenToWorldPoint(touchPos);
	}

	public void OnTouchStationaryAnywhere() {}


	public void ResetPosition() {
		this.transform.position = startPos;
	}

	public bool CheckPos() {
		ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
		if (Physics.Raycast(ray, out hit)){
			endPos = new Vector3(hit.point.x, hit.point.y, this.transform.position.z);
		}

		if (endPos.x > 7)
			return true;
		else
			return false;
	}
}
