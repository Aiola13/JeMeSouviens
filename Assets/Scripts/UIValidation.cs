using UnityEngine;
using System.Collections;

public class UIValidation : TouchLogic {
	
	public GUITexture texValidation;
	public GUITexture texNextState;
	
	void Start() {
		NePasAfficherTexture(texValidation);
	}

	public override void OnTouchBegan() {
		if (name == "GUI_Validation") {
			DesactiverDrag();
			print ("desactive");
		}
	}

	public override void OnTouchEnded () {
		if (name == "GUI_Validation") {
			GameManagerCrepe.boutonValidation = true;
		}

		if (name == "GUI_NextState") {
			GameManagerCrepe.nextState = true;
		}
	}
	
	
	void ActiverDrag() {
		Camera.main.GetComponent<TouchLogicDrag>().enabled = true;
	}
	void DesactiverDrag() {
		Camera.main.GetComponent<TouchLogicDrag>().enabled = false;
	}
	
	// Parameters: prev State, curr State
	void ChangeState (GameManagerCrepe.GameState prev, GameManagerCrepe.GameState current) { 
		GameManagerCrepe.curGameState = current;
		GameManagerCrepe.prevGameState = prev;
	}
	
	// active et affiche la texture t
	void AfficherTexture(GUITexture t) {
		t.guiTexture.enabled = true;
		t.gameObject.SetActive(true);
	}
	
	// désactive et enleve l'affichage de la texture t
	void NePasAfficherTexture(GUITexture t) {
		t.guiTexture.enabled = false;
		t.gameObject.SetActive(false);
	}
}
