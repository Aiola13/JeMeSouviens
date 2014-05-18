using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QueteCrepe : MonoBehaviour {

	// Ingrédients obligatoires
    public GameObject ing_sucre;
    public GameObject ing_farine;
    public GameObject ing_lait;
    public GameObject ing_oeuf;
    public GameObject ing_sel;

	// Ingrédients optionnels
    public GameObject ing_vanille;
    public GameObject ing_sirop_erable;
    public GameObject ing_pomme;
    public GameObject ing_rhum;
    public GameObject ing_fleur_oranger;
    public GameObject ing_bleuet;

	public List<GameObject> liste_quete;
	public List<GameObject> liste_saladier;

	List<GameObject> ing_obligatoire;
	List<GameObject> ing_optionnels;
	

	// Use this for initialization
	void Start () {

		intitalisationListe();

		Shuffle(ing_obligatoire);
        
		Shuffle(ing_optionnels);
        
		repartition();
        
		afficherListeDebug();
	}
	
	// Update is called once per frame
	void Update () {


	}

	void intitalisationListe(){

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

		int nbAEnlever = Random.Range(1, 4);

		for (int i = 0; i<nbAEnlever; i++){
			int k = Random.Range(0 ,ing_optionnels.Count + 1);
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

	// Répartition des ingrédiants obligatoires : Les 2 premiers de la liste sont ceux que Noémie a déja mis dans le saladier
	// Les 3 derniers font partis de la quête
	void repartition(){
		for (int i = 0; i<ing_obligatoire.Count; i++){
			if (i < 2 ){
				liste_saladier.Add(ing_obligatoire[i]);
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
			Debug.Log(liste_saladier[i].tag);
		}
    }

    public string texteQuete()
    {
        string quete = "Bonjour que dirais-tu de m'aider à préparer des crêpes?\n";
        quete += "J'ai déjà mis quelques ingrédients dans le saladier!\n";
        quete += "Voici les ingrédients que tu dois ajouter pour finir la pâte :\n";
        for (int i = 0; i < liste_quete.Count; i++)
        {
			quete += "- " + nomIngredient(liste_quete[i].tag) + "\n";
        }

            return quete;
    }

    public string nomIngredient(string tag)
    {
        string nomIng = "";

        switch (tag)
        {
            case "ing_abricot":
                nomIng = "un abricot";
                break;
            case "ing_sucre":
                nomIng = "du sucre";
                break;
            case "ing_farine":
                nomIng = "de la farine";
                break;
            case "ing_lait":
                nomIng = "du lait";
                break;
            case "ing_oeuf":
                nomIng = "des oeufs";
                break;
            case "ing_sel":
                nomIng = "du sel";
                break;
            case "ing_vanille":
                nomIng = "de la vanille";
                break;
            case "ing_sirop_erable":
                nomIng = "du sirop d'érable";
                break;
            case "ing_pomme":
                nomIng = "une pomme";
                break;
            case "ing_rhum":
                nomIng = "du rhum";
                break;
            case "ing_fleur_oranger":
                nomIng = "de la fleur d'oranger";
                break;

        }

        return nomIng;

    }

}
