using UnityEngine;
using System.Collections;

public class DialoguePeche : TouchLogic {

    public GUITexture texValidation;
    public GUITexture texAnnulation;

    protected Ray ray;
    protected RaycastHit hit;

    public override void OnTouchEndedAnywhere() {
        

        // State Quete de JC

        if (GameManagerPeche.curGameState == GameManagerPeche.GameState.queteJeanClaude) {
            ChangeState(GameManagerPeche.GameState.queteJeanClaude, GameManagerPeche.GameState.pecher);
            //AfficherTexture(texValidation);
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

        // State degivrage

        else if (GameManagerPeche.curGameState == GameManagerPeche.GameState.degivrerTrou) {


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

   // active et affiche la texture t
   public void AfficherTexture(GUITexture t) {
       t.guiTexture.enabled = true;
       t.gameObject.SetActive(true);
   }

   // désactive et enleve l'affichage de la texture t
   public void NePasAfficherTexture(GUITexture t) {
       t.guiTexture.enabled = false;
       t.gameObject.SetActive(false);
   }

}
