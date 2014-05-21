using UnityEngine;
using System.Collections;

public class UIValidation : TouchLogic {
	
	public GUITexture fleche;
	
	void Start() {
		NePasAfficherTexture(fleche);
	}
	
	public override void OnTouchEnded () {
		if (name == "GUI_Fleche") {
			GameManagerCrepe.boutonValidation = true;
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
