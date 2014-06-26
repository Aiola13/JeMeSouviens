using UnityEngine;
using System.Collections;

public class DialoguePeche : TouchLogic {

    private Ray ray;
    private RaycastHit hit;
    private bool canAskSkypi = true;

    public override void OnTouchEndedAnywhere() {
        

        // State Quete de JC

        if (GameManagerPeche.curGameState == GameManagerPeche.GameState.queteJeanClaude) {
            ChangeState(GameManagerPeche.GameState.queteJeanClaude, GameManagerPeche.GameState.pecher);
        }

        // State Aide de Skypi

        else if (GameManagerPeche.curGameState == GameManagerPeche.GameState.aideDeSkypi) {
            ChangeState(GameManagerPeche.GameState.aideDeSkypi, GameManagerPeche.prevGameState);
            canAskSkypi = true;
        }

        // State Peche

        else if (GameManagerPeche.curGameState == GameManagerPeche.GameState.pecher) {
            if (GameManagerPeche.peche.poissonPeche && GameManagerPeche.boutonValidation) {
                GameManagerPeche.peche.poissonPeche = false;
                GameManagerPeche.boutonValidation = false;
            } else if (GameManagerPeche.peche.poissonPeche && GameManagerPeche.boutonAnnulation) {
                GameManagerPeche.peche.poissonPeche = false;
                GameManagerPeche.boutonAnnulation = false;
            }

        } 
        
        // State fin de partie

        else if (GameManagerPeche.curGameState == GameManagerPeche.GameState.finDePartie) {
            ChangeState(GameManagerPeche.GameState.finDePartie, GameManagerPeche.GameState.score);
        }


        // Si on touche Skypi

        if (GameManagerPeche.curGameState == GameManagerPeche.GameState.degivrerTrou ||
        GameManagerPeche.curGameState == GameManagerPeche.GameState.pecher)
        {
            Debug.Log("Etat actuel : " + GameManagerPeche.curGameState);
            ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.tag == "Skypi")) {
                canAskSkypi = false;
                switch (GameManagerPeche.curGameState) {
                    case GameManagerPeche.GameState.degivrerTrou:
                        ChangeState(GameManagerPeche.GameState.degivrerTrou, GameManagerPeche.GameState.aideDeSkypi);
                        break;
                    case GameManagerPeche.GameState.pecher:
                        ChangeState(GameManagerPeche.GameState.pecher, GameManagerPeche.GameState.aideDeSkypi);
                        break;
                }
                GameManagerPeche.miaulement.Play();
                GameManagerPeche.nbAppelsAide++;
            }

        }

    }

    // Parameters: prev State, curr State
   void ChangeState(GameManagerPeche.GameState prev, GameManagerPeche.GameState current) {
        GameManagerPeche.curGameState = current;
        GameManagerPeche.prevGameState = prev;
    }

}
