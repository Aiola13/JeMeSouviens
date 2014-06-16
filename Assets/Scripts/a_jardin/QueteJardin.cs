using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QueteJardin : MonoBehaviour {

	private GameManagerJardin gmJardin;

	private int nbLegumes = 0;
	private int nbParcelles = 0;

	public int nbLegumesArroses = 0;			// permet de lever une alerte lorsqu'unlegume a oublie d'etre arrosé

	private GameObject[] listeLegumes;
	public List<GameObject> legumesPlantes;
	public List<GameObject> legumesPlantesEnP1;

	public List<GameObject> test;


	public GameObject legumeObligatoire;


	void Start () {
		gmJardin = GetComponent<GameManagerJardin>();

		InitQueteP1();


		test.Add (listeLegumes[0]);
		test.Add (listeLegumes[1]);
		test.Add (listeLegumes[2]);
		//ComparerLegumes(legumesPlantesEnP1, legumesPlantes);
		ComparerLegumes(test, legumesPlantes);
	}


	/*
	void Update() {

		string txt = "obl: [" + legumeObligatoire.name + "]  ||  " + legumesPlantes.Count + " Sup: ";
		for (int i = 0; i < legumesPlantes.Count; i++) {
			txt = txt + legumesPlantes[i].name + "   ";
		}
		print(txt);
	}
	*/


	public string DialogueQueteP1(){
		string txt = "Bonjour, que dirais-tu de t'occuper du potager un petit peu?\n";
		txt += "Tu es libre de planter ce que tu veux.\n";
		txt += "Mais, par contre, j'aurais besoin que tu plantes\n"; 
		txt += "au moins " + getLegumeName(legumeObligatoire) + " s'il te plait.";
		return txt;
	}


	public string DialogueQueteP2(){
		string txt = "Essai de reconstituer les légumes que tu avais planté.\n";
		return txt;
	}


	//
	public void InitQueteP1() {

		// nombre de légumes differents dans l'ui
		nbLegumes = GameObject.Find("UI_Legumes").transform.childCount;
		listeLegumes = new GameObject[nbLegumes];

		// initialise la liste de legumes
		int count = 0;
		foreach (Transform child in GameObject.Find("UI_Legumes").transform) {
			listeLegumes[count] = child.gameObject;
			count++;
		}
		
		// nombre de parcelles dans le potager
		foreach (Transform bloc in GameObject.Find("Potager").transform) {
			// on itere dans les bloc du potager
			foreach (Transform par in bloc) {
				nbParcelles++;
			}
		}

		// override nbParcelles here
		nbParcelles = 3;

		// set le legume obligatoire a planté
		legumeObligatoire = SelectionnerLegumeObligatoire();
	}


	//
	public void InitQueteP2() {

		// on enregistre la liste de legumes plantés en p1
		legumesPlantesEnP1 = legumesPlantes;
		legumesPlantes = null;
		nbLegumesArroses = 0;

		// on reset les parcelles
		foreach (Transform bloc in GameObject.Find("Potager").transform) {
			// on itere dans les bloc du potager
			foreach (Transform par in bloc) {
				par.GetComponent<Parcelle>().ResetParcelle();
			}
		}

		// on destroy les graines
		foreach (GameObject graine in GameObject.FindGameObjectsWithTag("Graine")) {
			Destroy(graine);
		}
	}


	// retourne un legume aleatoire
	private GameObject SelectionnerLegumeObligatoire() {
		int rnd = Random.Range(0, nbLegumes);
		return listeLegumes[rnd];
	}


	// verifie dans la liste de legume plante si le legume obligatoire est présent
	public bool VerifierLegumeObligatoire() {
		if (legumesPlantes.Contains(legumeObligatoire))
			return true;
		else
			return false;
	}


	// verifie dans la liste de legume plante si legume est présent
	private bool VerifierLegume(GameObject legume) {
		if (legumesPlantes.Contains(legume))
			return true;
		else
			return false;
	}


	// ajoute un nouveau legume a la liste de legumes plantés et retourne vrai si le legume a ete plante
	public bool VerifierAjoutLegume(GameObject legume) {
		
		// legume est ajouté si on n'a pas depassé nbParcelles
		if (legumesPlantes.Count < nbParcelles) {
			// si on a pas planté le legume obligatoire lorsqu'on est sur le point de plante un legume sur la derniere parcelle, on previent le joueur
			if (legumesPlantes.Count == nbParcelles - 1) {
				// si legume obligatoire a deja ete plante ou que le legume qu'on va plante est legume obligatoire
				if (VerifierLegumeObligatoire() || legumeObligatoire.name == legume.name) {
					legumesPlantes.Add(legume);
					return true;
				}
				else {
					gmJardin.SetAlerte(GameManagerJardin.AlerteState.planterLegumeObligatoire);
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
			gmJardin.SetAlerte(GameManagerJardin.AlerteState.parcelleMaxAtteint);
			return false;
		}
	}


	// Compare les légumes de list1 avec list2
	void ComparerLegumes(List<GameObject> list1, List<GameObject> list2) {

		List<GameObject> l1;
		List<GameObject> l2;

		//for (GameObject go in l1 {

		//}
		//legumesPlantes.
	}


	// incremente de 1 le nombre de legumes arroses
	public void IncrementNbLegumesArroses() {
		nbLegumesArroses++;
	}


	// retourne vrai si tous les legumes ont ete arroses
	public bool VerifierLegumesArroses() {
		if (nbLegumesArroses == legumesPlantes.Count)
			return true;
		else {
			return false;
		}

	}


	// permet d'avoir le bon article devant chaque légume
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


	// retourne la liste des legumes plantés pendant l'une des phases
	public string getLegumesPlantes() {
		string txt = "Tu as plantés " + legumesPlantes.Count + " légumes:\n";
		for (int i = 0; i < legumesPlantes.Count; i++) {
			txt += legumesPlantes[i].name;
			if (i != legumesPlantes.Count - 1)
				txt += ", ";
		}
		txt += "\nLe légume obligatoire a planté est " + getLegumeName(legumeObligatoire) + ".";
		return txt;
	}

}
