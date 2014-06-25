using UnityEngine;
using System.Collections;

public class AnimatedFeedbackJardin : MonoBehaviour {

    public GUITexture ecran;
    public Texture[] dragGraine;
    public Texture[] arroser;
    public Texture[] creuser;

    int framesPerSecond = 5;

    bool playDragGraine;
    bool playCreuser;
    bool playArroser;

    // Use this for initialization
    void Start() {
        ecran.enabled = false;
        playDragGraine = false;
        playCreuser = false;
        playArroser = false;
    }

    void Update() {
        if (playDragGraine) {
            int index = (int)(Time.time * framesPerSecond) % dragGraine.Length;
            ecran.texture = dragGraine[index];
        } else if (playCreuser) {
            int index = (int)(Time.time * framesPerSecond) % creuser.Length;
            ecran.texture = creuser[index];
        } else if (playArroser) {
            int index = (int)(Time.time * framesPerSecond) % arroser.Length;
            ecran.texture = arroser[index];
        }
    }

    public void playVidDragGraine() {
        ecran.enabled = true;
        playDragGraine = true;
        playCreuser = false;
        playArroser = false;
    }

    public void playVidCreuser() {
        ecran.enabled = true;
        playCreuser = true;
        playArroser = false;
        playDragGraine = false;
    }

    public void playVidArroser() {
        ecran.enabled = true;
        playArroser = true;
        playDragGraine = false;
        playCreuser = false;
    }

    public void ecranInvisible() {
        ecran.enabled = false;
        playDragGraine = false;
        playCreuser = false;
        playArroser = false;
    }

}
