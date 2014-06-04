using UnityEngine;
using System.Collections;

public class GameManagerPeche : GameManager {

    #region attributs

    public enum GameState {
        queteJeanClaude,
        degivrerTrou,
        pecher,
        aideDeSkypi
    }

    public static GameState curGameState;
    public static GameState prevGameState;

    public static QuetePeche quetePeche;
    public static Peche peche;

    public Texture2D jeanClaude;
    public Texture2D skypi;

    public AudioClip musiqueAmbiance;
    public AudioClip miaulementSkypi;
    public static AudioSource ambiance;
    public static AudioSource miaulement;

	public AudioClip bipMauvais;
	public AudioClip bipBon;
	public static AudioSource errorBip;
	public static AudioSource goodBip;

    public GUITexture validation;
    public GUITexture annulation;

	private bool makeNewSymbol = false;

    #endregion

    // Use this for initialization
	void Start () {



        quetePeche = GetComponent<QuetePeche>();
        peche = GetComponent<Peche>();

        curGameState = GameState.queteJeanClaude;

        ambiance = AddAudio(musiqueAmbiance, true, true, 0.5f);
        miaulement = AddAudio(miaulementSkypi, false, false, 0.8f);
		
		errorBip = AddAudio(bipMauvais, false, false, 1.0f);
		goodBip = AddAudio(bipBon, false, false, 1.0f);

        ambiance.Play();
	}

    #region OnGUI
    void OnGUI() {

        print("INGM cur : " + curGameState + "    prev :  " + prevGameState + "          bouton validation = " + boutonValidation + "        bouton annulation = " + boutonAnnulation + "   poisson peche =" + peche.poissonPeche);

        if (!jeanClaude || !skypi) {
            Debug.LogError("Ajouter les textures!");
            return;
        }


        #region quete

        if (curGameState == GameState.queteJeanClaude) {
            AfficherDialogue(jeanClaude, quetePeche.texteQuete());
        }
        #endregion quete


        #region degivrage du trou

        else if (curGameState == GameState.degivrerTrou) {
			if (makeNewSymbol) {
				Gesture.NewSymbol();
				Gesture.canDraw = true;
				makeNewSymbol = false;
			}
        }

        #endregion


        #region peche

        else if (curGameState == GameState.pecher) {

            if (peche.poissonPeche) {

                AfficherDialogue(jeanClaude, peche.infoPoisson);
                AfficherTexture(validation);
                AfficherTexture(annulation);

                if (boutonValidation) {
                    quetePeche.listePanier.Add(peche.poisson);
                    AfficherDialogue(jeanClaude, "Le poisson a été ajouté dans ton panier.");
                    boutonValidation = false;
                    peche.poissonPeche = false;

                    if (quetePeche.listePanier.Count >= 5) {
                        string poissonsCorrects;
                        string poissonsIncorrects;
                        if (quetePeche.verifVictoire(out poissonsCorrects, out poissonsIncorrects)) {
                            AfficherDialogue(jeanClaude, "Félicitation c'est un sans faute!");
                        } else {
                            AfficherDialogue(jeanClaude, "Ce n'est pas exactement ça");
                        }
                    }


					// on change d'etat et on veut faire la reconnaissance de symbole
					ChangeState(GameState.pecher, GameState.degivrerTrou);
					makeNewSymbol = true;

                }
                else if (boutonAnnulation) {
                    AfficherDialogue(jeanClaude, "Le poisson a été relaché dans le lac.");
                    boutonAnnulation = false;
                    peche.poissonPeche = false;
                }
            }

        }

        #endregion peche

        #region aide de skipy

        else if (curGameState == GameState.aideDeSkypi) {
            string aide = "";
            switch (prevGameState) {
                case GameState.degivrerTrou:
                    aide = "Afin de dégivrer le trou, tu dois recopier le symbole qui s'affiche à l'écran.";
                    break;
                case GameState.pecher:
                    aide = "Lorsqu'un poisson mord à l'hameçon, relève la tablette d'un coup sec pour le sortir de l'eau. \n";
                    aide += quetePeche.poissonsManquants();
                    break;
                default:
                    aide = "Je ne sais pas quoi te dire";
                    break;
            }
            AfficherDialogue(skypi, aide);
        }
        #endregion
    }
    #endregion OnGUI

    public void ChangeState(GameState prev, GameState current) {
        curGameState = current;
        prevGameState = prev;
    }

}
