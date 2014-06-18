using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class QueteJardin : MonoBehaviour {

	private GameManagerJardin gmJardin;

	private int nbLegumes = 0;
	private int nbParcelles = 0;

	public int nbLegumesArroses = 0;			// permet de lever une alerte lorsqu'unlegume a oublie d'etre arrosé

	public GameObject legumeObligatoire;

	private GameObject[] listeLegumes;
	public List<GameObject> legumesPlantes;
	public List<GameObject> legumesPlantesEnP1;


	void Start () {
		gmJardin = GetComponent<GameManagerJardin>();

		InitQueteP1();
	}


	#region Dialogue and String Management
	public string DialogueQueteP1(){
		string txt = "Bonjour, que dirais-tu de t'occuper du potager un petit peu?\n";
		txt += "Tu es libre de planter ce que tu veux.\n";
		txt += "Mais, par contre, j'aurais besoin que tu plantes\n"; 
		txt += "au moins " + getLegumeName(legumeObligatoire) + " s'il te plait.";
		return txt;
	}


	public string DialogueQueteP2(){
		string txt = "Essai de reconstituer les l\xe9gumes que tu avais plant\xe9.\n";
		return txt;
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


	// retourne un string de legumes a partir de liste
	public string getListeLegumes(List<GameObject> liste) {
		string txt = "";
		for (int i = 0; i < liste.Count; i++) {
			txt += liste[i].name;
			if (i != liste.Count - 1)
				txt += ", ";
		}
		return txt;
	}


	// retourne la liste des legumes plantés pendant l'une des phases
	public string getLegumesPlantes() {
		string txt = "";
		if (legumesPlantes.Count == 0)
			txt += "Tu n'as pas encore plant\xe9 de l\xe9gumes.";
		else {
			txt = "Tu as plant\xe9s " + legumesPlantes.Count + " l\xe9gume";
			if (legumesPlantes.Count >1)
				txt += "s";
			txt += ":\n";
			txt += getListeLegumes(legumesPlantes);
		}
		txt += "\nLe l\xe9gume obligatoire a plant\xe9 est " + getLegumeName(legumeObligatoire) + ".";
		return txt;
	}


	// retourne un texte donnant les legumes oublies et les legumes en trop 
	public string ScoreText() {

		// on fait des copies des listes legumesPlantes et legumesPlantesEnP1
		List<GameObject> L1 = new List<GameObject>(legumesPlantesEnP1);
		List<GameObject> L2 = new List<GameObject>(legumesPlantes);
		
		// on itere a travers la liste legumesPlantesEnP1 puis on compare avec la liste legumesPlantes (celle en p2), on retire de L1 et L2 les elements en commun
		for (int i = 0; i < legumesPlantesEnP1.Count; i++) {
			GameObject go = legumesPlantesEnP1[i];
			if (L2.Contains(go)) {
				L1.Remove(go);
				L2.Remove(go);
			}
		}
		
		string legumesOublies = getListeLegumes(L1);
		string legumesEnTrop = getListeLegumes(L2);
		
		string txt = "";
		if (string.IsNullOrEmpty(legumesOublies))
			txt += "Bravo! Tu n'as pas oubli\xe9 de l\xe9gumes.\n";
		else {
			txt += "Tu as oubli\xe9 de planter " + L1.Count + " l\xe9gume";
			if (L1.Count > 1)
				txt += "s";
			txt += ":\n" + legumesOublies + "\n";
		}
		
		if (!string.IsNullOrEmpty(legumesEnTrop)) {
			txt += "Tu as plant\xe9 " + L2.Count + " l\xe9gume";
			if (L2.Count > 1)
				txt += "s";
			txt += " en trop:\n" + legumesEnTrop + "\n";
		}
		
		return txt;
	}
	#endregion


	// initialisation de la quete en phase 1
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

		// set le legume obligatoire a planté
		legumeObligatoire = SelectionnerLegumeObligatoire();
	}


	// Reset les parcelles et enleve les graines crées
	public void InitQueteP2() {

		// on enregistre la liste de legumes plantés en p1
		legumesPlantesEnP1 = new List<GameObject>(legumesPlantes);
		legumesPlantes.Clear();
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


	
}
