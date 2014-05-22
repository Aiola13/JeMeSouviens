using UnityEngine;
using System.Collections;

public class UIDialogueManager : TouchLogic {
	
	public GUITexture texValidation;
	
	Vector3 camPreparationPatePos = new Vector3(0, 2, -3);
	Vector3 camCuissonPos = new Vector3(-2.7f, 2, -3);
	
	Ray ray ;
	RaycastHit hit;

	void Start() {
		AfficherTexture(texValidation);
	}
	
	public override void OnTouchEndedAnywhere () {
		print("anywhere");

		// State quete noemie
		if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.queteNoemie) {
			ChangeState(GameManagerCrepe.GameState.queteNoemie, GameManagerCrepe.GameState.preparationPate);
			CameraMove(camPreparationPatePos);
			AfficherTexture(texValidation);
			ActiverDrag();
		}
		
		// State aide de skypi
		if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.aideDeSkypi) {
			ChangeState(GameManagerCrepe.GameState.aideDeSkypi, GameManagerCrepe.prevGameState);
			ActiverDrag();
		}
		
		// si on est en train de preparer la pate et qu'on a appuyer n'importe sur le bouton de validation
		if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.preparationPate && GameManagerCrepe.boutonValidation == true) {
			GameManagerCrepe.boutonValidation = false;
			
			// si on a reussit la recette on passe a l'etalage du beurre
			if (GameManagerCrepe.queteCrepe.queteAccomplie()) {
				ChangeState(GameManagerCrepe.GameState.etalerLeBeurre, GameManagerCrepe.GameState.etalerLeBeurre);
				CameraMove(camCuissonPos);
				RotateCat();
			}
			ActiverDrag();
		}
		
		// si on est en train de preparer la pate et qu'on a appuyer n'importe ou
		if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.preparationPate) {
			GameManagerCrepe.boutonValidation = false;
		}
		
		// State etalage beurre
		if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.etalerLeBeurre) {

		}

		// State cuisson
		if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.cuissonCrepe) {

		}

		// if we touch the cat
		ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
		if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.tag == "Skypi")) {
			DesactiverDrag();
			switch (GameManagerCrepe.curGameState) {
				case GameManagerCrepe.GameState.preparationPate:
					ChangeState(GameManagerCrepe.GameState.preparationPate, GameManagerCrepe.GameState.aideDeSkypi);
					break;
				case GameManagerCrepe.GameState.etalerLeBeurre:
					ChangeState(GameManagerCrepe.GameState.etalerLeBeurre, GameManagerCrepe.GameState.aideDeSkypi);
					break;
				case GameManagerCrepe.GameState.cuissonCrepe:
					ChangeState(GameManagerCrepe.GameState.cuissonCrepe, GameManagerCrepe.GameState.aideDeSkypi);
					break;
			}
            GameManagerCrepe.miaulement.Play();
		}
	}
	
	

	void ActiverDrag() {
		Camera.main.GetComponent<TouchLogicDrag>().enabled = true;
	}
	void DesactiverDrag() {
		Camera.main.GetComponent<TouchLogicDrag>().enabled = false;
	}
	
	void RotateCat() {
		Transform t = GameObject.FindGameObjectWithTag("Skypi").transform;
		t.Rotate(t.rotation.x, -75, t.rotation.z);
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
