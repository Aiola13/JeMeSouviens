using UnityEngine;
using System.Collections;

public class UIButtonsCrepe : TouchLogic {

	public GUITexture texMenu;
	public GUITexture texRejouer;

	public override void OnTouchEnded() {
		if (name == "menuButton") {
			Application.LoadLevel("menu");
		}

		if (name == "rejouerButton") {
			Application.LoadLevel("a_crepe");
		}
	}


}
