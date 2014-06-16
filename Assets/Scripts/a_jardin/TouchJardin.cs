using UnityEngine;
using System.Collections;

public class TouchJardin : TouchLogic {

	#region attributs
	private GameManagerJardin gmJardin;

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
	public GUITexture menu;

	// Parcelles
	private int parcelles = 8;			// number of the parcelle layer
	public GameObject selectedParcelle;

	// swipe creuser
	private float dist = 10;			// distance to register a swipe in any given direction
	private Vector2 fp ; 	 			// first finger position
	private Vector2 lp;  				// last finger position

	// dragNdrop legumes
	public bool dragging = false;		// est vrai si on drag un legume a partir de l'ui
	public GUITexture legumeDragged;
	public Transform grainePrefab;
	private Transform graineClone;

	// accelerometre arrosage
	private GameObject arrosoir;
	private float zRot = 0.0f;
	private float maxAngle = 130.0f;	// angle maximum pour l'arrosoir
	private float accMax = 0.5f;		// pourcentage d'orientation de la tablette

	private Ray ray;
	private RaycastHit hit;
	#endregion

	void Start() {
		gmJardin = GetComponent<GameManagerJardin>();
		arrosoir = GameObject.FindGameObjectWithTag("Arrosoir");

		NePasAfficherUILegumes();
		NePasAfficherBoutons();
	}


	public override void OnTouchBeganAnywhere () {
		
		#region plantation on touch began
		// si on touche une parcelle
		if ((GameManagerJardin.curGameState == GameManagerJardin.GameState.planterP1) || 
		    (GameManagerJardin.curGameState == GameManagerJardin.GameState.planterP2)){

			ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

			#region swipe begin
			if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.layer == parcelles)) {

				GameObject parcelle = hit.collider.gameObject;
				Parcelle scriptParcelle = parcelle.GetComponent<Parcelle>();

				if (scriptParcelle._curState == Parcelle.ParcelleState.creuser) {
					// si on commence le swipe sur la parcelle selectionne
					if (Input.touchCount == 1 && selectedParcelle && selectedParcelle.tag == parcelle.tag) {
						fp = Input.GetTouch(0).position;
						lp = Input.GetTouch(0).position;
					}
				}
			}
			#endregion

			#region drag begin
			if (selectedParcelle){
				if (selectedParcelle.GetComponent<Parcelle>()._curState == Parcelle.ParcelleState.graine) {
					if (Input.touchCount == 1) {
						if ((carotte.HitTest(Input.GetTouch(0).position))) {
							dragging = true;
							legumeDragged = carotte;
							Vector3 pos = new Vector3(hit.point.x, 1, hit.point.z);
							graineClone = (Transform)Instantiate(grainePrefab, pos, Quaternion.identity) as Transform;
						}
						else if (tomate.HitTest(Input.GetTouch(0).position)) {
							dragging = true;
							legumeDragged = tomate;
						}
						else if (choux.HitTest(Input.GetTouch(0).position)) {
							dragging = true;
							legumeDragged = choux;
						}
						else if (aubergine.HitTest(Input.GetTouch(0).position)) {
							dragging = true;
							legumeDragged = aubergine;
						}
						else if (patate.HitTest(Input.GetTouch(0).position)) {
							dragging = true;
							legumeDragged = patate;
						}
						else if (oignon.HitTest(Input.GetTouch(0).position)) {
							dragging = true;
							legumeDragged = oignon;
						}
					}
				}
			}
			#endregion
		}
		#endregion
	}


	public override void OnTouchMovedAnywhere () {

		#region plantation on touch moved
		if ((GameManagerJardin.curGameState == GameManagerJardin.GameState.planterP1) || 
		    (GameManagerJardin.curGameState == GameManagerJardin.GameState.planterP2)){

			ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

			// si on est en train de dragger, on met a jour la position de la graine
			if (dragging) {
				graineClone.position = new Vector3(hit.point.x, 1, hit.point.z);
			}

			if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.layer == parcelles)) {

				GameObject parcelle = hit.collider.gameObject;
				Parcelle scriptParcelle = parcelle.GetComponent<Parcelle>();

				// si on a une parcelle selectionné et que la parcelle touché est la parcelle sélectionné
				if (selectedParcelle && selectedParcelle.tag == parcelle.tag) {

					#region swipe moved
					if (scriptParcelle._curState == Parcelle.ParcelleState.creuser) {
						if (Input.touchCount == 1) {
							lp = Input.GetTouch(0).position;
						}
					}
					#endregion

					#region drag moved
					// si on est en train de dragger et que la parcelle touché est dans la phase de plantation
					if (dragging && scriptParcelle._curState == Parcelle.ParcelleState.graine) {
						if (Input.touchCount == 1) {

							print("moving");
						}
					}
					#endregion
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

			// on vérifie si on a appuyé sur le bouton de menu
			RetournerMenu();

			// on vérifie si on a appuyé sur le bouton de validation
			ValiderLegumes();

			// on vérifie si on a appuyé sur le bouton d'info
			AfficherLegumesPlantes();

			ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

			// si on a termine un touch en dehors d'une parcelle, sur le jardin
			if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.tag == "FloorJardin")) {
				// si on etait en train de dragger un legume
				if (dragging) {
					AnnulerGraine();
				}
				// on deselectionne la parcelle couremment selectionne
				else if (selectedParcelle) {
					selectedParcelle.GetComponent<Parcelle>().AEteDeSelectionne();
					selectedParcelle = null;
					NePasAfficherUILegumes();
				}
			}

			// si on a termine un touch en sur une parcelle
			else if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.layer == parcelles)) {

				GameObject parcelle = hit.collider.gameObject;
				Parcelle scriptParcelle = parcelle.GetComponent<Parcelle>();

				// si on termine un touch sur une parcelle selectionne
				if (scriptParcelle.isSelected) {

					// faire 3 petits swipe pour creuser
					if (scriptParcelle._curState == Parcelle.ParcelleState.creuser) {
						// swipe action
						#region swipe ended
						if ( ((fp.x - lp.x) > dist) || ((fp.x - lp.x) < -dist) || ((fp.y - lp.y) < -dist) || ((fp.y - lp.y) > dist) ) { 
							scriptParcelle.IncrementDigged();
							scriptParcelle.AEteCreuse();
						}
						#endregion

						if (scriptParcelle.IsFullyDigged()) {
							AfficherUILegumes();
						}
					}

					// on plante la graine, faire un drag and drop
					// changement d'etat traité dans AjoutLegume(), GameManagerJardin()->SelectionnerLegume()->AjoutLegume()
					else if (scriptParcelle._curState == Parcelle.ParcelleState.graine) {
						#region drag ended
						if (dragging && Input.touchCount == 1) {
							// si le legume a ete plante, on le place correctement
							if (AjoutLegume(legumeDragged)) {
								graineClone.position = new Vector3(parcelle.transform.position.x, 0.15f, parcelle.transform.position.z);
								dragging = false;
							}
							else {
								AnnulerGraine();
							}
						}
						#endregion
					}

					// accélérometre pour arroser
					// changement d'etat traité dans ArroserLegume(), GameManagerJardin()->ArroserLegume()
					else if (scriptParcelle._curState == Parcelle.ParcelleState.arrosage) {

					}

					// la plante est mature
					else if (scriptParcelle._curState == Parcelle.ParcelleState.mature) {

						scriptParcelle.EstMature();
					}
				}

				// si on termine un touch sur une parcelle non selectionne
				else {
					// si on etait en train de dragger, on annule le placement de la graine
					if (dragging && Input.touchCount == 1) {
						AnnulerGraine();
					}
					// sinon on a termine le touch sur une parcelle non selectionne
					else {
						if (selectedParcelle) {
							selectedParcelle.GetComponent<Parcelle>().AEteDeSelectionne();
							NePasAfficherUILegumes();
						}

						// la nouvelle parcelle selectionné est celle qu'on a touché
						selectedParcelle = parcelle;
						selectedParcelle.tag = parcelle.tag;
						scriptParcelle.AEteSelectionne();


						// si on reselectionne une parcelle qui attend une graine
						if (scriptParcelle._curState == Parcelle.ParcelleState.graine) {
							AfficherUILegumes();
						}
					}


				}

				//print (hit.collider.tag + "   " + scriptParcelle._curState + "   " + scriptParcelle._legume);
			}
		}
		#endregion
	}

	void AnnulerGraine() {
		dragging = false;
		Destroy (graineClone.gameObject);
	}

	// accélérometre
	public void ArroserLegume() {
		Parcelle scriptParcelle = selectedParcelle.GetComponent<Parcelle>();

		zRot = (-Input.acceleration.x / accMax) * maxAngle;

		if (zRot > maxAngle) {
			zRot = maxAngle;
			scriptParcelle.AEteArrose();
			return;
		}
		else if (zRot < 5)
			zRot = 0;

		arrosoir.transform.eulerAngles = new Vector3(0, 0, zRot);
	}



	// Parameters: prev State, curr State
	void ChangeState(GameManagerJardin.GameState prev, GameManagerJardin.GameState current) { 
		GameManagerJardin.curGameState = current;
		GameManagerJardin.prevGameState = prev;
	}



	#region methodes pour l'UI
	// renvoit vrai si on peut ajouter un legume, faux sinon
	bool AjoutLegume(GUITexture gTex) {
		QueteJardin scriptQueteJardin = GetComponent<QueteJardin>();
		
		// si le legume a ete plante
		if (scriptQueteJardin.VerifierAjoutLegume(gTex.gameObject)) {
			NePasAfficherUILegumes();
			selectedParcelle.GetComponent<Parcelle>().AEteSeme(gTex);
			return true;
		}
		else
			return false;
	}

	// verifie si on a valider la quete ds la phase 1 ou 2 quand on appui sur lebouton de validation
	void ValiderLegumes() {
		if (Input.touchCount == 1) {
			if (validation.HitTest(Input.GetTouch(0).position)) {
				
				QueteJardin scriptQueteJardin = GetComponent<QueteJardin>();

				// plantation 1
				if (GameManagerJardin.curGameState == GameManagerJardin.GameState.planterP1) {
					// si on a plante le legume obligatoire, on passe a la transition
					if (scriptQueteJardin.VerifierLegume()) {
						// tous les legumes ont ete arrosés
						if (scriptQueteJardin.VerifierLegumesArroses()) {
							scriptQueteJardin.EnregistrerLegumes();
							selectedParcelle = null;
							NePasAfficherUILegumes();
							NePasAfficherBoutons();
							ChangeState(GameManagerJardin.GameState.planterP1, GameManagerJardin.GameState.dialogueTransition1);
						}
						// un legume n'a pas été arrosé
						else 
							gmJardin.SetAlerte(GameManagerJardin.AlerteState.arroserLegumes);
					}
					// le légume obligatoire n'a pas été planté
					else
						gmJardin.SetAlerte(GameManagerJardin.AlerteState.planterLegumeObligatoire);
				}

				// plantation 2
				else if (GameManagerJardin.curGameState == GameManagerJardin.GameState.planterP2) {

					//verifier ici si la liste de legumes plante a la p1 a ete replante correctement

					ChangeState(GameManagerJardin.GameState.planterP2, GameManagerJardin.GameState.score);
					selectedParcelle = null;
					NePasAfficherUILegumes();
					NePasAfficherBoutons();
				}

			}
		}
	}

	// affiche une alerte quand on appui sur le bouton d'info
	void AfficherLegumesPlantes() {
		if (Input.touchCount == 1) {
			if (info.HitTest(Input.GetTouch(0).position)) {
				gmJardin.SetAlerte(GameManagerJardin.AlerteState.afficherLegumesPlantes);
			}
		}
		
	}

	// retourne au menu quand on appui sur le bouton de menu
	void RetournerMenu() {
		if (Input.touchCount == 1) {
			if (menu.HitTest(Input.GetTouch(0).position)) {
				Application.LoadLevel("menu");
			}
		}
	}

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
		GameObject.Find("validerText").guiText.enabled = true;

		GameManager.AfficherTexture(info);
	}
	
	// désactive et enleve le bouton de validation
	void NePasAfficherBoutons() {
		GameManager.NePasAfficherTexture(validation);
		GameObject.Find("validerText").guiText.enabled = false;

		GameManager.NePasAfficherTexture(info);
	}
	#endregion

}
