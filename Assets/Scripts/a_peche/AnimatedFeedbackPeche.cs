using UnityEngine;
using System.Collections;

public class AnimatedFeedbackPeche : MonoBehaviour {

    public GUITexture ecran;
    public Texture[] moviePeche;
    public Texture[] movieDegivrer;

    int framesPerSecond = 2;

    bool playPeche;
    bool playDegivre;

	// Use this for initialization
	void Start () {
        ecran.enabled = false;
        playPeche = false;
        playDegivre = false;
	}

    void Update() {
        if (playPeche) {
            int index = (int)(Time.time * framesPerSecond) % moviePeche.Length;
            ecran.texture = moviePeche[index];
        } else if (playDegivre) {
            int index = (int)(Time.time * framesPerSecond) % movieDegivrer.Length;
            ecran.texture = movieDegivrer[index];
        }
    }
    
    public void playVidPeche(){
        ecran.enabled = true;
        playPeche = true;
    }

    public void playVidDegivrer() {
        ecran.enabled = true;
        playDegivre = true;
    }

    public void ecranInvisible() {
        ecran.enabled = false;
        playPeche = false;
        playDegivre = false;
    }
    
}
