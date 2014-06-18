﻿using UnityEngine;
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
		parcelleMaxAtteint,
		arroserLegumes
	}

	private float _alertTime = 0.0f;
	public static AlerteState _alertState;

	public Texture2D jessica;

	private TouchJardin touchJardin;
	private QueteJardin queteJardin;

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

		#region plantation
		// phase de plantation numéro 1 ou 2
		if (curGameState == GameState.planterP1 || curGameState == GameState.planterP2) {

			// si une parcelle est selectionné
			if (touchJardin.selectedParcelle) {
				if (touchJardin.selectedParcelle.GetComponent<Parcelle>()._curState == Parcelle.ParcelleState.arrosage) {
					touchJardin.ArroserLegume();
				}
			}
		}
		#endregion
		
		
		#region transition
		// phase de transition
		else if (curGameState == GameState.transition) {

		}
		#endregion


		#region score
		// score
		else if (curGameState == GameState.score) {

		}
		#endregion 
	}
	#endregion


	////////////////////////////////////////////////////////////////////////////////////////


	#region OnGUI
	void OnGUI() {


		//AfficherAlerte("ceci est une <a>alerte\n");
		//AfficherAlerteAvecSurbrillance("ceci est une <a>alerte \net ca <a>aussi \n encore <a>une");
		//test ("ceci est une <a>alerte\net ca <a>aussi \n encore <a>une a\n b\n c\n d\n");

		if (!jessica) {
			Debug.LogError("Ajouter les textures!");
			return;
		}

		
		#region quetes
		// Affichage de la quete 1
		if (curGameState == GameState.queteJessicaP1) {
			AfficherDialogue(jessica, queteJardin.DialogueQueteP1());
		}

		// Affichage de la quete 2
		else if (curGameState == GameState.queteJessicaP2) {
			AfficherDialogue(jessica, queteJardin.DialogueQueteP2());
		}
		#endregion
		
		
		#region plantation
		// affiche l'aide pour la phase de plantation numéro 1 ou 2
		else if (curGameState == GameState.planterP1 || curGameState == GameState.planterP2) {
			if (touchJardin.selectedParcelle == null) {
				AfficherAide("Selectionne une parcelle en la touchant avec le doigt.");
			}
			else {

				if (touchJardin.selectedParcelle.GetComponent<Parcelle>()._curState == Parcelle.ParcelleState.creuser) {
					AfficherAide("Creuse la parcelle trois fois avec avec de petits mouvements du doigt.");
				}
				else if (touchJardin.selectedParcelle.GetComponent<Parcelle>()._curState == Parcelle.ParcelleState.graine) {
					AfficherAide("Plante une graine en la maintenant et en la d\xe9posant sur la parcelle.");
				}
				else if (touchJardin.selectedParcelle.GetComponent<Parcelle>()._curState == Parcelle.ParcelleState.arrosage) {
					AfficherAide("Arrose la parcelle en inclinant la tablette.");
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
			AfficherAlerte(queteJardin.ScoreText());
		}
		#endregion


		#region alert handling
		// quand on doit plante le legume obligatoire
		if (_alertState == AlerteState.planterLegumeObligatoire) {
			Alerte("N'oublie pas de planter le l\xe9gume obligatoire: " + queteJardin.getLegumeName(queteJardin.legumeObligatoire) + ".", 5.0f);
		}
		// quand on appui sur le bouton d'info
		else if (_alertState == AlerteState.afficherLegumesPlantes) {
			Alerte(queteJardin.getLegumesPlantes(), 5.0f);
		}
		// quand on peut plus planté
		else if (_alertState == AlerteState.parcelleMaxAtteint) {
			Alerte("Le nombre de l\xe9gumes autoris\xe9 a planter a \xe9t\xe9 atteint.", 5.0f);
		}
		// si un legumes n'a pas ete arrose
		else if (_alertState == AlerteState.arroserLegumes) {
			Alerte("Tu as oubli\xe9 d'arroser un l\xe9gume.", 5.0f);
		}
		#endregion
	}
	#endregion

	////////////////////////////////////////////////////////////////////////////////////////

	#region autres méthodes
	// permet de configurer une alerte
	public void SetAlerte(AlerteState state) {
		_alertState = state;
		_alertTime = 0.0f;
	}

	// Affiche l'alerte txt pendant tps secondes puis reset l'etat alerte a 'en attente'
	void Alerte(string txt, float tps) {
		_alertTime += Time.deltaTime;
		AfficherAlerte(txt);

		if (_alertTime > tps) {
			_alertState = AlerteState.attente;
			_alertTime = 0.0f;
		}
	}


	// Parameters: prev State, curr State
	void ChangeState(GameState prev, GameState current) {
		curGameState = current;
		prevGameState = prev;
	}
	#endregion

}