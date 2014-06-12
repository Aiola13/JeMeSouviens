using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QueteJardin : MonoBehaviour {
	
	private int nbLegumes = 6;
	private int nbParcelles = 3;

	public GameObject[] listeLegumes;
	public List<GameObject> legumesPlantes;

	public GameObject legumeObligatoire;


	void Start () {
		listeLegumes = new GameObject[nbLegumes];
	
		InitListeLegumes();
		legumeObligatoire = SelectionnerLegumeObligatoire();
	}
	

	void Update() {
		/*
		if (Input.GetButtonDown("Fire1")) {
			GameObject leg = SelectionnerLegumeObligatoire();
			AjouterLegume(leg);
		}
*/
		string txt = "obl: [" + legumeObligatoire.name + "]  ||  " + legumesPlantes.Count + " Sup: ";
		for (int i = 0; i < legumesPlantes.Count; i++) {
			txt = txt + legumesPlantes[i].name + "   ";
		}
		print(txt);
	}

	// initialise la liste de legumes
	private void InitListeLegumes() {
		int count = 0;
		foreach (Transform child in GameObject.Find("UI_Legumes").transform) {
			listeLegumes[count] = child.gameObject;
			count++;
		}
	}

	// retourne un legume aleatoire
	private GameObject SelectionnerLegumeObligatoire() {
		int rnd = Random.Range(0, nbLegumes);
		return listeLegumes[rnd];
	}
	
	// verifie dans la liste de legume plante si legume est présent
	private bool VerifierLegume(GameObject legume) {
		if (legumesPlantes.Contains(legume))
			return true;
		else
			return false;
	}


	// ajoute un nouveau legume a la liste de legumes plantés
	public bool AjouterLegume(GameObject legume) {
		
		// legume est ajouté si on n'a pas depassé nbParcelles
		if (legumesPlantes.Count < nbParcelles) {
			// si on a pas planté le legume obligatoire lorsqu'on est sur le point de plante un legume sur la derniere parcelle, on previent le joueur
			if (legumesPlantes.Count == nbParcelles - 1) {
				// si legume obligatoire a deja ete plante ou que le legume qu'on va plante est le gume obligatoire
				if (VerifierLegume(legumeObligatoire) || legumeObligatoire.name == legume.name) {
					legumesPlantes.Add(legume);
					return true;
				}
				else {
					GameManagerJardin._alertState = GameManagerJardin.AlerteState.planterLegumeObligatoire;
					return false;
				}
			}
			// on peut plante car on est a moins de nbParcelles - 1 
			else {
				legumesPlantes.Add(legume);
				return true;
			}
		}
		else {
			GameManagerJardin._alertState = GameManagerJardin.AlerteState.parcelleMaxAtteint;
			return false;
		}
	}

	public string getLegumeName(GameObject legume) {
		if (legume.name == "carotte")
			return "une carotte";
		else if (legume.name == "tomate")
			return "une tomate";
		else if (legume.name == "choux")
			return "un choux";
		else if (legume.name == "aubergine")
			return "une aubergine";
		else if (legume.name == "patate")
			return "une patate";
		else if (legume.name == "oignon")
			return "un oignon";
		else
			return "null";
	}

	public string getLegumesPlantes() {
		string txt = "Tu as plantés " + legumesPlantes.Count + " légumes:\n";
		for (int i = 0; i < legumesPlantes.Count; i++) {
			txt += legumesPlantes[i].name;

			if (i != legumesPlantes.Count - 1)
				txt += ", ";
		}
		txt += "\nLe légume obligatoire a planté est: " + getLegumeName(legumeObligatoire) + ".";
		return txt;
	}

	public string QueteP1(){
		string txt = "Bonjour, que dirais-tu de t'occuper du potager un petit peu?\n";
		txt += "Tu es libre de planter ce que tu veux.\n";
		txt += "Mais, par contre, j'aurais besoin que tu plantes\n"; 
		txt += "au moins " + getLegumeName(legumeObligatoire) + " s'il te plait.";
		return txt;
	}

}
