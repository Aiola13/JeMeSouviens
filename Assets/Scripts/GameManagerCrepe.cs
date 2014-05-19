using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    public Texture2D noemie;
    public Texture2D skypi;
	public Texture2D Tex_dialogue;

    public GameState curGameState;
    public GameState prevGameState;

	static private int brd = Screen.height/100;

	List<GameObject> listeIngQuete;

	List<GameObject> listeIngSaladier;

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

        listeIngQuete = queteCrepe.liste_quete;

		listeIngSaladier = queteCrepe.liste_saladier;
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

			GUIStyle style = new GUIStyle ();
			style.fontSize = Screen.height/36;
			style.alignment = TextAnchor.MiddleLeft;
			style.font = (Font)Resources.Load("Roboto-Regular");

			GUI.DrawTexture(new Rect(brd, Screen.height*2/3, Screen.width-brd*2, Screen.height/3 -brd), Tex_dialogue, ScaleMode.StretchToFill, true, 0);
			GUI.DrawTexture(new Rect(brd*2, Screen.height*2/3+brd, Screen.width/5, Screen.height/3 -brd*3), noemie, ScaleMode.ScaleToFit, true, 0);
			GUI.Box(new Rect(Screen.width*1/4 , Screen.height*2/3+brd*2, Screen.width-20, Screen.height/3 -10), queteCrepe.texteQuete(), style);

			//style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = Screen.height/28;
			GUI.Box(new Rect(Screen.width*2/3 , Screen.height*2/3+brd*2, Screen.width/10, Screen.height/3 -10), "TOUCHER POUR CONTINUER !", style);

            // Bouton de validation 
            if (Input.touches.Length == 1)
            {
                // interpolation pour aller plus près du plan de travail
                float temps = 1000.0f;
                Vector3 posArrive = new Vector3(0, 2, -3);
                transform.position = Vector3.Lerp(transform.position, posArrive, temps);

                // Passage à létat suivant
                curGameState = GameState.preparationPate;
                prevGameState = GameState.queteNoemie; 
            }

        }

        else if (curGameState == GameState.preparationPate)
        {
            GUI.BeginGroup(new Rect(Screen.width - (Screen.width/4), 0, Screen.width/4, Screen.height));

            if (GUI.Button(new Rect(0, 0, 100, 100), "Demander de l'aide à Skypi!"))
            {
                curGameState = GameState.aideDeSkypi;
                prevGameState = GameState.preparationPate;
            }

            if (GUI.Button(new Rect(0, (Screen.height / 3), 100, 100), "Quels sont les ingrédients dans le saladier?"))
            {
                GUI.BeginGroup(new Rect(0, Screen.height - (Screen.height / 3), Screen.width, Screen.height / 3));

                GUI.Box(new Rect(0, 0, Screen.width / 4, Screen.height / 3), new GUIContent(noemie));

                GUI.Box(new Rect(Screen.width / 4, 0, Screen.width - 2 * (Screen.width / 4), Screen.height / 3), contenuDuSaladier());

                if (GUI.Button(new Rect(5 * (Screen.width / 6), 50, 100, 100), "D'accord!"))
                {
                    curGameState = prevGameState;
                    prevGameState = GameState.aideDeSkypi;
                }

                GUI.EndGroup();

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

            if (GUI.Button(new Rect(5 * (Screen.width / 6), 50, 100, 100), "Merci Skypi!"))
            {
                curGameState = prevGameState;
                prevGameState = GameState.aideDeSkypi;
            }

            GUI.EndGroup();
        }
    }

    #endregion
	/*
    bool queteAccomplie()
    {
        bool queteAccomplie = true;
        bool dansLeSaladier = false;

        if (listeIngQuete.Length + 2 != listeIngSaladier.Length){
            queteAccomplie = false;
        }
        else{

            for (int i = 2; i < listeIngSaladier.Length; i++)
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
	*/
    string contenuDuSaladier()
    {
        string contenuDuSaladier = "Les ingrédients actuellement dans le saladier sont : \n";

        for (int i = 0; i < listeIngSaladier.Count; i++)
        {
            contenuDuSaladier += "- " + queteCrepe.nomIngredient(listeIngSaladier[i].tag) + "\n";
        }

        return contenuDuSaladier;
    }

}
