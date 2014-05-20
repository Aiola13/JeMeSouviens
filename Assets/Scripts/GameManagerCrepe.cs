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

    private QueteCrepe queteCrepe;

    public Texture2D noemie;
    public Texture2D skypi;
	public Texture2D Tex_dialogue;

    public GameState curGameState;
    public GameState prevGameState;

	static private int brd = Screen.height/100;

<<<<<<< HEAD
	List<string> listeIngSaladier;
=======
	List<GameObject> listeIngQuete;
	List<GameObject> listeIngSaladier;
>>>>>>> 6fda35f639192ef6c87f21b37f9c4b13ec672eeb
    public AudioClip musiqueAmbiance;
    

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

        queteCrepe = new QueteCrepe();

		listeIngSaladier = queteCrepe.liste_saladier;
	
        //Musique d'ambiance ici ?
        AudioSource sourceAudio = gameObject.AddComponent<AudioSource>();
        audio.clip = musiqueAmbiance;
        audio.loop = true;
        audio.Play();
        
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
			GUI.Box(new Rect(Screen.width*1/4 , Screen.height*7/10+brd*2, Screen.width-20, Screen.height/5 -10), queteCrepe.texteQuete(), style);

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

            if (GUI.Button(new Rect(0, 0, 150, 100), "La pâte est prête!"))
            {
                if (queteAccomplie())
                {
                    Debug.Log("Quete accomplie");
                    curGameState = GameState.etalerLeBeurre;
                    prevGameState = GameState.preparationPate;
                }
                else
                {
                    Debug.Log("Quete echouee");
                    curGameState = GameState.aideDeSkypi;
                    prevGameState = GameState.preparationPate;
                }

            }

            GUI.Box(new Rect(0, Screen.height / 2, Screen.width / 4, Screen.height / 3), contenuDuSaladier());

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

            string aide = "";

            switch (prevGameState)
            {
                case GameState.preparationPate: aide = "Pour mettre des ingrédients dans le saladier, il te suffit de les faire glisser dedans! \nVoici ce qu'il manque :\n";
                    aide += ingredientManquants();
                    break;
                case GameState.etalerLeBeurre: aide = "Étale le beurre en utilisant ton doigt sur la poële";
                    break;
                case GameState.cuissonCrepe: aide = "Étale la pâte à crêpe en penchant la tablette !";
                    break;
                default: aide = "Je ne sais pas quoi te dire";
                    break;
            }

            GUI.Box(new Rect(Screen.width / 4, 0, Screen.width - 2 * (Screen.width / 4), Screen.height / 3), aide);

            if (GUI.Button(new Rect(5 * (Screen.width / 6), 50, 100, 100), "Merci Skypi!"))
            {
                curGameState = prevGameState;
                prevGameState = GameState.aideDeSkypi;
            }

            GUI.EndGroup();
        }
    }

    #endregion

    #region verifEtatDuSaladier

    bool queteAccomplie()
    {
        bool queteAccomplie = true;

        for (int i = 0; i < listeIngQuete.Count; i++)
        {
            if (!listeIngSaladier.Contains(listeIngQuete[i].tag))
            {
                queteAccomplie = false;
            }
        }

        return queteAccomplie;
    }

    string contenuDuSaladier()
    {
        string contenuDuSaladier = "Les ingrédients actuellement dans le saladier sont : \n";

        for (int i = 0; i < listeIngSaladier.Count; i++)
        {
            contenuDuSaladier += "- " + queteCrepe.nomIngredient(listeIngSaladier[i]) + "\n";
        }

        return contenuDuSaladier;
    }

    string ingredientManquants()
    {
        string ingManquant = "";

        for (int i = 0; i < listeIngQuete.Count; i++)
        {
            if (!listeIngSaladier.Contains(listeIngQuete[i].tag))
            {
                ingManquant += "- " + queteCrepe.nomIngredient(listeIngQuete[i].tag) + "\n";
            }
        }
        return ingManquant;
    }

    void ajoutIngredientSaladier()
    {


    }

    #endregion

}
