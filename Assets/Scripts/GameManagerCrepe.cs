using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerCrepe : MonoBehaviour {

    #region attributs
    public enum GameState {
        queteNoemie,
        preparationPate,
        etalerLeBeurre,
        cuissonCrepe,
        aideDeSkypi
    }

    public static QueteCrepe queteCrepe;
	private UIManager uiManager;

    public Texture2D noemie;
    public Texture2D skypi;

	public static GameState curGameState;
	public static GameState prevGameState;

	List<GameObject> listeIngQuete;
	List<string> listeIngSaladier;

    public AudioClip musiqueAmbiance;
	#endregion attributs

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
        queteCrepe = GetComponent<QueteCrepe>();
		uiManager = GetComponent<UIManager>();

		curGameState = GameState.queteNoemie;
        listeIngQuete = queteCrepe.liste_quete;
		listeIngSaladier = queteCrepe.liste_saladier;

        //Musique d'ambiance ici
        AudioSource sourceAudio = gameObject.AddComponent<AudioSource>();
        audio.clip = musiqueAmbiance;
        audio.loop = true;
        audio.Play();
	}

    #region GUI
    void OnGUI() {
		print ("INGM cur : " + GameManagerCrepe.curGameState + "    prev :  " + GameManagerCrepe.prevGameState);

        if (!noemie || !skypi) {
            Debug.LogError("Ajouter les textures!");
            return;
        }

		// Affichage de la quete
        if (curGameState == GameState.queteNoemie) {
			uiManager.AfficherDialogue(noemie, queteCrepe.texteQuete());
        }


        else if (curGameState == GameState.preparationPate) {

			//if (!queteCrepe.queteAccomplie())
			//uiManager.AfficherDialogue(skypi, "Tu t'es trompé dans la recette.");



			/*
            GUI.BeginGroup(new Rect(Screen.width - (Screen.width/4), 0, Screen.width/4, Screen.height));

			// bouton pour valider la recette et finir l'etape 1
            if (GUI.Button(new Rect(0, 0, 150, 100), "La pâte est prête!")) {
                if (queteAccomplie()) {
                    Debug.Log("Quete accomplie");
					//changeState(GameState.preparationPate, GameState.etalerLeBeurre);
                }
                else {
                    Debug.Log("Quete echouee");
					//changeState(GameState.preparationPate, GameState.aideDeSkypi);
                }
            }

            GUI.Box(new Rect(0, Screen.height / 2, Screen.width / 4, Screen.height / 3), contenuDuSaladier());

            GUI.EndGroup();
            */
        }

        else if (curGameState == GameState.etalerLeBeurre) {

        }

        else if (curGameState == GameState.cuissonCrepe) {

        }

		// affichage de l'aide de skipy
        else if (curGameState == GameState.aideDeSkypi) {

			string aide = "";
			
			switch (prevGameState) {
				case GameState.preparationPate:
					aide = "Pour mettre des ingrédients dans le saladier, il te suffit de les faire glisser dedans! \nVoici ce qu'il manque :\n";
					//aide += ingredientManquants();
					break;
				case GameState.etalerLeBeurre:
					aide = "Étale le beurre en utilisant ton doigt sur la poële";
					break;
				case GameState.cuissonCrepe:
					aide = "Étale la pâte à crêpe en penchant la tablette !";
					break;
				default:
					aide = "Je ne sais pas quoi te dire";
					break;
			}

			uiManager.AfficherDialogue(skypi, aide);
        }
    }
	#endregion GUI




    #region verifEtatDuSaladier


    string contenuDuSaladier() {
        string contenuDuSaladier = "Les ingrédients actuellement dans le saladier sont : \n";
		
        for (int i = 0; i < listeIngSaladier.Count; i++) {
            contenuDuSaladier += "- " + queteCrepe.nomIngredient(listeIngSaladier[i]) + "\n";
        }

        return contenuDuSaladier;
    }

    string ingredientManquants() {
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
