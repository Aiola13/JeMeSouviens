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

    public Texture noemie;

    GameObject[] liste_ing;

    int[] ingDejaTire;

	// Use this for initialization
	void Start () {
        creerListe();
        afficherListeDebug();
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

    void afficherListeDebug()
    {
        for (int i = 0; i < liste_ing.Length; i++)
        {
            Debug.Log(liste_ing[i].tag);
        }
    }

    void OnGUI()
    {
        //GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "This is a title");
           // print("You clicked the button!");


        
        if (!noemie)
        {
            Debug.LogError("Ajouter la photo de Noemie!");
            return;
        }

        //GUI.BeginGroup(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 300, 800, 600));
        //GUI.Box(new Rect(0, 0, 800, 600), "This box is now centered! - here you would put your main menu");
        //GUI.EndGroup();

        //GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height / 3));

            // Affichage de la photo
            GUI.Box(new Rect(0, Screen.height - (Screen.height / 3), Screen.width/4, Screen.height/3), new GUIContent(noemie));

            // Affichage de la quête
            GUI.Box(new Rect(Screen.width / 4, Screen.height - (Screen.height / 3), Screen.width - (Screen.width / 4), Screen.height / 3), texteQuete());

        //GUI.EndGroup();

    }

    string texteQuete()
    {
        string quete = "Bonjour que dirais-tu de m'aider à préparer des crêpes?\n";
        quete += "Pour la crêpe d'aujourd'hui nous avons besoin des ingrédients suivants :\n";
        for (int i = 0; i < liste_ing.Length; i++)
        {
            quete += "- " + liste_ing[i].tag + "\n";
        }

            return quete;
    }

}
