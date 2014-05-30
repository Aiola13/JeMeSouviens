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

    public Texture2D jeanClaude;
    public Texture2D skypi;

    public AudioClip musiqueAmbiance;
    public AudioClip miaulementSkypi;

    public static AudioSource ambiance;
    public static AudioSource miaulement;

    #endregion

    // Use this for initialization
	void Start () {

        quetePeche = GetComponent<QuetePeche>();

        curGameState = GameState.queteJeanClaude;

        ambiance = AddAudio(musiqueAmbiance, true, true, 0.5f);
        miaulement = AddAudio(miaulementSkypi, false, false, 0.8f);
        ambiance.Play();
	}

    #region OnGUI
    void OnGUI() {

        print("INGM cur : " + curGameState + "    prev :  " + prevGameState);

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

        }

        #endregion


        #region peche

        else if (curGameState == GameState.pecher) {
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
