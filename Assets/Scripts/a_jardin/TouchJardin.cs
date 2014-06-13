using UnityEngine;
using System.Collections;

public class TouchJardin : TouchLogic {

	#region attributs
	// GUI legumes
	public GUITexture carotte;
	public GUITexture tomate;
	public GUITexture choux;
	public GUITexture aubergine;
	public GUITexture patate;
	public GUITexture oignon;

	// GUI other
	public GUITexture validation;
	public GUITexture info;

	// Parcelles
	private int parcelles = 8;					// number of the parcelle layer
	public GameObject selectedParcelle;

	// swipe creuser
	public float dist = 10;		// distance to register a swipe in any given direction
	private Vector2 fp ; 	 	// first finger position
	private Vector2 lp;  		// last finger position
	public int nbDig = 0;

	private Ray ray;
	private RaycastHit hit;
	#endregion

	void Start() {
		NePasAfficherUILegumes();
		NePasAfficherBoutons();
	}

	public override void OnTouchBeganAnywhere () {
		
		#region plantation on touch began
		// si on touche une parcelle
		if ((GameManagerJardin.curGameState == GameManagerJardin.GameState.planterP1) || 
		    (GameManagerJardin.curGameState == GameManagerJardin.GameState.planterP2)){

			ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
			if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.layer == parcelles)) {
				if (Input.touchCount == 1) {
					fp = Input.GetTouch(0).position;
					lp = Input.GetTouch(0).position;
				}
			}
		}
		#endregion
	}


	public override void OnTouchMovedAnywhere () {

		#region plantation on touch moved
		if ((GameManagerJardin.curGameState == GameManagerJardin.GameState.planterP1) || 
		    (GameManagerJardin.curGameState == GameManagerJardin.GameState.planterP2)){

			ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
			if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.layer == parcelles)) {

				if (Input.touchCount == 1) {
					lp = Input.GetTouch(0).position;
				}
			}
		}
		#endregion
	}


	public override void OnTouchEndedAnywhere () {

		#region DIALOGUES
		// quete 1
		if (GameManagerJardin.curGameState == GameManagerJardin.GameState.queteJessicaP1) {
			ChangeState(GameManagerJardin.GameState.queteJessicaP1, GameManagerJardin.GameState.planterP1);
			AfficherBoutons();
		}

		// dialogue 1 de la tansition 
		else if (GameManagerJardin.curGameState == GameManagerJardin.GameState.dialogueTransition1) {
			ChangeState(GameManagerJardin.GameState.dialogueTransition1, GameManagerJardin.GameState.transition);
		}

		// TEMPORARIRE
		// tansition 
		else if (GameManagerJardin.curGameState == GameManagerJardin.GameState.transition) {
			ChangeState(GameManagerJardin.GameState.transition, GameManagerJardin.GameState.dialogueTransition2);
		}
		
		// dialogue 2 de la tansition 2
		else if (GameManagerJardin.curGameState == GameManagerJardin.GameState.dialogueTransition2) {
			ChangeState(GameManagerJardin.GameState.dialogueTransition2, GameManagerJardin.GameState.queteJessicaP2);
		}

		// quete 2
		else if (GameManagerJardin.curGameState == GameManagerJardin.GameState.queteJessicaP2) {
			ChangeState(GameManagerJardin.GameState.queteJessicaP2, GameManagerJardin.GameState.planterP2);
			AfficherBoutons();
		}

		// score
		else if (GameManagerJardin.curGameState == GameManagerJardin.GameState.score) {
			// FAIRE UN RETOUR MENU ICI
			ChangeState(GameManagerJardin.GameState.score, GameManagerJardin.GameState.queteJessicaP1);
		}
		#endregion


		#region plantation on touch ended
		// si on touche une parcelle
		else if ((GameManagerJardin.curGameState == GameManagerJardin.GameState.planterP1) || 
		         (GameManagerJardin.curGameState == GameManagerJardin.GameState.planterP2)){

			// on vérifie si on a appuyé sur le bouton de validation
			ValiderLegumes();

			// on vérifie si on a appuyé sur le bouton d'info
			AfficherLegumesPlantes();


			// lorsqu'on appui sur une parcelle
			ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
			if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.layer == parcelles)) {

				GameObject parcelle = hit.collider.gameObject;
				Parcelle scriptParcelle = parcelle.GetComponent<Parcelle>();

				// si on a selectionne avant une parcelle differente de celle touché actuellement, on met son etat en deselectionné
				if (selectedParcelle != null && selectedParcelle.tag != parcelle.tag) {
					selectedParcelle.GetComponent<Parcelle>().AEteDeSelectionne();
					NePasAfficherUILegumes();
				}

				// la nouvelle parcelle selectionné est celle qu'on a touché
				selectedParcelle = parcelle;
				selectedParcelle.tag = parcelle.tag;

	
				// la parcelle change d'etat seulement si une interaction se fait dessus quand elle est selectionné
				if (scriptParcelle.isSelected) {
					if (scriptParcelle._curState == Parcelle.ParcelleState.creuser) {
						// faire ici un swipe pour creuser

						// swipe action
						if ( ((fp.x - lp.x) > dist) || ((fp.x - lp.x) < -dist) || ((fp.y - lp.y) < -dist) || ((fp.y - lp.y) > dist) ) { 
							scriptParcelle.IncrementDigged();
							scriptParcelle.AEteCreuser();
						}

						if (scriptParcelle.IsFullyDigged()) {
							AfficherUILegumes();
						}
					}
					else if (scriptParcelle._curState == Parcelle.ParcelleState.graine) {
						// on plante la graine, faire un drag and drop, changement d'etat traité dans Ajouterlegume()
					}
					else if (scriptParcelle._curState == Parcelle.ParcelleState.arrosage) {
						// accélérometre pour arroser

						scriptParcelle.AEteArroser();
					}
					else if (scriptParcelle._curState == Parcelle.ParcelleState.mature) {
						// la plante est mature
						
						scriptParcelle.EstMature();
					}
				}
				// si la parcelle n'etait pas deja selectionne
				else {
					// si on reselectionne une parcelle qui attend une graine
					if (scriptParcelle._curState == Parcelle.ParcelleState.graine) {
						AfficherUILegumes();
					}

					scriptParcelle.AEteSelectionne();
				}

				//print (hit.collider.tag + "   " + scriptParcelle._curState + "   " + scriptParcelle._legume);
			}
		}
		#endregion
	}



	// SelectionnerLegume est utilisé dans GameManagerJardin
	public void SelectionnerLegume() {
		if (Input.touchCount == 1) {
			if (Input.GetTouch(0).phase == TouchPhase.Ended) {

				if ((carotte.HitTest(Input.GetTouch(0).position))) {
					//print ("carotte");
					AjouterLegume(carotte);
				}
				else if (tomate.HitTest(Input.GetTouch(0).position)) {
					//print ("tomate");
					AjouterLegume(tomate);
				}
				else if (choux.HitTest(Input.GetTouch(0).position)) {
					//print ("choux");
					AjouterLegume(choux);
				}
				else if (aubergine.HitTest(Input.GetTouch(0).position)) {
					//print ("aubergine");
					AjouterLegume(aubergine);
				}
				else if (patate.HitTest(Input.GetTouch(0).position)) {
					//print ("patate");
					AjouterLegume(patate);
				}
				else if (oignon.HitTest(Input.GetTouch(0).position)) {
					//print ("oignon");
					AjouterLegume(oignon);
				}
				
				
			}
		}
	}


	void AjouterLegume(GUITexture gTex) {
		QueteJardin scriptQueteJardin = GetComponent<QueteJardin>();

		if(scriptQueteJardin.AjouterLegume(gTex.gameObject)) {
			NePasAfficherUILegumes();
			selectedParcelle.GetComponent<Parcelle>().AEtePlante();
			selectedParcelle.GetComponent<Parcelle>()._legume = gTex;
		}
	}

	void ValiderLegumes() {
		if (Input.touchCount == 1) {
			if (validation.HitTest(Input.GetTouch(0).position)) {

				QueteJardin scriptQueteJardin = GetComponent<QueteJardin>();

				//verifier ici si le legume obligatoire a ete plante
				// plantation 1
				if (GameManagerJardin.curGameState == GameManagerJardin.GameState.planterP1) {
					// si on a plante le legume obligatoire, on passe a la transition
					if (scriptQueteJardin.VerifierLegume()) {
						ChangeState(GameManagerJardin.GameState.planterP1, GameManagerJardin.GameState.dialogueTransition1);
						selectedParcelle = null;
						NePasAfficherUILegumes();
						NePasAfficherBoutons();
					}
					// sinon on avertit le joueur
					else
						GameManagerJardin._alertState = GameManagerJardin.AlerteState.planterLegumeObligatoire;
				}
				
				//verifier ici si la liste de legumes plante a la p1 a ete replante correctement
				// plantation 2
				else if (GameManagerJardin.curGameState == GameManagerJardin.GameState.planterP2) {
					ChangeState(GameManagerJardin.GameState.planterP2, GameManagerJardin.GameState.score);
					selectedParcelle = null;
					NePasAfficherUILegumes();
					NePasAfficherBoutons();
				}
				

			}
		}
	}

	void AfficherLegumesPlantes() {
		if (Input.touchCount == 1) {
			if (info.HitTest(Input.GetTouch(0).position)) {
				GameManagerJardin._alertState = GameManagerJardin.AlerteState.afficherLegumesPlantes;
			}
		}

	}
	// Parameters: prev State, curr State
	void ChangeState(GameManagerJardin.GameState prev, GameManagerJardin.GameState current) { 
		GameManagerJardin.curGameState = current;
		GameManagerJardin.prevGameState = prev;
	}


	#region methodes pour l'UI
	// active et affiche l'UI des legumes
	public void AfficherUILegumes() {
		GameManager.AfficherTexture(carotte);
		GameManager.AfficherTexture(tomate);
		GameManager.AfficherTexture(choux);
		GameManager.AfficherTexture(aubergine);
		GameManager.AfficherTexture(patate);
		GameManager.AfficherTexture(oignon);
	}
	
	// désactive et enleve l'UI des legumes
	public void NePasAfficherUILegumes() {
		GameManager.NePasAfficherTexture(carotte);
		GameManager.NePasAfficherTexture(tomate);
		GameManager.NePasAfficherTexture(choux);
		GameManager.NePasAfficherTexture(aubergine);
		GameManager.NePasAfficherTexture(patate);
		GameManager.NePasAfficherTexture(oignon);
	}

	// active et affiche les boutons de validation et d'info
	void AfficherBoutons() {
		GameManager.AfficherTexture(validation);
		GameObject.Find("validerLegumesText").guiText.enabled = true;

		GameManager.AfficherTexture(info);
	}
	
	// désactive et enleve le bouton de validation
	void NePasAfficherBoutons() {
		GameManager.NePasAfficherTexture(validation);
		GameObject.Find("validerLegumesText").guiText.enabled = false;

		GameManager.NePasAfficherTexture(info);
	}
	#endregion

}
