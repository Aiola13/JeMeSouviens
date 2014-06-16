using UnityEngine;
using System.Collections;

public class DialogueCrepe : TouchLogic {
	
	Vector3 camPreparationPatePos = new Vector3(0, 2, -3);
	Vector3 camCuissonPos = new Vector3(-2.7f, 2, -3);

    protected Ray ray;
	protected RaycastHit hit;

	void Start() {

	}
	
	public override void OnTouchEndedAnywhere () {

		// State quete noemie
		if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.queteNoemie) {
			CameraMove(camPreparationPatePos);
            GameManager.ActiverDrag();
			ChangeState(GameManagerCrepe.GameState.queteNoemie, GameManagerCrepe.GameState.preparationPate);
		}
		
		// State aide de skypi
		if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.aideDeSkypi) {
            GameManager.ActiverDrag();
			ChangeState(GameManagerCrepe.GameState.aideDeSkypi, GameManagerCrepe.prevGameState);
		}
		
		// si on est en train de preparer la pate et qu'on a appuyer n'importe sur le bouton de validation
        if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.preparationPate && GameManager.boutonValidation == true) {
            GameManager.boutonValidation = false;
			
			// si on a reussit la recette on passe a l'etalage du beurre
			if (GameManagerCrepe.queteCrepe.queteAccomplie()) {
				ChangeState(GameManagerCrepe.GameState.etalerLeBeurre, GameManagerCrepe.GameState.etalerLeBeurre);
				CameraMove(camCuissonPos);
				RotateCat();
			}
            GameManager.ActiverDrag();
		}
		
		// si on est en train de preparer la pate et qu'on a appuyer n'importe ou
		if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.preparationPate) {
            GameManager.boutonValidation = false;
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
            GameManager.DesactiverDrag();
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

    protected void RotateCat() {
		Transform t = GameObject.FindGameObjectWithTag("Skypi").transform;
		t.Rotate(t.rotation.x, -75, t.rotation.z);
	}

    protected void CameraMove(Vector3 pos) {
		// interpolation pour aller plus près du plan de travail
		float temps = 1000.0f;
		Camera.main.transform.position = Vector3.Lerp(transform.position, pos, temps);
	}
	
	// Parameters: prev State, curr State
    protected void ChangeState(GameManagerCrepe.GameState prev, GameManagerCrepe.GameState current) { 
		GameManagerCrepe.curGameState = current;
		GameManagerCrepe.prevGameState = prev;
	}
}
