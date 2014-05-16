using UnityEngine;
using System.Collections;

public class QueteCrepe : MonoBehaviour {

    public GameObject ing_sucre;
    public GameObject ing_farine;
    public GameObject ing_lait;
    public GameObject ing_oeuf;
    public GameObject ing_sel;
    public GameObject ing_vanille;
    public GameObject ing_sirop_erable;
    public GameObject ing_pomme;
    public GameObject ing_rhum;
    public GameObject ing_fleur_oranger;
    public GameObject ing_abricot;

    GameObject[] liste_ing;

    int nbOptionnels;
    int[] ingDejaTireOpt;

    int[] ingDejaTireObl;

    bool showGUI = true;

	// Use this for initialization
	void Start () {
        
        nbOptionnels = Random.Range(1, 4);
        
        liste_ing = new GameObject[3 + nbOptionnels];
        
        ingredientsObligatoires();
        
        ingredientsOptionnels();
        
        afficherListeDebug();
	}
	
	// Update is called once per frame
	void Update () {


	}

    void ingredientsOptionnels()
    {
        ingDejaTireOpt = new int[nbOptionnels];

        int j = 0;

        for (int i = 3; i < 3 + nbOptionnels; i++)
        {
            int ingredientOpt = Random.Range(1, 7);

            if (j == 0)
            {
                ingDejaTireOpt[j] = ingredientOpt;
                j++;
            }
            else
            {
                while (verifDejaTire(ingredientOpt, j, ingDejaTireOpt))
                {
                    ingredientOpt = Random.Range(1, 7);

                }
                ingDejaTireOpt[j] = ingredientOpt;
                j++;
            }

            switch (ingredientOpt)
            {
                case 1:
                    liste_ing[i] = ing_vanille;
                    break;

                case 2:
                    liste_ing[i] = ing_sirop_erable;
                    break;

                case 3:
                    liste_ing[i] = ing_pomme;
                    break;

                case 4:
                    liste_ing[i] = ing_rhum;
                    break;

                case 5:
                    liste_ing[i] = ing_fleur_oranger;
                    break;

                case 6:
                    liste_ing[i] = ing_abricot;
                    break;

            }
        }
    }

    void ingredientsObligatoires()
    {
        ingDejaTireObl = new int[3];

        for (int i = 0; i < 3; i++)
        {
            int ingredientObl = Random.Range(1, 6);

            if (i == 0)
            {
                ingDejaTireObl[i] = ingredientObl;
            }
            else
            {
                while (verifDejaTire(ingredientObl, i, ingDejaTireObl))
                {
                    ingredientObl = Random.Range(1, 6);

                }
                ingDejaTireObl[i] = ingredientObl;
            }

            switch (ingredientObl)
            {
                case 1:
                    liste_ing[i] = ing_sucre;
                    break;

                case 2:
                    liste_ing[i] = ing_farine;
                    break;

                case 3:
                    liste_ing[i] = ing_lait;
                    break;

                case 4:
                    liste_ing[i] = ing_oeuf;
                    break;

                case 5:
                    liste_ing[i] = ing_sel;
                    break;

            }
        }
    }

    bool verifDejaTire(int numIng, int nbIngDejaTire, int[] tab)
    {
        bool verif = false;

        for (int i = 0; i < nbIngDejaTire; i++)
        {
            if (numIng == tab[i])
            {
                verif = true;
            }
        }
        return verif;
    }

    void afficherListeDebug()
    {
        for (int i = 0; i < liste_ing.Length; i++)
        {
            Debug.Log(liste_ing[i].tag);
        }
    }

    public string texteQuete()
    {
        string quete = "Bonjour que dirais-tu de m'aider à préparer des crêpes?\n";
        quete += "J'ai déjà mis quelques ingrédients dans le saladier!\n";
        quete += "Voici les ingrédients que tu dois ajouter pour finir la pâte :\n";
        for (int i = 0; i < liste_ing.Length; i++)
        {
            quete += "- " + nomIngredient(liste_ing[i].tag) + "\n";
        }

            return quete;
    }

    string nomIngredient(string tag)
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
