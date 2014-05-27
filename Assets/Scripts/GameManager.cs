using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    GUIStyle style = new GUIStyle();
    private int brd = Screen.height / 100;
    public Texture2D Tex_dialogue;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    protected AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float volume) {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = volume;
        return newAudio;
    }

    protected void ActiverDrag() {
        Camera.main.GetComponent<TouchLogicDrag>().enabled = true;
    }
    protected void DesactiverDrag() {
        Camera.main.GetComponent<TouchLogicDrag>().enabled = false;
    }

    // Parameters: prev State, curr State
    protected void ChangeState(GameManagerCrepe.GameState prev, GameManagerCrepe.GameState current) {
        GameManagerCrepe.curGameState = current;
        GameManagerCrepe.prevGameState = prev;
    }

    // affiche une boite de dialogue avec comme texture pnj et comme texte txt,
    protected void AfficherDialogue(Texture2D pnj, string txt) {
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

}
