using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerJardin :  GameManager{
	
	#region attributs
	public enum GameState {
		queteJessicaP1,
		planterP1,
		dialogueTransition1,
		transition,
		dialogueTransition2,
		queteJessicaP2,
		planterP2,
		score
	}

	public static GameState curGameState;
	public static GameState prevGameState;

	public enum AlerteState {
		attente,
		planterLegumeObligatoire,
		afficherLegumesPlantes,
		parcelleMaxAtteint
	}

	public static AlerteState _alertState;

	public Texture2D jessica;

	public TouchJardin touchJardin;
	public QueteJardin queteJardin;

	#endregion attributs


	void Start() {
		curGameState = GameState.queteJessicaP1;
		_alertState = AlerteState.attente;

		touchJardin = GetComponent<TouchJardin>();
		queteJardin = GetComponent<QueteJardin>();
	}


	#region Update
	void Update() {

		//print("cur : " + curGameState + "    prev :  " + prevGameState);


		#region quetes
		// quete 1
		if (curGameState == GameState.queteJessicaP1) {

		}

		// quete 2
		else if (curGameState == GameState.queteJessicaP2) {
			
		}
		#endregion
		
		
		#region plantation
		// phase de plantation numéro 1 ou 2
		else if (curGameState == GameState.planterP1 || curGameState == GameState.planterP2) {

			// si une parcelle est selectionné
			if (touchJardin.selectedParcelle) {
				// si la parcelle selectionné est dans l'etat de plantation d'une graine
				if (touchJardin.selectedParcelle.GetComponent<Parcelle>()._curState == Parcelle.ParcelleState.graine) {
					touchJardin.SelectionnerLegume();
				}
			}
		}
		#endregion
		
		
		#region transition
		// dialogue 1 de la tansition 
		else if (curGameState == GameState.dialogueTransition1) {

		}

		// phase de transition
		else if (curGameState == GameState.transition) {

		}

		// dialogue 2 de la tansition 2
		else if (curGameState == GameState.dialogueTransition2) {

		}
		#endregion

		#region score
		// score
		else if (curGameState == GameState.score) {

		}
		#endregion 
	}
	#endregion




	#region OnGUI
	void OnGUI() {

		if (!jessica) {
			Debug.LogError("Ajouter les textures!");
			return;
		}

		
		#region quetes
		// Affichage de la quete 1
		if (curGameState == GameState.queteJessicaP1) {
			AfficherDialogue(jessica, queteJardin.QueteP1());
		}

		// Affichage de la quete 2
		else if (curGameState == GameState.queteJessicaP2) {
			AfficherDialogue(jessica, "queteJessicaP2");
		}
		#endregion
		
		
		#region plantation
		// affiche l'aide pour la phase de plantation numéro 1 ou 2
		else if (curGameState == GameState.planterP1 || curGameState == GameState.planterP2) {
			if (touchJardin.selectedParcelle == null) {
				AfficherAide("Selectionne une parcelle.");
			}
			else {

				if (touchJardin.selectedParcelle.GetComponent<Parcelle>()._curState == Parcelle.ParcelleState.creuser) {
					AfficherAide("Creuse la parcelle.");
				}
				else if (touchJardin.selectedParcelle.GetComponent<Parcelle>()._curState == Parcelle.ParcelleState.graine) {
					AfficherAide("Plante une graine.");
				}
				else if (touchJardin.selectedParcelle.GetComponent<Parcelle>()._curState == Parcelle.ParcelleState.arrosage) {
					AfficherAide("Arrose la parcelle.");
				}
				else if (touchJardin.selectedParcelle.GetComponent<Parcelle>()._curState == Parcelle.ParcelleState.mature) {
					AfficherAide("Selectionne une autre parcelle ou valide.");
				}
			}
		}
		#endregion


		#region transition
		// affichage du dialogue 1 de la tansition
		else if (curGameState == GameState.dialogueTransition1) {
			AfficherDialogue(jessica, "dialogueTransition1");
		}

		// affichage de l'aide pour la phase de transition
		else if (curGameState == GameState.transition) {
			AfficherDialogue(jessica, "transition");
		}

		// affichage du dialogue 2 de la tansition
		else if (curGameState == GameState.dialogueTransition2) {
			AfficherDialogue(jessica, "dialogueTransition2");
		}
		#endregion


		#region score
		// affichage du tableau de score
		else if (curGameState == GameState.score) {
			AfficherDialogue(jessica, "score");
		}
		#endregion


		#region alert handling
		if (_alertState == AlerteState.planterLegumeObligatoire) {
			StartCoroutine(AlertReset("N'oublie pas de planter le legume obligatoire: " + queteJardin.getLegumeName(queteJardin.legumeObligatoire) + ".", 3.0f));
		}
		else if (_alertState == AlerteState.afficherLegumesPlantes) {
			StartCoroutine(AlertReset(queteJardin.getLegumesPlantes(), 3.0f));
		}
		else if (_alertState == AlerteState.parcelleMaxAtteint) {
			StartCoroutine(AlertReset("Le nombre de legumes autorise a planter a été atteint.", 3.0f));
		}
		#endregion
	}
	#endregion

	// Affiche une alerte pendant tps secondes puis reset l'etat alerte
	IEnumerator AlertReset(string txt, float tps) {
		AfficherAlerte(txt);
		yield return new WaitForSeconds(tps);
		_alertState = AlerteState.attente;
	}

	// Parameters: prev State, curr State
	void ChangeState(GameState prev, GameState current) {
		curGameState = current;
		prevGameState = prev;
	}

}