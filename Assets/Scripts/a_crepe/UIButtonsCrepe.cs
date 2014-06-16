using UnityEngine;
using System.Collections;

public class UIButtonsCrepe : TouchLogic {

	public GUITexture texMenu;
	public GUITexture texValidation;
	public GUITexture texNextState;
	
	void Awake() {
		GameManager.NePasAfficherTexture(texValidation);
		GameObject.Find("validerText").guiText.enabled = false;
	}

	public override void OnTouchBegan() {
		if (name == "validerButton") {
            GameManager.DesactiverDrag();
		}
		if (name == "menuButton") {
			Application.LoadLevel("menu");
		}
	}

	public override void OnTouchEnded () {
		if (name == "validerButton") {
			GameManager.boutonValidation = true;
		}

		if (name == "GUI_NextState") {
            GameManager.nextState = true;
		}
	}

}
