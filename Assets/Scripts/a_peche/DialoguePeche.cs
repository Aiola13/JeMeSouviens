using UnityEngine;
using System.Collections;

public class DialoguePeche : TouchLogic {

    private Ray ray;
    private RaycastHit hit;

    public override void OnTouchEndedAnywhere() {
        

        // State Quete de JC

        if (GameManagerPeche.curGameState == GameManagerPeche.GameState.queteJeanClaude) {
            ChangeState(GameManagerPeche.GameState.queteJeanClaude, GameManagerPeche.GameState.pecher);
        }

        // State Aide de Skypi

        else if (GameManagerPeche.curGameState == GameManagerPeche.GameState.aideDeSkypi) {
            ChangeState(GameManagerPeche.GameState.aideDeSkypi, GameManagerPeche.prevGameState);
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
        
        else if (GameManagerPeche.curGameState == GameManagerPeche.GameState.finDePartie) {
            Application.LoadLevel("a_menu");
        }

        


        // Si on touche Skypi
        ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
        if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.tag == "Skypi")) {
            switch (GameManagerPeche.curGameState) {
                case GameManagerPeche.GameState.degivrerTrou:
                    ChangeState(GameManagerPeche.GameState.degivrerTrou, GameManagerPeche.GameState.aideDeSkypi);
                    break;
                case GameManagerPeche.GameState.pecher:
                    ChangeState(GameManagerPeche.GameState.pecher, GameManagerPeche.GameState.aideDeSkypi);
                    break;
            }
            GameManagerPeche.miaulement.Play();
        }

    }

    // Parameters: prev State, curr State
   void ChangeState(GameManagerPeche.GameState prev, GameManagerPeche.GameState current) {
        GameManagerPeche.curGameState = current;
        GameManagerPeche.prevGameState = prev;
    }

}
