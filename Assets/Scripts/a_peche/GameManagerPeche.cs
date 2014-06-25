using UnityEngine;
using System.Collections;

public class GameManagerPeche : GameManager {

    #region attributs

    public enum GameState {
        queteJeanClaude,
        degivrerTrou,
        pecher,
        aideDeSkypi,
        finDePartie,
        score
    }

    public static int compteurPoisson;

    public static GameState curGameState;
    public static GameState prevGameState;

    public static QuetePeche quetePeche;
    public static Peche peche;
    public static AnimatedFeedbackPeche videos;

    public GUITexture texValidation;
    public GUITexture texAnnulation;
    public GUITexture texReplay;

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

	private bool makeNewSymbol;

    GameObject canne;

    #endregion

    // Use this for initialization
	void Start () {

        initGameManager();
        initGameManagerPeche();
        chrono = new System.Diagnostics.Stopwatch();
        GameManager.chrono.Start();
        
	}

    void Update() {
        if (GameManagerPeche.curGameState == GameState.aideDeSkypi || GameManagerPeche.curGameState == GameState.queteJeanClaude ||
            GameManagerPeche.curGameState == GameState.finDePartie || GameManagerPeche.curGameState == GameState.score) {
            GameManager.chrono.Stop();
            Debug.Log("Je stoppe le chrono " + GameManagerPeche.curGameState);
        } else
            GameManager.chrono.Start();
    }

    #region OnGUI
    void OnGUI() {

        //print("INGM cur : " + curGameState + "    prev :  " + prevGameState + "          bouton validation = " + boutonValidation + "        bouton annulation = " + boutonAnnulation);

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

            AfficherAide("Dégivre le trou en dessinant le symbole!");

            videos.playVidDegivrer();

			if (makeNewSymbol) {
				Gesture.NewSymbol();
				Gesture.canDraw = true;
				makeNewSymbol = false;
			}
        }

        #endregion


        #region peche

        else if (curGameState == GameState.pecher) {

            modePeche();
            if (peche.aMordu()) {
                AfficherAide("Releve la tablette maintenant!");
            }
            else {
                AfficherAide("Patience un poisson va bientôt mordre à l'hameçon.");
            }
            
            videos.ecranInvisible();

            if (peche.poissonPeche) {
                canneApeche.Stop();

                AfficherDialogue(jeanClaude, peche.infoPoisson, false);
                GameManager.AfficherTexture(texValidation, GameObject.Find("valider_text").guiText);
                GameManager.AfficherTexture(texAnnulation, GameObject.Find("annuler_text").guiText);

                if (boutonValidation) {

                    GameManager.NePasAfficherTexture(texValidation, GameObject.Find("valider_text").guiText);
                    GameManager.NePasAfficherTexture(texAnnulation, GameObject.Find("annuler_text").guiText);

                    if (compteurPoisson < 5) {

                        AfficherDialogue(jeanClaude, "Le poisson a été ajouté dans ton panier.");

                    } else {

                        ChangeState(GameState.pecher, GameState.finDePartie);
                    }

                }
                else if (boutonAnnulation) {

                    GameManager.NePasAfficherTexture(texValidation, GameObject.Find("valider_text").guiText);
                    GameManager.NePasAfficherTexture(texAnnulation, GameObject.Find("annuler_text").guiText);

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

        #region fin de partie

        else if (curGameState == GameState.finDePartie) {

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
        #endregion

        #region score

        else if (curGameState == GameState.score) {

            GameManager.AfficherTexture(texReplay, GameObject.Find("replay_text").guiText);
            if (hasWritenStats == false)
            {
                EcrireStatsPeche();
                hasWritenStats = true;
            }
            AfficherScore(calculerEtoilesPeche());
            
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

    void initGameManagerPeche() {

        compteurPoisson = 0;
        makeNewSymbol = false;
        canne = GameObject.FindGameObjectWithTag("CanneAPeche");

        quetePeche = GetComponent<QuetePeche>();
        peche = GetComponent<Peche>();
        videos = GetComponent<AnimatedFeedbackPeche>();

        curGameState = GameState.queteJeanClaude;

        ambiance = AddAudio(musiqueAmbiance, true, true, 0.5f);
        miaulement = AddAudio(miaulementSkypi, false, false, 0.8f);
        canneApeche = AddAudio(canneaPeche, false, false, 0.8f);
        errorBip = AddAudio(bipMauvais, false, false, 1.0f);
        goodBip = AddAudio(bipBon, false, false, 1.0f);

        ambiance.Play();
    }

    int calculerEtoilesPeche() {
        if (nbAppelsAide == 0 && nbErreurs == 0 && tempsPartie <= 60.0)
            return 3;
        else if (nbAppelsAide <= 2 || nbErreurs <= 1 || tempsPartie <= 90.0)
            return 2;
        else
            return 1;
    }

    void EcrireStatsPeche()
    {
        System.IO.FileStream fs = System.IO.File.Open(cheminFichierStats, System.IO.FileMode.Append);
        System.Byte[] stats = new System.Text.UTF8Encoding(true).GetBytes(idPartie + "," + tempsPartie + "," + nbErreurs + "," + nbAppelsAide + "\n");
        fs.Write(stats, 0, stats.Length);
        fs.Close();
    }

}
