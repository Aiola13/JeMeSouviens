using UnityEngine;
using System.Collections;

public class DialogueCrepe : TouchLogic {
	
	public GUITexture texValidation;
	
	Vector3 camPreparationPatePos = new Vector3(0, 2, -3);
	Vector3 camCuissonPos = new Vector3(-2.7f, 2, -3);

    protected Ray ray;
	protected RaycastHit hit;
    public static bool canRestartChrono = false;

	void Start() {
        GameManager.AfficherTexture(texValidation);
		GameObject.Find("validerText").guiText.enabled = false;
	}
	
	public override void OnTouchEndedAnywhere () {

		// State quete noemie
		if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.queteNoemie) {
			ChangeState(GameManagerCrepe.GameState.queteNoemie, GameManagerCrepe.GameState.preparationPate);
			CameraMove(camPreparationPatePos);
            GameManager.AfficherTexture(texValidation);
			GameObject.Find("validerText").guiText.enabled = true;
            GameManager.ActiverDrag();
            canRestartChrono = true;
		}
		
		// State aide de skypi
		else if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.aideDeSkypi) {
			ChangeState(GameManagerCrepe.GameState.aideDeSkypi, GameManagerCrepe.prevGameState);
            GameManager.ActiverDrag();
            canRestartChrono = true;
		}
		
		// si on est en train de preparer la pate et qu'on a appuyer n'importe sur le bouton de validation
        else if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.preparationPate && GameManager.boutonValidation == true) {
            GameManager.boutonValidation = false;
			
			// si on a reussit la recette on passe a l'etalage du beurre
			if (GameManagerCrepe.queteCrepe.queteAccomplie()) {
				ChangeState(GameManagerCrepe.GameState.etalerLeBeurre, GameManagerCrepe.GameState.etalerLeBeurre);
				CameraMove(camCuissonPos);
				RotateCat();
			}
            GameManager.ActiverDrag();
            canRestartChrono = true;
		}
		
		// si on est en train de preparer la pate et qu'on a appuyer n'importe ou
		else if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.preparationPate) {
            GameManager.boutonValidation = false;
            canRestartChrono = true;
		}
		
		// State etalage beurre
		else if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.etalerLeBeurre) {
            canRestartChrono = true;
		}

		// State cuisson
		else if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.cuissonCrepe) {
            canRestartChrono = true;
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
