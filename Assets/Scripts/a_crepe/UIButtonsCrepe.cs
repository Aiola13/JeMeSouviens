using UnityEngine;
using System.Collections;

public class UIButtonsCrepe : TouchLogic {

	public GUITexture texMenu;
	

	public override void OnTouchBegan() {
		if (name == "menuButton") {
			Application.LoadLevel("menu");
		}
	}


}
