using UnityEngine;
using System.Collections;

public class MainMenu : TouchLogic {

	public GUITexture jouer;
	public GUITexture quitter;

    public override void OnTouchEndedAnywhere() {

		if (Input.touchCount == 1) {
			if (jouer.HitTest(Input.GetTouch(0).position)) {
				Application.LoadLevel("a_menu");
			}

			else if (quitter.HitTest(Input.GetTouch(0).position)) {
				Application.Quit();
			}
		}

	}

}
