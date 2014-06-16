using UnityEngine;
using System.Collections;

public class VideoFeedback : MonoBehaviour {

    public GUITexture ecran;
    //public MovieTexture moviePeche;
    //public MovieTexture movieDegivrer;

	// Use this for initialization
	void Start () {

        ecran.enabled = false;
	}
	
    public void playVidPeche(){
        ecran.enabled = true;
        //ecran.texture = moviePeche as MovieTexture;
        //moviePeche.Play();
        //moviePeche.loop = true;
    }

    public void playVidDegivrer() {
        ecran.enabled = true;
        //ecran.texture = movieDegivrer as MovieTexture;
        //movieDegivrer.Play();
        //movieDegivrer.loop = true;
    }

    public void ecranInvisible() {
        ecran.enabled = false;
    }
}
