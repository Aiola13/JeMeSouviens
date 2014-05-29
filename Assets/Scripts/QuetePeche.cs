using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuetePeche : MonoBehaviour {


	public List<GameObject> listeQuete;

	public List<GameObject> listePanier;

	// Use this for initialization
	void Start () {

        listePanier = new List<GameObject>();

        remplirListeQuete();
	}


	private void remplirListeQuete(){

        listeQuete = new List<GameObject>();

        // Selection des 5 poissons a pecher
        for (int i = 0; i < 5; i++) {
            int j = Random.Range(1, 6);
            switch(j){
                case 1:
                    listeQuete.Add(GameObject.FindGameObjectWithTag("poi_eperlant"));
                    break;
                case 2:
                    listeQuete.Add(GameObject.FindGameObjectWithTag("poi_turbot"));
                    break;
                case 3:
                    listeQuete.Add(GameObject.FindGameObjectWithTag("poi_morue"));
                    break;
                case 4:
                    listeQuete.Add(GameObject.FindGameObjectWithTag("poi_saumon"));
                    break;
                case 5:
                    listeQuete.Add(GameObject.FindGameObjectWithTag("poi_sebaste"));
                    break;
            }
        }


	}

	public bool queteAccomplie() {
		bool queteAccomplie = true;
		
		for (int i = 0; i < listeQuete.Count; i++) {
			if (!listePanier.Contains(listeQuete[i])) {
				queteAccomplie = false;
			}
		}
		
		return queteAccomplie;
	}

	public string toString(string tag, List<GameObject> liste){
		
        string nomPoisson = "";

        int nbPoissons = 0;
        for (int i = 0; i < liste.Count; i++) {
            if (liste[i].tag == tag)
                nbPoissons++;
        }

        if (nbPoissons > 1) {
            switch (tag) {

                case "poi_eperlant":
                    nomPoisson = nbPoissons + " eperlants";
                    break;
                case "poi_turbot":
                    nomPoisson = nbPoissons + " turbots";
                    break;
                case "poi_morue":
                    nomPoisson = nbPoissons + " morues";
                    break;
                case "poi_saumon":
                    nomPoisson = nbPoissons + " saumons";
                    break;
                case "poi_sebaste":
                    nomPoisson = nbPoissons + " sebastes";
                    break;
            }
        } else {
            switch (tag) {

                case "poi_eperlant":
                    nomPoisson = nbPoissons + " eperlant";
                    break;
                case "poi_turbot":
                    nomPoisson = nbPoissons + " turbot";
                    break;
                case "poi_morue":
                    nomPoisson = nbPoissons + " morue";
                    break;
                case "poi_saumon":
                    nomPoisson = nbPoissons + " saumon";
                    break;
                case "poi_sebaste":
                    nomPoisson = nbPoissons + " sebaste";
                    break;
            }
        }



		return nomPoisson;
	}

	public string contenuDuPanier(){
		string contenuDuPanier = "Actuellement dans le panier : \n\n";
		
		for (int i = 0; i < listePanier.Count; i++)
		{
            contenuDuPanier += "- " + toString(listePanier[i].tag, listePanier) + "\n";
		}

        return contenuDuPanier;
	}

	public string poissonsManquants(){

        string poissonsManquants = "Voici les poissons qu'il reste à pêcher : \n\n";

        for (int i = 0; i < listeQuete.Count; i++) {
            poissonsManquants += "- " + toString(listeQuete[i].tag, listeQuete) + "\n";
        }

        return poissonsManquants;
	}

	public bool ajouterPoissonDansPanier(GameObject poisson){
        
        bool poissonValide = false;

        // On vérifie que l ingrédient mis dans le saladier fait partie de la quete
        for (int i = 0; i < listeQuete.Count; i++) {
            if (listeQuete.Contains(poisson)) {
                poissonValide = true;
            }
        }
        // Si oui on l ajoute a la liste et on le détruit ensuite

        if (poissonValide) {
            listePanier.Add(poisson);
            listeQuete.Remove(poisson);
        }
        return poissonValide;
	}

	public string texteQuete(){

		string quete = "Bonjour est ce que tu serais partant pour une bonne partie de pêche?\n";
		quete += "Voici la liste des poissons qu'il faut pêcher, relâche ceux qui ne sont pas dans la liste!\n";
		
		for (int i = 0; i < listeQuete.Count; i++)
		{
            quete += "- " + toString(listeQuete[i].tag, listeQuete) + "\n";

		}
		
		return quete;
	}


}
