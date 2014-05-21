using UnityEngine;
using System.Collections;

public class UIManager : TouchLogic {

	public GUITexture fleche;

	GUIStyle style = new GUIStyle ();
	private int brd = Screen.height/100;

	public Texture2D Tex_dialogue;
	public Texture2D noemie;
	public Texture2D skypi;

	Ray ray ;
	RaycastHit hit;


	void Start() {
		NePasAfficherTexture(fleche);
	}

	public override void OnTouchBegan () {

		// on check si la recette est correcte ou pas a l'appui du bouton de validation
		if (name == "GUI_Fleche") {
			// si la quete est reussie
			if (GameManagerCrepe.queteCrepe.queteAccomplie()) {
				//AfficherDialogue(skypi, "Tu as parfaitement réussie la recette!");
			}
			// si la recette a mal été suivie
			else {
				print ("not ok");
				this.AfficherDialogue(skypi, "Tu t'es trompé dans la recette.");
			}
			//uiManager.AfficherDialogue(noemie, queteCrepe.texteQuete());

		}
	}

	public override void OnTouchEndedAnywhere () {

		Touch touch = Input.touches[0];
		Vector3 pos = touch.position;

		ray = Camera.main.ScreenPointToRay(pos);

		if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.queteNoemie) {
			CameraZoomIn();
			ChangeState(GameManagerCrepe.GameState.queteNoemie, GameManagerCrepe.GameState.preparationPate);
			AfficherTexture(fleche);
		}
		if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.aideDeSkypi) {
			ChangeState(GameManagerCrepe.GameState.aideDeSkypi, GameManagerCrepe.prevGameState);
		}

		
		if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.tag == "Skypi")) {
			ChangeState(GameManagerCrepe.GameState.preparationPate, GameManagerCrepe.GameState.aideDeSkypi);
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
	
	// affiche une boite de dialogue avec comme texture pnj et comme texte txt
	public void AfficherDialogue (Texture2D pnj, string txt) { 
		style.fontSize = Screen.height/36;
		style.alignment = TextAnchor.MiddleLeft;
		style.font = (Font)Resources.Load("Roboto-Regular");
		
		GUI.DrawTexture(new Rect(brd, Screen.height*2/3, Screen.width-brd*2, Screen.height/3 -brd), Tex_dialogue, ScaleMode.StretchToFill, true, 0);
		GUI.DrawTexture(new Rect(brd*2, Screen.height*2/3+brd, Screen.width/5, Screen.height/3 -brd*3), pnj, ScaleMode.ScaleToFit, true, 0);
		GUI.Box(new Rect(Screen.width*1/4 , Screen.height*7/10+brd*2, Screen.width-20, Screen.height/5 -10), txt, style);
		
		//style.alignment = TextAnchor.MiddleCenter;
		style.fontSize = Screen.height/28;
		GUI.Box(new Rect(Screen.width*2/3 , Screen.height*2/3+brd*2, Screen.width/10, Screen.height/3 -10), "TOUCHER POUR CONTINUER !", style);
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
