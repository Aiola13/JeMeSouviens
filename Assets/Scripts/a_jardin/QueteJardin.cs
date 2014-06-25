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
		txt += "Cette fois, j'aimerais bien que tu plantes au moins " + getLegumeName(legumeObligatoire) + " s'il te plait.\n\n";
		txt += "Tu es libre de planter d'autres l\xe9gumes.\n";
		txt += "Par contre, plus tu en plantes plus il sera difficle de te souvenir de ceux que tu as plant\xe9s.\n";
		txt += "Mais meilleur sera ton score final!";
		return txt;
	}


	public string DialogueQueteP2(){
		string txt = "";
		if (legumesPlantesEnP1.Count < 5) {
			txt += "Pas mal! Tu as plant\xe9 " + legumesPlantesEnP1.Count;
			if (legumesPlantesEnP1.Count == 1)
				txt += " l\xe9gume\n\n";
			else
				txt +=  " l\xe9gumes\n\n";
            GameManagerJardin.fourchetteLegumesPlantes = 0;
		}
		else if (legumesPlantesEnP1.Count < 9) {
			txt += txt += "Bien! Tu as plant\xe9 " + legumesPlantesEnP1.Count + " l\xe9gumes\n\n";
            GameManagerJardin.fourchetteLegumesPlantes = 1;
		}
		else if (legumesPlantesEnP1.Count < 13) {
			txt += txt += "Bravo!! Tu as plant\xe9 " + legumesPlantesEnP1.Count + " l\xe9gumes\n\n";
            GameManagerJardin.fourchetteLegumesPlantes = 2;
		}
		else if (legumesPlantesEnP1.Count < 17) {
			txt += txt += "Impressionnant!!! Tu as plant\xe9 " + legumesPlantesEnP1.Count + " l\xe9gumes\n\n";
            GameManagerJardin.fourchetteLegumesPlantes = 3;
		}

		txt += "Maintenant, essai de replanter les l\xe9gumes que tu as plant\xe9.\n";
		txt += "Bonne chance!";
		return txt;
	}
	

	// formate l'objet legume en un string dependement du nombre qu'on donne
	// par defaut, getLegumeName(GameObject legume) retourne un legume au singulier
	public string getLegumeName(GameObject legume, int count = 1) {

		if (count == 0) {
			return "";
		}

		else if (legume.name == "carotte") {
			if (count == 1)
				return "une carotte";
			else 
				return count + " carottes";
		}

		else if (legume.name == "tomate") {
			if (count == 1)
				return "une tomate";
			else 
				return count + " tomates";
		}

		else if (legume.name == "choux") {
			if (count == 1)
				return "un choux";
			else 
				return count + " choux";
		}

		else if (legume.name == "aubergine") {
			if (count == 1)
				return "une aubergine";
			else 
				return count + " aubergines";
		}

		else if (legume.name == "patate") {
			if (count == 1)
				return "une patate";
			else 
				return count + " patates";
		}

		else if (legume.name == "oignon") {
			if (count == 1)
				return "un oignon";
			else 
				return count + " oignons";
		}

		else
			return "null";
	}


	// retourne un string de legumes a partir de liste
	public string getListeLegumes(List<GameObject> liste) {
		List<GameObject> checkListe = new List<GameObject>();

		string txt = "";
		for (int i = 0; i < liste.Count; i++) {
			// si liste[i] n'est pas deja dans notre checkliste
			if (!checkListe.Contains(liste[i])) {
				// on recherche toutes les occurences de liste[i]
				int count = liste.FindAll(s => s.Equals(liste[i])).Count;
				// on ajoute liste[i] a notre checkListe
				checkListe.Add(liste[i]);

				// on ajoute une virgule au string si checkliste a plus d'un element
				if (checkListe.Count > 1)
					txt += ", ";
				txt += getLegumeName(liste[i], count);
			}
		}
		return txt;
	}


	// retourne la liste des legumes plantés pendant l'une des phases, bouton info
	public string getLegumesPlantes() {
		string txt = "";
		if (legumesPlantes.Count == 0)
			txt += "Tu n'as pas encore plant\xe9 de l\xe9gumes.\n\n";
		else {
			txt = "Tu as plant\xe9s " + legumesPlantes.Count;
			if (legumesPlantes.Count == 1)
				txt += " l\xe9gume:\n";
			else
				txt += " l\xe9gumes:\n";
			txt += getListeLegumes(legumesPlantes) + "\n\n";
		}
		txt += "Il faut que tu plantes au moins " + getLegumeName(legumeObligatoire) + ".";
		return txt;
	}


	// retourne un texte donnant les legumes oublies et les legumes en trop 
	public string ScoreText() {

		// on fait des copies des listes legumesPlantes et legumesPlantesEnP1
		List<GameObject> L1 = new List<GameObject>(legumesPlantesEnP1);
		List<GameObject> L2 = new List<GameObject>(legumesPlantes);

        GameManagerJardin.nbLegumesBienPlantes = L2.Count;

		// on itere a travers la liste legumesPlantesEnP1 puis on compare avec la liste legumesPlantes (celle en p2), on retire de L1 et L2 les elements en commun
		for (int i = 0; i < legumesPlantesEnP1.Count; i++) {
			GameObject go = legumesPlantesEnP1[i];
			if (L2.Contains(go)) {
				L1.Remove(go);
				L2.Remove(go);
			}
		}

		string strLegumesOublies = getListeLegumes(L1);
		string strLegumesEnTrop = getListeLegumes(L2);

		// legumes oubliés
		string txt = "";
        if (string.IsNullOrEmpty(strLegumesOublies))
        {
            txt += "Bravo! Tu n'as pas oubli\xe9 de l\xe9gumes.\n\n";
            GameManagerJardin.nbLegumesOublies = 0;
        }
        else
        {
            txt += "Tu as oubli\xe9 de planter " + L1.Count;
            if (L1.Count == 1)
                txt += " l\xe9gume:\n";
            else
                txt += " l\xe9gumes:\n";
            txt += strLegumesOublies + "\n\n";
            GameManagerJardin.nbLegumesOublies = L1.Count;
        }

		// légumes en trop
		if (!string.IsNullOrEmpty(strLegumesEnTrop)) {
			txt += "Tu as plant\xe9 " + L2.Count;
			if (L2.Count == 1)
				txt += " l\xe9gume en trop:\n";
			else
				txt += " l\xe9gumes en trop:\n";
			txt += strLegumesEnTrop + "\n";
            GameManagerJardin.nbLegumesEnTrop = L2.Count;
		}
		else {
			txt += "Bravo! Tu n'as plant\xe9 aucun l\xe9gumes suppl\xe9mentaires.\n";
            GameManagerJardin.nbLegumesEnTrop = 0;
		}
        GameManagerJardin.nbLegumesBienPlantes -= GameManagerJardin.nbLegumesEnTrop;

        Debug.Log("Nombre de légumes bien plantés : " + GameManagerJardin.nbLegumesBienPlantes);
        Debug.Log("Nombre de légumes plantés en trop : " + GameManagerJardin.nbLegumesEnTrop);
        Debug.Log("Nombre de légumes oubliés : " + GameManagerJardin.nbLegumesOublies);
        Debug.Log("Chrono final : " + GameManagerJardin.chrono.Elapsed);
        Debug.Log("Nombre d'appels à l'aide final : " + GameManager.nbAppelsAide);
        
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
