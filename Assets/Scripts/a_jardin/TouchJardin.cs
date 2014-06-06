using UnityEngine;
using System.Collections;

public class TouchJardin : TouchLogic {

	public GUITexture carotte;
	public GUITexture tomate;
	public GUITexture choux;
	public GUITexture aubergine;
	public GUITexture patate;
	public GUITexture oignon;

	public GUITexture validation;

	private int parcelles = 8;					// number of the parcelle layer
	public GameObject selectedParcelle;
	
	private Ray ray;
	private RaycastHit hit;
	
	void Start() {
		NePasAfficherUILegumes();
		NePasAfficherValidation();
	}

	public void ValiderLegumes() {
		if (Input.touchCount == 1) {
			if ((validation.HitTest(Input.GetTouch(0).position) && (Input.GetTouch(0).phase == TouchPhase.Ended))) {

				//verifier ici si le legume obligatoire a ete plante
				// plantage 1
				if (GameManagerJardin.curGameState == GameManagerJardin.GameState.planterP1) {
					ChangeState(GameManagerJardin.GameState.planterP1, GameManagerJardin.GameState.dialogueTransition1);

				}

				//verifier ici si la liste de legumes plante a la p1 a ete replante correctement
				// plantage 2
				if (GameManagerJardin.curGameState == GameManagerJardin.GameState.planterP2) {
					ChangeState(GameManagerJardin.GameState.planterP2, GameManagerJardin.GameState.finJardin);
					
				}

				NePasAfficherValidation();
			}
		}
	}

	public void SelectionnerLegume() {
		if (Input.touchCount == 1) {
			if (Input.GetTouch(0).phase == TouchPhase.Ended) {
				if ((carotte.HitTest(Input.GetTouch(0).position))) {
					print ("carotte");
				}
				else if (tomate.HitTest(Input.GetTouch(0).position)) {
					print ("tomate");
				}
				else if (choux.HitTest(Input.GetTouch(0).position)) {
					print ("choux");
				}
				else if (aubergine.HitTest(Input.GetTouch(0).position)) {
					print ("aubergine");
				}
				else if (patate.HitTest(Input.GetTouch(0).position)) {
					print ("patate");
				}
				else if (oignon.HitTest(Input.GetTouch(0).position)) {
					print ("oignon");
				}
			}
		}
	}
	
	public override void OnTouchEndedAnywhere () {

		#region DIALOGUES
		// quete 1
		if (GameManagerJardin.curGameState == GameManagerJardin.GameState.queteJessicaP1) {
			ChangeState(GameManagerJardin.GameState.queteJessicaP1, GameManagerJardin.GameState.planterP1);
			AfficherValidation();
		}

		// dialogue 1 de la tansition 
		else if (GameManagerJardin.curGameState == GameManagerJardin.GameState.dialogueTransition1) {
			ChangeState(GameManagerJardin.GameState.dialogueTransition1, GameManagerJardin.GameState.transition);
		}

		// dialogue 2 de la tansition 2
		else if (GameManagerJardin.curGameState == GameManagerJardin.GameState.dialogueTransition2) {
			ChangeState(GameManagerJardin.GameState.dialogueTransition2, GameManagerJardin.GameState.queteJessicaP2);
		}

		// quete 2
		else if (GameManagerJardin.curGameState == GameManagerJardin.GameState.queteJessicaP2) {
			ChangeState(GameManagerJardin.GameState.queteJessicaP2, GameManagerJardin.GameState.planterP2);
			AfficherValidation();
		}

		// on a fini l'activité
		else if (GameManagerJardin.curGameState == GameManagerJardin.GameState.finJardin) {
			ChangeState(GameManagerJardin.GameState.finJardin, GameManagerJardin.GameState.score);
		}

		// score
		else if (GameManagerJardin.curGameState == GameManagerJardin.GameState.score) {
			// FAIRE UN RETOUR MENU ICI
			ChangeState(GameManagerJardin.GameState.score, GameManagerJardin.GameState.queteJessicaP1);
		}
		#endregion

		#region PLANTAGE
		// si on touche une parcelle
		else if ((GameManagerJardin.curGameState == GameManagerJardin.GameState.planterP1) || 
		         (GameManagerJardin.curGameState == GameManagerJardin.GameState.planterP2)){

			ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
			if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.layer == parcelles)) {
				/*
				if () {

				}
*/
				GameObject parcelle = hit.collider.gameObject;
				Parcelle scriptParcelle = parcelle.GetComponent<Parcelle>();

				selectedParcelle = parcelle;

				scriptParcelle.isSelected = true;

				if (scriptParcelle._curState == Parcelle.ParcelleState.attente) {
					scriptParcelle.ChangeState(Parcelle.ParcelleState.attente, Parcelle.ParcelleState.selectionne);
				}
				else if (scriptParcelle._curState == Parcelle.ParcelleState.selectionne) {
					scriptParcelle.ChangeState(Parcelle.ParcelleState.selectionne, Parcelle.ParcelleState.creuser);
				}
				else if (scriptParcelle._curState == Parcelle.ParcelleState.creuser) {
					AfficherUILegumes();
					scriptParcelle.ChangeState(Parcelle.ParcelleState.creuser, Parcelle.ParcelleState.graine);
				}
				else if (scriptParcelle._curState == Parcelle.ParcelleState.graine) {
					NePasAfficherUILegumes();
					scriptParcelle.ChangeState(Parcelle.ParcelleState.graine, Parcelle.ParcelleState.arrosage);
				}
				else if (scriptParcelle._curState == Parcelle.ParcelleState.arrosage) {
					scriptParcelle.ChangeState(Parcelle.ParcelleState.arrosage, Parcelle.ParcelleState.mature);
				}

				print (hit.collider.tag + "   " + scriptParcelle._curState + "   " + scriptParcelle._legume);
			}
		}
		#endregion
	}
	
	
	// Parameters: prev State, curr State
	void ChangeState(GameManagerJardin.GameState prev, GameManagerJardin.GameState current) { 
		GameManagerJardin.curGameState = current;
		GameManagerJardin.prevGameState = prev;
	}


	#region methodes pour l'UI
	// active et affiche l'UI des legumes
	void AfficherUILegumes() {
		GameManager.AfficherTexture(carotte);
		GameManager.AfficherTexture(tomate);
		GameManager.AfficherTexture(choux);
		GameManager.AfficherTexture(aubergine);
		GameManager.AfficherTexture(patate);
		GameManager.AfficherTexture(oignon);
	}
	
	// désactive et enleve l'UI des legumes
	void NePasAfficherUILegumes() {
		GameManager.NePasAfficherTexture(carotte);
		GameManager.NePasAfficherTexture(tomate);
		GameManager.NePasAfficherTexture(choux);
		GameManager.NePasAfficherTexture(aubergine);
		GameManager.NePasAfficherTexture(patate);
		GameManager.NePasAfficherTexture(oignon);
	}

	// active et affiche le bouton de validation
	void AfficherValidation() {
		GameManager.AfficherTexture(validation);
		//GameObject.Find("validerLegumesText").SetActive(true);
	}
	
	// désactive et enleve le bouton de validation
	void NePasAfficherValidation() {
		GameManager.NePasAfficherTexture(validation);
		//GameObject.Find("validerLegumesText").SetActive(false);
	}
	#endregion

}
