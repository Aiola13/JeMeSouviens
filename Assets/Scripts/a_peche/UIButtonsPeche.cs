using UnityEngine;
using System.Collections;

public class UIButtonsPeche : TouchLogic {

    public GUITexture texValidation;
    public GUITexture texAnnulation;

    int compteurPoissons;

    Vector3[] positionsPoissons;

    void Start() {
        GameManager.NePasAfficherTexture(texValidation);
        GameManager.NePasAfficherTexture(texAnnulation);
        compteurPoissons = 0;
        positionsPoissons = new Vector3[5];
        float x = 20;
        for (int i = 0; i < positionsPoissons.Length; i++) {
            positionsPoissons[i] = new Vector3(x, 90, 91);
            x += 35;
        }
    }

    public override void OnTouchEnded() {
        if (name == "GUI_Validation") {
            GameManager.boutonValidation = true;
            GameManagerPeche.quetePeche.listePanier.Add(GameManagerPeche.peche.poisson.tag);
            GameManagerPeche.peche.poisson.transform.position = positionsPoissons[compteurPoissons];
            compteurPoissons++;
        }

        if (name == "GUI_Annulation") {
            GameManager.boutonAnnulation = true;
            Destroy(GameManagerPeche.peche.poisson);
        }
    }

}
