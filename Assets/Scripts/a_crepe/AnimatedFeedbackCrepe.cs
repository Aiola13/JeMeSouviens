using UnityEngine;
using System.Collections;

public class AnimatedFeedbackCrepe : MonoBehaviour {

    public GUITexture ecran;
    public Texture[] dragIngredients;
    public Texture[] etalerPate;
    public Texture[] retournerCrepe;

    int framesPerSecond = 5;

    bool playEtaler;
    bool playRetourner;
    bool playDrag;

    // Use this for initialization
    void Start() {
        ecran.enabled = false;
        playEtaler = false;
        playRetourner = false;
        playDrag = false;
    }

    void Update() {
        if (playEtaler) {
            int index = (int)(Time.time * framesPerSecond) % etalerPate.Length;
            ecran.texture = etalerPate[index];
        } else if (playRetourner) {
            int index = (int)(Time.time * framesPerSecond) % retournerCrepe.Length;
            ecran.texture = retournerCrepe[index];
        } else if (playDrag) {
            int index = (int)(Time.time * framesPerSecond) % dragIngredients.Length;
            ecran.texture = dragIngredients[index];
        }
    }

    public void playVidDrag() {
        ecran.enabled = true;
        playDrag = true;
    }

    public void playVidRetourner() {
        ecran.enabled = true;
        playRetourner = true;
    }

    public void playVidEtaler() {
        ecran.enabled = true;
        playEtaler = true;
    }

    public void ecranInvisible() {
        ecran.enabled = false;
        playEtaler = false;
        playRetourner = false;
        playDrag = false;
    }

}
