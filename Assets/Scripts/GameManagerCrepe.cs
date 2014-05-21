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
	//private UIManager uiManager;

    public Texture2D noemie;
    public Texture2D skypi;
	public Texture2D Tex_dialogue;
	GUIStyle style = new GUIStyle ();
	private int brd = Screen.height/100;
	
	public static bool aValide = false;
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
		//uiManager = GetComponent<UIManager>();

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
		print ("INGM cur : " + GameManagerCrepe.curGameState + "    prev :  " + GameManagerCrepe.prevGameState + "  " + aValide);

        if (!noemie || !skypi) {
            Debug.LogError("Ajouter les textures!");
            return;
        }

		// Affichage de la quete
        if (curGameState == GameState.queteNoemie) {
			AfficherDialogue(noemie, queteCrepe.texteQuete());
        }

		// prepaparation de la pate
        else if (curGameState == GameState.preparationPate) {
			// si on a appuye sur le bouton de validation
			if (aValide) {
				// si la quete est reussie
				if (GameManagerCrepe.queteCrepe.queteAccomplie()) {
					AfficherDialogue(noemie, "Tu as parfaitement réussie la recette!");
				}
				// si la recette a mal été suivie
				else {
					AfficherDialogue(noemie, "Tu t'es trompé dans la recette.");
				}
			}
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
			AfficherDialogue(skypi, aide);
        }
    }
	#endregion GUI




	// affiche une boite de dialogue avec comme texture pnj et comme texte txt,
	public void AfficherDialogue (Texture2D pnj, string txt) { 
		style.fontSize = Screen.height/36;
		style.alignment = TextAnchor.MiddleLeft;
		style.font = (Font)Resources.Load("Roboto-Regular");
		
		GUI.DrawTexture(new Rect(brd, Screen.height*2/3, Screen.width-brd*2, Screen.height/3 -brd), Tex_dialogue, ScaleMode.StretchToFill, true, 0);
		GUI.DrawTexture(new Rect(brd*2, Screen.height*2/3+brd, Screen.width/5, Screen.height/3 -brd*3), pnj, ScaleMode.ScaleToFit, true, 0);
		GUI.Box(new Rect(Screen.width*1/4 , Screen.height*7/10+brd*2, Screen.width-20, Screen.height/5 -10), txt, style);
		
		//style.alignment = TextAnchor.MiddleCenter;
		style.fontSize = Screen.height/28;
		GUI.Box(new Rect(Screen.width*2/3 , Screen.height*2/3+brd*2, Screen.width/10, Screen.height/3 -10), "TOUCHER POUR CONTINUER !", style);
	}
	

}
