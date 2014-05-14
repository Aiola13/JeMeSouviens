using UnityEngine;
using System.Collections;

public class QueteCrepe : MonoBehaviour {

    public GameObject ing_sucre;
    public GameObject ing_farine;
    public GameObject ing_lait;
    public GameObject ing_vanille;
    public GameObject ing_sirop_erable;
    public GameObject ing_pomme;
    public GameObject ing_rhum;
    public GameObject ing_fleur_oranger;
    public GameObject ing_abricot;

    GameObject[] liste_ing;

    int[] ingDejaTire;

	// Use this for initialization
	void Start () {
        creerListe();
        afficherListe();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void creerListe() {

        // On détermine combien on veut d'ingrédients optionnels (entre 1 et 3)

        int nbOptionnels = Random.Range(1, 4);

        // Ajout des ingrédients obligatoires dans le tableau

        liste_ing = new GameObject[3 + nbOptionnels];

        liste_ing[0] = ing_sucre;
        liste_ing[1] = ing_farine;
        liste_ing[2] = ing_lait;

        // Ajout des ingrédients optionnels ds le tableau

        ingDejaTire = new int[nbOptionnels];

        int j = 0;

        for (int i = 3; i < 3 + nbOptionnels; i++)
        {
            int ingredientOpt = Random.Range(1, 7);

            if (j == 0)
            {
                ingDejaTire[j] = ingredientOpt;
                j++;
            }
            else
            {
                while (verifDejaTire(ingredientOpt, j))
                {
                    ingredientOpt = Random.Range(1, 7);
                }
                j++;
            }

            switch (ingredientOpt)
            {
                case 1 :
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

    bool verifDejaTire(int numIng, int nbIngDejaTire)
    {
        bool verif = false;

        for (int i = 0; i < nbIngDejaTire; i++)
        {
            if (numIng == ingDejaTire[i])
            {
                verif = true;
            }
        }
        return verif;
    }

    void afficherListe()
    {
        for (int i = 0; i < liste_ing.Length; i++)
        {
            Debug.Log(liste_ing[i].tag);
        }
    }

}
