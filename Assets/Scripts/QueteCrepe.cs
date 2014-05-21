using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QueteCrepe : MonoBehaviour {

	// Ingrédients obligatoires
    private GameObject ing_sucre;
    private GameObject ing_farine;
    private GameObject ing_lait;
    private GameObject ing_oeuf;
    private GameObject ing_sel;

	// Ingrédients optionnels
    private GameObject ing_vanille;
    private GameObject ing_sirop_erable;
    private GameObject ing_pomme;
    private GameObject ing_rhum;
    private GameObject ing_fleur_oranger;
    private GameObject ing_bleuet;

	public List<GameObject> liste_quete;
	public List<string> liste_saladier;

	List<GameObject> ing_obligatoire;
	List<GameObject> ing_optionnels;

	void Start () {
		// Ingrédients obligatoires
		ing_sucre = GameObject.FindGameObjectWithTag("ing_sucre");
		ing_farine = GameObject.FindGameObjectWithTag("ing_farine");
		ing_lait = GameObject.FindGameObjectWithTag("ing_lait");
		ing_oeuf = GameObject.FindGameObjectWithTag("ing_oeuf");
		ing_sel = GameObject.FindGameObjectWithTag("ing_sel");
		
		// Ingrédients optionnels
		ing_vanille = GameObject.FindGameObjectWithTag("ing_vanille");
		ing_sirop_erable = GameObject.FindGameObjectWithTag("ing_sirop_erable");
		ing_pomme = GameObject.FindGameObjectWithTag("ing_pomme");
		ing_rhum = GameObject.FindGameObjectWithTag("ing_rhum");
		ing_fleur_oranger = GameObject.FindGameObjectWithTag("ing_fleur_oranger");
		ing_bleuet = GameObject.FindGameObjectWithTag("ing_bleuet");

		initialisationListe();
		selectionIngOptionnels();
		Shuffle(ing_obligatoire);
		Shuffle(ing_optionnels);
		repartition();
		//afficherListeDebug();
	}

	void initialisationListe(){

		// Initialisation de la liste d'ingrédients aléatoire
		ing_obligatoire = new List<GameObject>();
		ing_obligatoire.Add(ing_sucre);
		ing_obligatoire.Add(ing_farine);
		ing_obligatoire.Add(ing_lait);
		ing_obligatoire.Add(ing_oeuf);
		ing_obligatoire.Add(ing_sel);

		// Initialisation de la liste d'ingrédients optionnels
		ing_optionnels = new List<GameObject>();
		ing_optionnels.Add(ing_vanille);
		ing_optionnels.Add(ing_sirop_erable);
		ing_optionnels.Add(ing_pomme);
		ing_optionnels.Add(ing_rhum);
		ing_optionnels.Add(ing_fleur_oranger);
		ing_optionnels.Add(ing_bleuet);
	}

	// Permet de séléctionner les ingrédients optionnels à retirer
	void selectionIngOptionnels(){

        int nbAEnlever = Random.Range(3, ing_optionnels.Count);

        for (int i = 0; i < nbAEnlever; i++)
        {
			int k = Random.Range(0 ,ing_optionnels.Count);
			ing_optionnels.RemoveAt(k);
		}
	}


	// Algo permettant de mélanger la liste passée en paramêtre
	public void Shuffle(IList<GameObject> list)  
	{  
		int n = list.Count;  
		while (n > 1) {  
			n--;  
			int k = Random.Range(0 ,n + 1);  
			GameObject value = list[k];  
			list[k] = list[n];  
			list[n] = value;  
		}  
	}

	// Répartition des ingrédients obligatoires : Les 2 premiers de la liste sont ceux que Noémie a déja mis dans le saladier
	// Les 3 derniers font partis de la quête
	void repartition(){
		for (int i = 0; i<ing_obligatoire.Count; i++){
			if (i < 2 ){
				liste_saladier.Add(ing_obligatoire[i].tag);
                ing_obligatoire[i].SetActive(false);
			}
			else {
				liste_quete.Add(ing_obligatoire[i]);
			}
		}
		for (int i = 0; i<ing_optionnels.Count; i++){
			liste_quete.Add(ing_optionnels[i]);
		}
	}
	

    void afficherListeDebug()
    {
		for (int i = 0; i < liste_quete.Count; i++)
        {
			Debug.Log(liste_quete[i].tag);
        }

		for (int i = 0; i < liste_saladier.Count; i++)
		{
			Debug.Log(liste_saladier[i]);
		}
    }

    public string texteQuete()
    {
        string quete = "Bonjour que dirais-tu de m'aider à préparer des crêpes?\n";
        quete += "J'ai déjà mis quelques ingrédients dans le saladier!\n";
        quete += "Voici les ingrédients que tu dois ajouter pour finir la pâte :\n\n";

		for (int i = 0; i < liste_quete.Count; i++)
		{
			quete += "- " + nomIngredient(liste_quete[i].tag) + "\t";
			if(i==1 || i==3) quete += "\n";
		}

        return quete;
    }



    public string nomIngredient(string tag)
    {
        string nomIng = "";

        switch (tag)
        {

			case "ing_bleuet":
				nomIng = "des bleuets\t\t\t\t\t";
				break;
			case "ing_sucre":
				nomIng = "du sucre\t\t\t\t\t";
				break;
			case "ing_farine":
				nomIng = "de la farine\t\t\t\t";
				break;
			case "ing_lait":
				nomIng = "du lait\t\t\t\t\t\t\t";
				break;
			case "ing_oeuf":
				nomIng = "des oeufs\t\t\t\t\t";
				break;
			case "ing_sel":
				nomIng = "du sel\t\t\t\t\t\t\t";
				break;
			case "ing_vanille":
				nomIng = "de la vanille\t\t\t\t";
				break;
			case "ing_sirop_erable":
				nomIng = "du sirop d'érable\t\t";
				break;
	        case "ing_pomme":
				nomIng = "une pomme\t\t\t\t";
				break;
			case "ing_rhum":
				nomIng = "du rhum\t\t\t\t\t\t";
				break;
			case "ing_fleur_oranger":
				nomIng = "de la fleur d'oranger\t";
				break;

        }

        return nomIng;

    }

	public bool queteAccomplie() {
		bool queteAccomplie = true;
		
		for (int i = 0; i < liste_quete.Count; i++) {
			if (!liste_saladier.Contains(liste_quete[i].tag)) {
				queteAccomplie = false;
			}
		}
		
		return queteAccomplie;
	}


    public bool ajouterIngredientSaladier(GameObject ingredient)
    {
        bool ingredientValide = false;

        // On vérifie que l ingrédient mis dans le saladier fait partie de la quete
        for (int i = 0; i < liste_quete.Count; i++)
        {
            if (liste_quete.Contains(ingredient))
            {
                ingredientValide = true;
            }
        }
        // Si oui on l ajoute a la liste et on le détruit ensuite

        if (ingredientValide)
        {
            liste_saladier.Add(ingredient.tag);
            ingredient.SetActive(false);
        }
        return ingredientValide;
    }


    public string contenuDuSaladier()
    {
        string contenuDuSaladier = "Actuellement dans le saladier : \n";

        for (int i = 0; i < liste_saladier.Count; i++)
        {
            contenuDuSaladier += "- " + nomIngredient(liste_saladier[i]) + "\n";
        }

        return contenuDuSaladier;
    }


    public string ingredientManquants()
    {
        string ingManquant = "";

        for (int i = 0; i < liste_quete.Count; i++)
        {
            if (!liste_saladier.Contains(liste_quete[i].tag))
            {
                ingManquant += "- " + nomIngredient(liste_quete[i].tag) + "\n";
            }
        }
        return ingManquant;
    }


}
