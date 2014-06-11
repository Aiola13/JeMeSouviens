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
			AfficherValidation();
		}

		// score
		else if (GameManagerJardin.curGameState == GameManagerJardin.GameState.score) {
			// FAIRE UN RETOUR MENU ICI
			ChangeState(GameManagerJardin.GameState.score, GameManagerJardin.GameState.queteJessicaP1);
		}
		#endregion


		#region plantation
		// si on touche une parcelle
		else if ((GameManagerJardin.curGameState == GameManagerJardin.GameState.planterP1) || 
		         (GameManagerJardin.curGameState == GameManagerJardin.GameState.planterP2)){

			// on vérifie si on a appuyé sur le bouton de validation
			ValiderLegumes();


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

				/*
				// si on reselectionne une parcelle qui attend une graine
				if (!scriptParcelle.isSelected && scriptParcelle._curState == Parcelle.ParcelleState.graine) {
					AfficherUILegumes();
				}
*/
				// la parcelle change d'etat seulement si une interaction se fait dessus quand elle est selectionné
				if (scriptParcelle.isSelected) {
					if (scriptParcelle._curState == Parcelle.ParcelleState.creuser) {
						// faire ici un swipe pour creuser

						AfficherUILegumes();
						scriptParcelle.AEteCreuser();
					}
					else if (scriptParcelle._curState == Parcelle.ParcelleState.graine) {
						// on plante la graine

						NePasAfficherUILegumes();
						scriptParcelle.AEtePlante();
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

				print (hit.collider.tag + "   " + scriptParcelle._curState + "   " + scriptParcelle._legume);
			}
		}
		#endregion
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


	void ValiderLegumes() {
		if (Input.touchCount == 1) {
			if (validation.HitTest(Input.GetTouch(0).position)) {
				
				//verifier ici si le legume obligatoire a ete plante
				// plantation 1
				if (GameManagerJardin.curGameState == GameManagerJardin.GameState.planterP1) {
					ChangeState(GameManagerJardin.GameState.planterP1, GameManagerJardin.GameState.dialogueTransition1);
				}
				
				//verifier ici si la liste de legumes plante a la p1 a ete replante correctement
				// plantation 2
				else if (GameManagerJardin.curGameState == GameManagerJardin.GameState.planterP2) {
					ChangeState(GameManagerJardin.GameState.planterP2, GameManagerJardin.GameState.score);
				}
				
				selectedParcelle = null;
				NePasAfficherUILegumes();
				NePasAfficherValidation();
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

	// active et affiche le bouton de validation
	void AfficherValidation() {
		GameManager.AfficherTexture(validation);
		GameObject.Find("validerLegumesText").guiText.enabled = true;
	}
	
	// désactive et enleve le bouton de validation
	void NePasAfficherValidation() {
		GameManager.NePasAfficherTexture(validation);
		GameObject.Find("validerLegumesText").guiText.enabled = false;
	}
	#endregion

}
