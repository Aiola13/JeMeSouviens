using UnityEngine;
using System.Collections;

public class UIManager : TouchLogic {

	public GUITexture fleche;

	//public Texture2D noemie;
	public Texture2D skypi;
    public AudioClip miaulementSkypi;

	Ray ray ;
	RaycastHit hit;

	/*
	void Start() {
		NePasAfficherTexture(fleche);

        //AudioSource sourceAudio = gameObject.AddComponent<AudioSource>();
        //audio.clip = miaulementSkypi;

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
*/

	public override void OnTouchEndedAnywhere () {

		ray = Camera.main.ScreenPointToRay(Input.touches[0].position);


		// State preparer pate
		if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.preparationPate) {
			GameManagerCrepe.aValide = false;
		}
		// State quete noemie
		if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.queteNoemie) {
			CameraZoomIn();
			ChangeState(GameManagerCrepe.GameState.queteNoemie, GameManagerCrepe.GameState.preparationPate);
			AfficherTexture(fleche);

		}
		// State aide de skypi
		if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.aideDeSkypi) {
			ChangeState(GameManagerCrepe.GameState.aideDeSkypi, GameManagerCrepe.prevGameState);
		}

		// if we touch the cat
		if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.tag == "Skypi")) {
			ChangeState(GameManagerCrepe.GameState.preparationPate, GameManagerCrepe.GameState.aideDeSkypi);
            //audio.Play();
		}
	}


	
	void CameraZoomIn () {
		// interpolation pour aller plus près du plan de travail
		float temps = 1000.0f;
		Vector3 posArrive = new Vector3(0, 2, -3);
		Camera.main.transform.position = Vector3.Lerp(transform.position, posArrive, temps);
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
