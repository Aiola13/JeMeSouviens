using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerCrepe :  GameManager{

    #region attributs
    public enum GameState {
        queteNoemie,
        preparationPate,
        etalerLeBeurre,
        etalerLaPate,
        cuissonCrepe,
        aideDeSkypi
    }

    public static QueteCrepe queteCrepe;

	public GUITexture texValidation;

    public Texture2D noemie;
    public Texture2D skypi;

    public static GameState curGameState;
    public static GameState prevGameState;

    public AudioClip musiqueAmbiance;
    public AudioClip miaulementSkypi;
    public AudioClip dragOK;
    public AudioClip cuissonCrepe;

    public static AudioSource ambiance;
    public static AudioSource miaulement;
    public static AudioSource sonDragOK;
    public static AudioSource cuisson;

    private static int nbIngOptUtil;
    private static int nbCrepesFaites;

    #endregion attributs

    void Start() {
        queteCrepe = GetComponent<QueteCrepe>();

        curGameState = GameState.queteNoemie;

        ambiance = AddAudio(musiqueAmbiance, true, true, 0.5f);
        miaulement = AddAudio(miaulementSkypi, false, false, 0.8f);
        sonDragOK = AddAudio(dragOK, false, false, 0.8f);
        cuisson = AddAudio(cuissonCrepe, true, true, 0.3f);
        ambiance.Play();
        CreerFichierStats();
        ObtenirStatsDernierePartie();
        chrono = new System.Diagnostics.Stopwatch();
    }

    void Update()
    {
        if (curGameState == GameState.aideDeSkypi || curGameState == GameState.queteNoemie || DialogueCrepe.canRestartChrono == false)
        {
            tempsPartie += Time.deltaTime;
            chrono.Stop();
        }
        else
        {
            chrono.Start();
        }
        Debug.Log(chrono.Elapsed);
    }


    #region OnGUI
    void OnGUI() {
        //print("INGM cur : " + curGameState + "    prev :  " + prevGameState + "   boutonValidation: " + GameManager.boutonValidation);

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

			AfficherTexture(texValidation);
			GameObject.Find("validerText").guiText.enabled = true;

			#region debug passage a etaler le beurre
            if (GameManager.nextState) {
				ChangeState(GameState.etalerLeBeurre, GameState.etalerLeBeurre);
				Camera.main.transform.position = Vector3.Lerp(transform.position, new Vector3(-2.7f, 2, -3), 1000.0f);
                GameManager.nextState = false;
			}
			#endregion


            // si on a appuye sur le bouton de validation
            if (GameManager.boutonValidation) {
                // si la quete est reussie
                if (queteCrepe.queteAccomplie()) {
                    AfficherDialogue(noemie, "Tu as parfaitement réussie la recette!");
                }
                // si la recette a mal été suivie
                else {
                    AfficherDialogue(noemie, "Tu t'es trompé dans la recette.");
                }
            }
			else {				
				AfficherIngredients();
			}
        }
		#endregion preparation de la pate


		#region etalage du beurre
        // etalage du beurre
        else if (curGameState == GameState.etalerLeBeurre) {
			#region debug passage a etalage pate
            if (GameManager.nextState) {
                ChangeState(GameState.etalerLeBeurre, GameState.etalerLaPate);
                GameManager.nextState = false;
			}
			#endregion

			DesactiverDrag();
        }
		#endregion etalage du beurre



        #region etalage de la pate
            // etalage de la pate
        else if (curGameState == GameState.etalerLaPate) {
            #region debug passage a cuisson
            if (GameManager.nextState) {
                ChangeState(GameState.etalerLaPate, GameState.cuissonCrepe);
                GameManager.nextState = false;
            }
            #endregion

            DesactiverDrag();
        }
        #endregion etalage du beurre


		#region cuisson de la crepe
        // cuisson de la crepe
        else if (curGameState == GameState.cuissonCrepe) {
            if (RetournerCrepe.counter >= 5.0f)
                AfficherDialogue(noemie, "Hum quelle bonne odeur!\n Tu peux maintenant retourner ta crêpe");
            if (RetournerCrepe.isCook && RetournerCrepe.counter >= 5.0f)
                AfficherDialogue(noemie, "Parfait la crêpe est finie allons la manger!");
        }
		#endregion cuisson de la crepe


		#region aide de skipy
        // affichage de l'aide de skipy
        else if (curGameState == GameState.aideDeSkypi) {
            string aide = "";
            switch (prevGameState) {
                case GameState.preparationPate:
                    if (!queteCrepe.queteAccomplie()) {
                        aide = "Pour mettre des ingrédients dans le saladier, il te suffit de les faire glisser dedans! \nVoici ce qu'il manque :\n";
                        aide += queteCrepe.ingredientManquants();
                    }
                    else {
					    aide = "Tu as mis tous les ingrédients nécessaires!\n clique sur la fleche verte pour passer a l'étape suivante!";
                    }
                    break;
                case GameState.etalerLeBeurre:
                    aide = "Étale le beurre en utilisant ton doigt sur la poele";
                    break;
                case GameState.cuissonCrepe:
                    aide = "Étale la pate a crepe en penchant la tablette !";
                    break;
                default:
                    aide = "Je ne sais pas quoi te dire";
                    break;
            }
            nbAppelsAide++;
            Debug.Log("Appel à l'aide");
            AfficherDialogue(skypi, aide);
        }
		#endregion aide de skipy
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
}