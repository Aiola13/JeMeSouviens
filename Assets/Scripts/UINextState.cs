using UnityEngine;
using System.Collections;

public class UINextState : TouchLogic {
	
	public GUITexture fleche;
	
	void Start() {

	}
	
	public override void OnTouchEnded () {
		if (name == "GUI_NextState") {
			GameManagerCrepe.nextState = true;
		}
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
