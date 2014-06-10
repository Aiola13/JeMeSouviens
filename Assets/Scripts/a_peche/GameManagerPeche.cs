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

    public static int compteurPoisson = 0;

    public static GameState curGameState;
    public static GameState prevGameState;

    public static QuetePeche quetePeche;
    public static Peche peche;

    public Texture2D jeanClaude;
    public Texture2D skypi;

    public AudioClip musiqueAmbiance;
    public AudioClip miaulementSkypi;
    public AudioClip canneaPeche;


    public static AudioSource ambiance;
    public static AudioSource miaulement;
    public static AudioSource canneApeche;

	public AudioClip bipMauvais;
	public AudioClip bipBon;
	public static AudioSource errorBip;
	public static AudioSource goodBip;

    public GUITexture validation;
    public GUITexture annulation;

	private bool makeNewSymbol = false;

    GameObject canne;

    #endregion

    // Use this for initialization
	void Start () {

        canne = GameObject.FindGameObjectWithTag("CanneAPeche");

        quetePeche = GetComponent<QuetePeche>();
        peche = GetComponent<Peche>();

        curGameState = GameState.queteJeanClaude;

        ambiance = AddAudio(musiqueAmbiance, true, true, 0.5f);
        miaulement = AddAudio(miaulementSkypi, false, false, 0.8f);
        canneApeche = AddAudio(canneaPeche, false, false, 0.8f);	
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

            modeDegivrage();

            AfficherDialogue(jeanClaude, "La glace a recouvert le trou! Dégivre le en dessinant le symbole!");

			if (makeNewSymbol) {
				Gesture.NewSymbol();
				Gesture.canDraw = true;
				makeNewSymbol = false;
			}
        }

        #endregion


        #region peche

        else if (curGameState == GameState.pecher) {

            //print("Axe X " + Input.acceleration.x + "      Axe Y " + Input.acceleration.y + "         Axe Z " + Input.acceleration.z);
            modePeche();

            if (peche.poissonPeche) {
                canneApeche.Stop();
                AfficherDialogue(jeanClaude, peche.infoPoisson);
                AfficherTexture(validation);
                AfficherTexture(annulation);

                if (boutonValidation) {

                    NePasAfficherTexture(validation);
                    NePasAfficherTexture(annulation);
                    if (compteurPoisson < 5) {

                        AfficherDialogue(jeanClaude, "Le poisson a été ajouté dans ton panier.");

                    } else {
                         if (quetePeche.verifVictoire()) {
                             AfficherDialogue(jeanClaude, "Félicitation c'est un sans faute!");
                         } else {
                             string poissonsCorrects = "Poissons corrects : \n";
                             string poissonsIncorrects = "Poissons incorrects : \n";
                             string feedBackJC = "Ce n'est pas exactement ça. \n";
                             poissonsCorrects += quetePeche.ToStringListePoissons(quetePeche.listeCorrecte);
                             poissonsIncorrects += quetePeche.ToStringListePoissons(quetePeche.listeIncorrecte);
                             feedBackJC += poissonsCorrects;
                             feedBackJC += poissonsIncorrects; 
                             AfficherDialogue(jeanClaude, feedBackJC);
                         }
                        
                    }

                }
                else if (boutonAnnulation) {

                    NePasAfficherTexture(validation);
                    NePasAfficherTexture(annulation);

                    AfficherDialogue(jeanClaude, "Le poisson a été relaché dans le lac.");

                }
            } else if (peche.solGele) {
                ChangeState(GameState.pecher, GameState.degivrerTrou);
                makeNewSymbol = true;
            }

        }

        #endregion peche

        #region aide de skypi

        else if (curGameState == GameState.aideDeSkypi) {
            string aide = "";
            switch (prevGameState) {
                case GameState.degivrerTrou:
                    aide = "Afin de dégivrer le trou, tu dois recopier le symbole qui s'affiche à l'écran mais fais attention à ne pas relever le doigt avant d'avoir terminé.";
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

    public static void ChangeState(GameState prev, GameState current) {
        curGameState = current;
        prevGameState = prev;
    }

    public void modeDegivrage() {
        canne.SetActive(false);
        transform.position = new Vector3(117, 145, 107);
        transform.eulerAngles = new Vector3(80, 0, 0);
    }

    public void modePeche() {
        canne.SetActive(true);
        transform.position = new Vector3(111, 147, 38);
        transform.eulerAngles = new Vector3(30, 0, 0);
    }

}
