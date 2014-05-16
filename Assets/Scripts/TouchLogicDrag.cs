using UnityEngine;
using System.Collections;

public class TouchLogicDrag : MonoBehaviour {
	
	private GUIText guiInfo;
	
	public static int lastTouch = 0;			// so other scripts can know what was the last touch on screen
	
	public float distCam;
	public bool dragging = false;
	public Transform ObjectToDrag;
	//private Vector3 startPos;
	private int draggable = 8;					// number of the draggable layer
	
	private Vector4 dropBox;					// x, y, w, h  -- x,y respects GUI coordinates (origin top left and y grows down)
	

	void Start () {
		//guiInfo = GameObject.Find("Info_drop").guiText;
		//guiInfo.text = "dropBox x: " + dropBox.x + " y: " + dropBox.y + " w: " + dropBox.z + " h: " + dropBox.w;
	}

	void OnGUI() {
		dropBox = new Vector4( Screen.width/3, Screen.height - Screen.height/3, Screen.width/3, Screen.height/3);
		GUI.Box(new Rect(dropBox.x, dropBox.y, dropBox.z, dropBox.w), "Allowed Drop");
	}


	public void Update () {
		if (Input.touches.Length > 0) {
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
		}
	}
	
	#region methods for Dragging
	public void OnDragBegan() {
		Touch touch = Input.touches[0]; // we allow the drag only with the first finger touched
		Vector3 pos = touch.position;
		
		Ray ray = Camera.main.ScreenPointToRay(pos);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit) && (hit.collider.gameObject.layer == draggable)) {
			ObjectToDrag = hit.transform;
			distCam = hit.transform.position.z - Camera.main.transform.position.z;
			//startPos = new Vector3(pos.x, pos.y, distCam);
			//startPos = Camera.main.ScreenToWorldPoint(startPos);
			dragging = true;
		}
	}
	public void OnDragMoved() {
		Vector3 dragPos = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, distCam);
		dragPos = Camera.main.ScreenToWorldPoint(dragPos);
		ObjectToDrag.position = dragPos;
	}
	public void OnDragEnded() {
		dragging = false;
		if (CheckPos()) {
			// the object was dragged to the right place
		}
		else {
			// the object was dragged to the wrong place, we reset its position
			ResetPosition();
		}
		ObjectToDrag = null;
	}
	
	
	public void ResetPosition() {
		ObjectToDrag.position = ObjectToDrag.GetComponent<IngredientOriginalPos>().originalPos;
	}
	
	public bool CheckPos() {
		Vector2 fingerPos = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
		fingerPos.y = Screen.height - fingerPos.y; // we convert the finger coordinates into gui coordinates
		
		// Is the finger on the dropBox?
		if ( (fingerPos.x > dropBox.x && fingerPos.x < (dropBox.x + dropBox.z)) &&
		    (fingerPos.y > dropBox.y && fingerPos.y < (dropBox.y + dropBox.w)) ) {
			return true;
		}
		else
			return false;
	}
	#endregion

}
