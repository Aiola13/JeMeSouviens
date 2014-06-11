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

	public Texture2D jessica;

	private TouchJardin touchJardin;


	#endregion attributs


	void Start() {
		curGameState = GameState.queteJessicaP1;

		touchJardin = GetComponent<TouchJardin>();
	}


	#region Update
	void Update() {

		print("cur : " + curGameState + "    prev :  " + prevGameState);


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

			if (touchJardin.selectedParcelle) {
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
			AfficherDialogue(jessica, "queteJessicaP1");
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
					//touchJardin.NePasAfficherUILegumes();
				}
				else if (touchJardin.selectedParcelle.GetComponent<Parcelle>()._curState == Parcelle.ParcelleState.graine) {
					AfficherAide("Plante une graine.");
					//touchJardin.AfficherUILegumes();
				}
				else if (touchJardin.selectedParcelle.GetComponent<Parcelle>()._curState == Parcelle.ParcelleState.arrosage) {
					AfficherAide("Arrose la parcelle.");
					//touchJardin.NePasAfficherUILegumes();
				}
				else if (touchJardin.selectedParcelle.GetComponent<Parcelle>()._curState == Parcelle.ParcelleState.mature) {
					AfficherAide("Selectionne une autre parcelle ou valide.");
					//touchJardin.NePasAfficherUILegumes();
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
	}
	#endregion
	
	
	
	// Parameters: prev State, curr State
	void ChangeState(GameState prev, GameState current) {
		curGameState = current;
		prevGameState = prev;
	}

}