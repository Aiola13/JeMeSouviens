using UnityEngine;
using System.Collections;

public class UIButtonsPeche : TouchLogic {

    public GUITexture texValidation;
    public GUITexture texAnnulation;

    void Start() {
        GameManager.NePasAfficherTexture(texValidation);
        GameManager.NePasAfficherTexture(texAnnulation);
    }

    /*
    void Update() {
        
        if (GameManagerPeche.curGameState == GameManagerPeche.GameState.pecher && GameManagerPeche.peche.poissonPeche) {
            GameManager.AfficherTexture(texValidation);
            GameManager.AfficherTexture(texAnnulation);

        } else {
            GameManager.NePasAfficherTexture(texValidation);
            GameManager.NePasAfficherTexture(texAnnulation);
        }
    }
    */
    public override void OnTouchEnded() {
        if (name == "GUI_Validation") {
            GameManager.boutonValidation = true;
        }

        if (name == "GUI_Annulation") {
            GameManager.boutonAnnulation = true;
        }
    }

}
