using UnityEngine;
using System.Collections;

public class UIButtonsCrepe : TouchLogic {

	public GUITexture texReplay;
	public GUITexture texValidation;
	public GUITexture texNextState;
	
	void Start() {
		GameManager.NePasAfficherTexture(texValidation);
	}

	public override void OnTouchBegan() {
		if (name == "GUI_Validation") {
            GameManager.DesactiverDrag();
		}
		if (name == "GUI_Replay") {
			Application.LoadLevel("menu");
		}
	}

	public override void OnTouchEnded () {
		if (name == "GUI_Validation") {
			GameManager.boutonValidation = true;
		}

		if (name == "GUI_NextState") {
            GameManager.nextState = true;
		}
	}

}
