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
		finJardin,
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


		#region quete 1
		// quete 1
		if (curGameState == GameState.queteJessicaP1) {

			if (Input.GetButtonUp("Jump"))
				ChangeState(GameState.queteJessicaP1, GameState.planterP1);
		}
		#endregion
		
		
		#region planterP1
		// phase de plantage numéro 1
		else if (curGameState == GameState.planterP1) {

			touchJardin.SelectionnerLegume();
			touchJardin.ValiderLegumes();

			if (Input.GetButtonUp("Jump"))
				ChangeState(GameState.planterP1, GameState.dialogueTransition1);
		}
		#endregion
		
		
		#region dialogue 1 de la transition
		// dialogue 1 de la tansition 
		else if (curGameState == GameState.dialogueTransition1) {

			if (Input.GetButtonUp("Jump"))
				ChangeState(GameState.dialogueTransition1, GameState.transition);
		}
		#endregion
		
		
		#region transition
		// phase de transition
		else if (curGameState == GameState.transition) {

			if (Input.GetButtonUp("Jump"))
				ChangeState(GameState.transition, GameState.dialogueTransition2);
		}
		#endregion


		#region dialogue 2 de la transition
		// dialogue 2 de la tansition 2
		else if (curGameState == GameState.dialogueTransition2) {
			
			if (Input.GetButtonUp("Jump"))
				ChangeState(GameState.dialogueTransition2, GameState.queteJessicaP2);
		}
		#endregion


		#region quete2
		// quete 2
		else if (curGameState == GameState.queteJessicaP2) {
			
			if (Input.GetButtonUp("Jump"))
				ChangeState(GameState.queteJessicaP2, GameState.planterP2);
		}
		#endregion


		#region planter2
		// phase de plantage numéro 2
		else if (curGameState == GameState.planterP2) {

			touchJardin.SelectionnerLegume();
			touchJardin.ValiderLegumes();

			if (Input.GetButtonUp("Jump"))
				ChangeState(GameState.planterP2, GameState.finJardin);
		}
		#endregion


		#region finJardin
		// on a fini l'activité
		else if (curGameState == GameState.finJardin) {
			
			if (Input.GetButtonUp("Jump"))
				ChangeState(GameState.finJardin, GameState.score);
		}
		#endregion


		#region score
		// score
		else if (curGameState == GameState.score) {

			if (Input.GetButtonUp("Jump"))
				ChangeState(GameState.score, GameState.queteJessicaP1);
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

		
		#region quete 1
		// Affichage de la quete 1
		if (curGameState == GameState.queteJessicaP1) {
			AfficherDialogue(jessica, "queteJessicaP1");
		}
		#endregion
		
		
		#region planter 1
		// affiche l'aide pour la phase de plantage numéro 1
		else if (curGameState == GameState.planterP1) {
			if (touchJardin.selectedParcelle == null) {
				AfficherAide("Selectionne une parcelle.");
			}
			else {
				if (touchJardin.selectedParcelle.GetComponent<Parcelle>()._curState == Parcelle.ParcelleState.creuser) {
					AfficherAide("Creuse.");
				}
				else if (touchJardin.selectedParcelle.GetComponent<Parcelle>()._curState == Parcelle.ParcelleState.graine) {
					AfficherAide("Plante une graine.");
				}
				else if (touchJardin.selectedParcelle.GetComponent<Parcelle>()._curState == Parcelle.ParcelleState.arrosage) {
					AfficherAide("Arrose");
				}
			}
		}
		#endregion


		#region dialogue 1 de la transition
		// affichage du dialogue 1 de la tansition
		else if (curGameState == GameState.dialogueTransition1) {
			AfficherDialogue(jessica, "dialogueTransition1");
		}
		#endregion


		#region transition
		// affichage de l'aide pour la phase de transition
		else if (curGameState == GameState.transition) {
			//AfficherDialogue(jessica, "transition");
		}
		#endregion


		#region dialogue 2 de la transition
		// affichage du dialogue 2 de la tansition
		else if (curGameState == GameState.dialogueTransition2) {
			AfficherDialogue(jessica, "dialogueTransition2");
		}
		#endregion


		#region quete 2
		// Affichage de la quete 2
		if (curGameState == GameState.queteJessicaP2) {
			AfficherDialogue(jessica, "queteJessicaP2");
		}
		#endregion


		#region planter 2
		// affiche l'aide pour la phase de plantage numéro 2
		else if (curGameState == GameState.planterP2) {
			//AfficherDialogue(jessica, "planterP2");
			
		}
		#endregion


		#region dialogue de fin de l'activité
		// affichage du dialogue de fin de l'activité
		else if (curGameState == GameState.finJardin) {
			AfficherDialogue(jessica, "finJardin");
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