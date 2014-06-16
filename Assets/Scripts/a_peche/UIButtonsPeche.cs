using UnityEngine;
using System.Collections;

public class UIButtonsPeche : TouchLogic {

    public GUITexture texValidation;
    public GUITexture texAnnulation;
    public GUITexture texMenu;
    public GUITexture texReplay;


    Vector3[] positionsPoissons;

    void Start() {

        GameManager.NePasAfficherTexture(texValidation, GameObject.Find("valider_text").guiText);
        GameManager.NePasAfficherTexture(texAnnulation, GameObject.Find("annuler_text").guiText);
        GameManager.NePasAfficherTexture(texReplay, GameObject.Find("replay_text").guiText);
        positionsPoissons = new Vector3[5];
        positionsPoissons[0] = new Vector3(57, 85, 104);
        positionsPoissons[1] = new Vector3(64, 85, 104);
        positionsPoissons[2] = new Vector3(57, 85, 98);
        positionsPoissons[3] = new Vector3(64, 85, 98);
        positionsPoissons[4] = new Vector3(60, 85, 95);

    }

    public override void OnTouchEnded() {

        if (name == "texAnnulation") {
            GameManager.boutonAnnulation = true;
            Destroy(GameManagerPeche.peche.poisson);
            if (GameManagerPeche.quetePeche.listeQuete.Contains(GameManagerPeche.peche.poisson.tag)){
                GameManagerPeche.errorBip.Play();
                GameManagerPeche.nbErreurs++;
            } else {
                GameManagerPeche.goodBip.Play();
            }
        } else if (name == "texValidation") {
            GameManager.boutonValidation = true;
            if (GameManagerPeche.quetePeche.listeQuete.Contains(GameManagerPeche.peche.poisson.tag)) {
                GameManagerPeche.goodBip.Play();
                GameManagerPeche.quetePeche.listeCorrecte.Add(GameManagerPeche.peche.poisson.tag);
                GameManagerPeche.quetePeche.listeQuete.Remove(GameManagerPeche.peche.poisson.tag);
            } else {
                GameManagerPeche.errorBip.Play();
                GameManagerPeche.nbErreurs++;
                GameManagerPeche.quetePeche.listeIncorrecte.Add(GameManagerPeche.peche.poisson.tag);
            }
            GameManagerPeche.peche.poisson.transform.position = positionsPoissons[GameManagerPeche.compteurPoisson];
            GameManagerPeche.peche.poisson.transform.eulerAngles = new Vector3(90, 90, 0);
            GameManagerPeche.compteurPoisson++;
        } else if (name == "texMenu") {
            Application.LoadLevel("menu");
        } else if (name == "texReplay") {
            Application.LoadLevel("a_peche");
        }

    }

}
