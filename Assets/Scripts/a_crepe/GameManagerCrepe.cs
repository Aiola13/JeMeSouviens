using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerCrepe :  GameManager{

    #region attributs
    public enum GameState {
        queteNoemie,
        preparationPate,
        etalerLaPate,
        cuissonCrepe,
        aideDeSkypi,
		score
    }

    public enum EtatCuisson {
        cuisson,
        retourner,
        fini
    }

    public EtatCuisson curEtatCuisson;

    public static QueteCrepe queteCrepe;
	public static AnimatedFeedbackCrepe videos;

    public Texture2D noemie;
    public Texture2D skypi;

	public GUITexture rejouer;
	public GUIText rejouerText;

	public static GameState curGameState;
    public static GameState prevGameState;

    public AudioClip musiqueAmbiance;
    public AudioClip miaulementSkypi;
    public AudioClip dragOK;
	public AudioClip erreur;
    public AudioClip cuissonCrepe;

    public static AudioSource ambiance;
    public static AudioSource miaulement;
    public static AudioSource sonDragOK;
	public static AudioSource sonErreur;
    public static AudioSource cuisson;

    private static int nbIngOptUtil;
    private static int nbCrepesFaites;

	public static bool bQueteAccomplie;

    #endregion attributs

    void Start() {

		initGameManager();
		
		initGameManagerCrepe();

        GetStatsSpecifique(2);
    }

    void Update()
    {
        if (curGameState == GameState.score || curGameState == GameState.aideDeSkypi || curGameState == GameState.queteNoemie || DialogueCrepe.canRestartChrono == false)
        {
            chrono.Stop();
        }
        else
        {
            chrono.Start();
        }
    }


    #region OnGUI
    void OnGUI() {
        print("INGM cur : " + curGameState + "    prev :  " + prevGameState);

        if (!noemie || !skypi) {
            Debug.LogError("Ajouter les textures!");
            return;
        }


		#region quete
        // Affichage de la quete
        if (curGameState == GameState.queteNoemie) {
			DesactiverDrag();
            AfficherDialogue(noemie, queteCrepe.texteQuete());
        }
		#endregion quete


		#region preparation de la pate
        // preparation de la pate
        else if (curGameState == GameState.preparationPate) {

			videos.playVidDrag();
			
			if (bQueteAccomplie) {
				
				videos.ecranInvisible();
				
				// si la quete est reussie
				if (nbErreurs == 0) {
					AfficherDialogue(noemie, "Tu as parfaitement réussi la recette!");
				}
				// si la recette a mal été suivie
				else {
					AfficherDialogue(noemie, "Tu as fait quelques erreurs mais ce n'est pas grave, passons à la suite!");
				}
			}
			else {				
				AfficherIngredients();
			}
        }
		#endregion preparation de la pate



        #region etalage de la pate
            // etalage de la pate
        else if (curGameState == GameState.etalerLaPate) {

            videos.playVidEtaler();

            if (EtalerPate.isEtaler) {
                AfficherDialogue(noemie, "Bien joué, cette crêpe est bien ronde!", true);
                videos.ecranInvisible();
            }

            DesactiverDrag();
        }
        #endregion

        #region score
            // score
        else if (curGameState == GameState.score) {
            chrono.Stop();
            tempsPartie = chrono.Elapsed.Minutes * 60 + chrono.Elapsed.Seconds;
            if (hasWritenStats == false)
            {
                EcrireStatsCrepe();
                hasWritenStats = true;
            }
            AfficherScore(calculerEtoilesCrepe());
			AfficherTexture(rejouer, rejouerText);
        }
        #endregion

		#region cuisson de la crepe
        // cuisson de la crepe
        else if (curGameState == GameState.cuissonCrepe) {
            if (curEtatCuisson == EtatCuisson.cuisson) {
                AfficherAide("La crêpe est en train de cuire. Patience ...");
                if (RetournerCrepe.counter >= 5.0f) {
                    curEtatCuisson = EtatCuisson.retourner;
                }
            } else if (curEtatCuisson == EtatCuisson.retourner) {
                videos.playVidRetourner();
                AfficherAide("Hum quelle bonne odeur!\n Tu peux maintenant retourner ta crêpe");
                if (RetournerCrepe.isCook && RetournerCrepe.counter >= 3.0f) {
                    curEtatCuisson = EtatCuisson.fini;
                }
            }
            else if (curEtatCuisson == EtatCuisson.fini) {
                videos.ecranInvisible();
                AfficherDialogue(noemie, "Parfait la crêpe est finie, allons la manger!", true);
            }

        }
		#endregion cuisson de la crepe


		#region aide de skypi
        // affichage de l'aide de skypi
		else if (curGameState == GameState.aideDeSkypi) {
			string aide = "";
			switch (prevGameState) {
			case GameState.preparationPate:
				if (!queteCrepe.queteAccomplie()) {
					aide = "Pour mettre des ingrédients dans le saladier, il te suffit de les faire glisser dedans! \nVoici ce qu'il manque :\n";
					aide += queteCrepe.ingredientManquants();
				}
				else {
					aide = "Tu as mis tous les ingrédients nécessaires!\n Clique sur la flèche verte pour passer à l'étape suivante!";
				}
				break;
			case GameState.etalerLaPate:
				aide = "Étale la pâte en inclinant la tablette.";
				break;
			case GameState.cuissonCrepe:
				aide = "Retourne la crêpe en ramenant la tablette vers toi.";
				break;
			default:
				aide = "Je ne sais pas quoi te dire";
				break;
			}
			AfficherDialogue(skypi, aide);
		}
		#endregion aide de skypi
    }
    #endregion OnGUI



    // Parameters: prev State, curr State
    void ChangeState(GameState prev, GameState current) {
        curGameState = current;
        prevGameState = prev;
    }
     
	// affiche une boite avec les ingredients contenu dans le saladier
	void AfficherIngredients() {
		TextNormal.fontSize = Screen.height / 36;
		TextNormal.alignment = TextAnchor.MiddleCenter;
		TextNormal.font = (Font)Resources.Load("Roboto-Regular");

		float brd = Screen.height / 100;

		Rect box = new Rect(Screen.width * 3/4, Screen.height - Screen.height/3, Screen.width/4-brd, Screen.height / 3 - brd);
		GUI.DrawTexture(box, Tex_dialogue, ScaleMode.ScaleAndCrop, true, 0);
		GUI.Box(box, queteCrepe.contenuDuSaladier(), TextNormal);
    }

	void initGameManagerCrepe() {
		
		queteCrepe = GetComponent<QueteCrepe>();
		videos = GetComponent<AnimatedFeedbackCrepe>();
		
		curGameState = GameState.queteNoemie;

        curEtatCuisson = EtatCuisson.cuisson;
		
		ambiance = AddAudio(musiqueAmbiance, true, true, 0.5f);
		miaulement = AddAudio(miaulementSkypi, false, false, 0.8f);
		sonDragOK = AddAudio(dragOK, false, false, 0.8f);
		sonErreur = AddAudio(erreur, false, false, 0.8f);
		cuisson = AddAudio(cuissonCrepe, true, true, 0.3f);
		ambiance.Play();
		CreerFichierStats();
		ObtenirStatsDernierePartie();
		chrono = new System.Diagnostics.Stopwatch();
		bQueteAccomplie = false;
        RetournerCrepe.isCook = false;
        RetournerCrepe.counter = 0.0f;
        EtalerPate.InitEtalerPate();
	}

    void EcrireStatsCrepe() {
        System.IO.FileStream fs = System.IO.File.Open(cheminFichierStats, System.IO.FileMode.Append);
        System.Byte[] stats = new System.Text.UTF8Encoding(true).GetBytes(idPartie + "," + tempsPartie + "," + nbErreurs + "," + nbAppelsAide + "," + 0 + "\n");
        fs.Write(stats, 0, stats.Length);
        fs.Close();
    }

    int calculerEtoilesCrepe() {
        if (nbAppelsAide == 0 && nbErreurs == 0 && tempsPartie <= 30.0)
            return 3;
        else if (nbAppelsAide <= 2 || nbErreurs <= 1 || tempsPartie <= 45.0)
            return 2;
        else
            return 1;
    }
}