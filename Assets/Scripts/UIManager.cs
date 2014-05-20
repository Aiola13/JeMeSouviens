using UnityEngine;
using System.Collections;

public class UIManager : TouchLogic {


	public GUITexture fleche;

	void Start() {
		NePasAfficherTexture(fleche);
	}
	
	public override void OnTouchEnded () {
		print ("appuiiii");
		NePasAfficherTexture(fleche);
		// interpolation pour aller plus près du plan de travail
		float temps = 1000.0f;
		Vector3 posArrive = new Vector3(0, 2, -3);
		Camera.main.transform.position = Vector3.Lerp(transform.position, posArrive, temps);

		// Passage à létat suivant
		GameManagerCrepe.curGameState = GameManagerCrepe.GameState.preparationPate;
		GameManagerCrepe.prevGameState = GameManagerCrepe.GameState.queteNoemie;
	}



	// active et affiche la texture t
	public void AfficherTexture(GUITexture t) {
		t.guiTexture.enabled = true;
		t.gameObject.SetActive(true);
	}

	// désactive et enleve l'affichage de la texture t
	public void NePasAfficherTexture(GUITexture t) {
		t.guiTexture.enabled = false;
		t.gameObject.SetActive(false);
	}
}
