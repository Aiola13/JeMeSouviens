using UnityEngine;
using System.Collections;

public class TouchLogicDrag : TouchLogic {

//	public override void Update() {
//		base.Update();
//	}


	public override void OnDragBegan() {
		if (tag == "CubeRouge")
			print ("drag began");
	}
	
	public override void OnDragMoved () {
		print ("drag move");
		Camera.main.backgroundColor = Color.green;
	}
	
	public override void OnDragEnded () {
		print ("drag ended");
		Camera.main.backgroundColor = Color.yellow;
	}
	
}
