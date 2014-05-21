using UnityEngine;
using System.Collections;

public class UIValidation : TouchLogic {

	public GUITexture fleche;

	void Start() {
		NePasAfficherTexture(fleche);
	}

	public override void OnTouchEnded () {
		
		// on check si la recette est correcte ou pas a l'appui du bouton de validation
		if (name == "GUI_Fleche") {
			// si la quete est reussie
			if (GameManagerCrepe.queteCrepe.queteAccomplie()) {
				ChangeState(GameManagerCrepe.GameState.preparationPate, GameManagerCrepe.GameState.etalerLeBeurre);
			}
			// si la recette a mal été suivie
			else {
				
			}
			GameManagerCrepe.aValide = true;
		}
	}

	// prev then current in parameters
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
