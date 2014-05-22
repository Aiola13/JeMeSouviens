using UnityEngine;
using System.Collections;

public class UIDialogueManager : TouchLogic {
	
	public GUITexture fleche;
	
	Vector3 camPreparationPatePos = new Vector3(0, 2, -3);
	Vector3 camCuissonPos = new Vector3(-2.7f, 2, -3);
	
	Ray ray ;
	RaycastHit hit;
	
	
	void Start() {
		//NePasAfficherTexture(fleche);
		
	}
	
	public override void OnTouchEndedAnywhere () {
		ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
		
		// si on est en train de preparer la pate et qu'on a appuyer n'importe sur le bouton de validation
		if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.preparationPate && GameManagerCrepe.boutonValidation == true) {
			GameManagerCrepe.boutonValidation = false;
			
			// si on a reussit la recette on passe a l'etalage du beurer
			if (GameManagerCrepe.queteCrepe.queteAccomplie()) {
				ChangeState(GameManagerCrepe.GameState.preparationPate, GameManagerCrepe.GameState.etalerLeBeurre);
				CameraMove(camCuissonPos);
			}
		}
		
		// si on est en train de preparer la pate et qu'on a appuyer n'importe ou
		if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.preparationPate) {
			GameManagerCrepe.boutonValidation = false;
		}
		
		// State quete noemie
		if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.queteNoemie) {
			ChangeState(GameManagerCrepe.GameState.queteNoemie, GameManagerCrepe.GameState.preparationPate);
			CameraMove(camPreparationPatePos);
			AfficherTexture(fleche);
		}
		
		// State aide de skypi
		if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.aideDeSkypi) {
			ChangeState(GameManagerCrepe.GameState.aideDeSkypi, GameManagerCrepe.prevGameState);
		}
		
		// if we touch the cat
		if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.tag == "Skypi")) {
			ChangeState(GameManagerCrepe.GameState.preparationPate, GameManagerCrepe.GameState.aideDeSkypi);
            GameManagerCrepe.miaulement.Play();
		}
	}
	
	
	
	
	
	
	void CameraMove (Vector3 pos) {
		// interpolation pour aller plus près du plan de travail
		float temps = 1000.0f;
		Camera.main.transform.position = Vector3.Lerp(transform.position, pos, temps);
	}
	
	// Parameters: prev State, curr State
	void ChangeState (GameManagerCrepe.GameState prev, GameManagerCrepe.GameState current) { 
		GameManagerCrepe.curGameState = current;
		GameManagerCrepe.prevGameState = prev;
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
