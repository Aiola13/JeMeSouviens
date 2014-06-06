using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Texture2D Tex_dialogue;
    protected GUIStyle style = new GUIStyle();
    protected int brd = Screen.height / 100;

    public static bool nextState = false;
    public static bool boutonValidation = false;
    public static bool boutonAnnulation = false;


    /////////////////////////////////////////////////////////////////////
 

    public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float volume) {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = volume;
        return newAudio;
    }

    // active et affiche la texture t
    public static void AfficherTexture(GUITexture t) {
        t.guiTexture.enabled = true;
        t.gameObject.SetActive(true);
    }

    // désactive et enleve l'affichage de la texture t
    public static void NePasAfficherTexture(GUITexture t) {
        t.guiTexture.enabled = false;
        t.gameObject.SetActive(false);
    }

    public static void ActiverDrag() {
        Camera.main.GetComponent<TouchLogicDrag>().enabled = true;
    }
    public static void DesactiverDrag() {
        Camera.main.GetComponent<TouchLogicDrag>().enabled = false;
    }

    // affiche une boite de dialogue avec comme texture pnj et comme texte txt,
    public void AfficherDialogue(Texture2D pnj, string txt) {
        style.fontSize = Screen.height / 36;
        style.alignment = TextAnchor.MiddleLeft;
        style.font = (Font)Resources.Load("Roboto-Regular");

        GUI.DrawTexture(new Rect(brd, Screen.height * 2 / 3, Screen.width - brd * 2, Screen.height / 3 - brd), Tex_dialogue, ScaleMode.StretchToFill, true, 0);
        GUI.DrawTexture(new Rect(brd * 2, Screen.height * 2 / 3 + brd, Screen.width / 5, Screen.height / 3 - brd * 3), pnj, ScaleMode.ScaleToFit, true, 0);
        GUI.Box(new Rect(Screen.width * 1 / 4, Screen.height * 7 / 10 + brd * 2, Screen.width - 20, Screen.height / 5 - 10), txt, style);

        //style.alignment = TextAnchor.MiddleCenter;
        style.fontSize = Screen.height / 28;
        GUI.Box(new Rect(Screen.width * 2 / 3, Screen.height * 2 / 3 + brd * 2, Screen.width / 10, Screen.height / 3 - 10), "TOUCHER POUR CONTINUER !", style);
    }

	// affiche une aide en bas de l'écran sur l'action courrante a faire
	public void AfficherAide(string txt) {
		style.fontSize = Screen.height / 24;
		style.alignment = TextAnchor.MiddleLeft;
		style.font = (Font)Resources.Load("Roboto-Regular");
		
		GUI.DrawTexture(new Rect(brd * 30, Screen.height * 90 / 100, Screen.width - brd * 60, Screen.height * 10 / 100 - brd), Tex_dialogue, ScaleMode.StretchToFill, true, 0);
		GUI.Box(new Rect(brd * 40, Screen.height * 90 / 100, Screen.width - brd * 70, Screen.height * 10 / 100 - brd), txt, style);
	}
}
