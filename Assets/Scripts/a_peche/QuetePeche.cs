using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuetePeche : MonoBehaviour {


	public List<string> listeQuete;

    public List<string> listeCorrecte;

    public List<string> listeIncorrecte;

	// Use this for initialization
	void Start () {

        remplirListeQuete();
        //afficherListeDebug();
	}


	private void remplirListeQuete(){

        // Selection des 5 poissons a pecher
        for (int i = 0; i < 5; i++) {
            int j = Random.Range(1, 6);
            switch(j){
                case 1:
                    listeQuete.Add("poi_eperlant");
                    break;
                case 2:
                    listeQuete.Add("poi_turbot");
                    break;
                case 3:
                    listeQuete.Add("poi_morue");
                    break;
                case 4:
                    listeQuete.Add("poi_saumon");
                    break;
                case 5:
                    listeQuete.Add("poi_sebaste");
                    break;
            }
        }


	}

    void afficherListeDebug() {
        for (int i = 0; i < listeQuete.Count; i++) {
            Debug.Log(listeQuete[i]);
        }
    }


    public string toString(string tag, List<string> liste) {

        string nomPoisson = "";

        int nbPoissons = 0;
        for (int i = 0; i < liste.Count; i++) {
            if (liste[i] == tag)
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

	public string poissonsManquants(){

        string poissonsManquants = "Voici les poissons qu'il reste à pêcher : \n\n";

        poissonsManquants += ToStringListePoissons(listeQuete);

        return poissonsManquants;
	}

    
	public bool verifVictoire(){

        if (listeQuete.Count == 0) {
            return true;
        }

        return false;
	}
    
    string nomPoisson(string tag) {

        switch (tag) {

            case "poi_eperlant":
                tag = "eperlant";
                break;
            case "poi_turbot":
                tag = "turbot";
                break;
            case "poi_morue":
                tag = "morue";
                break;
            case "poi_saumon":
                tag = "saumon";
                break;
            case "poi_sebaste":
                tag = "sebaste";
                break;
        }

        return tag;
    }

	public string texteQuete(){

		string quete = "Bonjour est ce que tu serais partant pour une bonne partie de pêche?\n";
		quete += "Voici la liste des poissons qu'il faut pêcher, relâche ceux qui ne sont pas dans la liste!\n";

        quete += ToStringListePoissons(listeQuete);
		
		return quete;
	}

    public string ToStringListePoissons(List<string> liste) {

        List<string> dejaAffiche = new List<string>();

        string listePoissons = "";

        for (int i = 0; i < liste.Count; i++) {
            if (i == 0) {
                dejaAffiche.Add(liste[i]);
                listePoissons += "- " + toString(liste[i], liste) + "\n";
            } else if (!dejaAffiche.Contains(liste[i])) {
                dejaAffiche.Add(liste[i]);
                listePoissons += "- " + toString(liste[i], liste) + "\n";
            }
        }

        return listePoissons;
    }

}
