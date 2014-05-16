using UnityEngine;
using System.Collections;

public class GameManagerCrepe : MonoBehaviour
{

    #region attributs

    public enum GameState
    {
        queteNoemie,
        preparationPate,
        etalerLeBeurre,
        cuissonCrepe,
        aideDeSkypi
    }

    public QueteCrepe queteCrepe;

    public Texture noemie;
    public Texture skypi;

    public GameState curGameState;
    public GameState prevGameState;

    GameObject[] listeIngQuete;

    GameObject[] listeIngSaladier;

    #endregion

    #region accesseurs

    //public gamestate getcurgamestate()
    //{
    //    get{
    //        return curgamestate;
    //    }
    //    set{
    //        curgamestate = value;
    //    }
    //}

    //public gamestate getprevgamestate()
    //{
    //    get{
    //        return prevgamestate;
    //    }
    //    set{
    //        prevgamestate = value;
    //    }
    //}

    #endregion

    // Use this for initialization
	void Start () {

        curGameState = GameState.queteNoemie;

        queteCrepe = GetComponent<QueteCrepe>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    #region GUI

    void OnGUI()
    {
        if (!noemie || !skypi)
        {
            Debug.LogError("Ajouter les textures!");
            return;
        }

        if (curGameState == GameState.queteNoemie)
        {

            GUI.BeginGroup(new Rect(0, Screen.height - (Screen.height / 3), Screen.width, Screen.height / 3));

            // Affichage de la photo
            GUI.Box(new Rect(0, 0, Screen.width / 4, Screen.height / 3), new GUIContent(noemie));

            // Affichage de la quête
            GUI.Box(new Rect(Screen.width / 4, 0, Screen.width - 2 * (Screen.width / 4), Screen.height / 3), queteCrepe.texteQuete());

            // Bouton de validation 
            if (GUI.Button(new Rect(5 * (Screen.width / 6), 50, 100, 100), "J'ai compris!"))
            {
                // interpolation pour aller plus près du plan de travail
                float temps = 1000.0f;
                Vector3 posArrive = new Vector3(0, 2, -3);
                transform.position = Vector3.Lerp(transform.position, posArrive, temps);

                // Passage à létat suivant
                curGameState = GameState.preparationPate;
                prevGameState = GameState.queteNoemie; 
            }

            GUI.EndGroup();

        }

        else if (curGameState == GameState.preparationPate)
        {
            GUI.BeginGroup(new Rect(Screen.width - (Screen.width/4), 0, Screen.width/4, Screen.height));

            if (GUI.Button(new Rect(0, 0, 100, 100), "Demander de l'aide à Skypi!"))
            {
                curGameState = GameState.aideDeSkypi;
                prevGameState = GameState.preparationPate;
            }

            if (GUI.Button(new Rect(0, (Screen.height / 3), 100, 100), "Quels sont les ingrédients à ajouter déjà?"))
            {


            }

            if (GUI.Button(new Rect(0, 2 * (Screen.height / 3), 100, 100), "La pâte est prête!"))
            {
                curGameState = GameState.etalerLeBeurre;
                prevGameState = GameState.preparationPate;
            }

            GUI.EndGroup();
        }

        else if (curGameState == GameState.etalerLeBeurre)
        {

        }
        else if (curGameState == GameState.cuissonCrepe)
        {

        }
        else if (curGameState == GameState.aideDeSkypi)
        {
            GUI.BeginGroup(new Rect(0, Screen.height - (Screen.height / 3), Screen.width, Screen.height / 3));

            GUI.Box(new Rect(0, 0, Screen.width / 4, Screen.height / 3), new GUIContent(skypi));

            GUI.Box(new Rect(Screen.width / 4, 0, Screen.width - 2 * (Screen.width / 4), Screen.height / 3), "Pour mettre des ingrédients dans le saladier, il te suffit de les faire glisser dedans!");

            // Bouton de validation 
            if (GUI.Button(new Rect(5 * (Screen.width / 6), 50, 100, 100), "Merci Skypi!"))
            {
                // Passage à létat suivant
                curGameState = prevGameState;
                prevGameState = GameState.aideDeSkypi;
            }

            GUI.EndGroup();
        }
    }

    #endregion

    bool queteAccomplie()
    {
        bool queteAccomplie = true;
        bool dansLeSaladier = false;

        if (listeIngQuete.Length != listeIngSaladier.Length){
            queteAccomplie = false;
        }
        else{

            for (int i = 0; i < listeIngSaladier.Length; i++)
            {
                int compteur = 0;
                while (!dansLeSaladier)
                {
                    if (listeIngSaladier[i].tag == listeIngQuete[compteur].tag && compteur < listeIngSaladier.Length)
                    {
                        dansLeSaladier = true;
                    }
                    else if (compteur >= listeIngSaladier.Length)
                    {
                        queteAccomplie = false;
                    }
                    compteur++;
                }
            }
        }

        return queteAccomplie;
    }
}
