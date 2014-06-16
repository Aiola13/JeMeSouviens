 using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour
{
	public float speed = 20.0f;         // The relative speed at which the camera will catch up.
	/*
	private float posCrepe = 0.0f;
	private float posPeche  = 20.0f;
    private float posJardin = 40.0f;
*/
	private float posCam;


    public GameObject focus;


	void Update() {
		posCam = transform.position.x;

        // on bouge la camera a droite
		if (TouchLogicSwipe.swipeLeft) {
            if (focus.name == "Crepe")
                focus = GameObject.Find("Peche");
            else if (focus.name == "Peche")
                focus = GameObject.Find("Jardinage");

            if (posCam < focus.transform.position.x) {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
        }

        // on bouge la camera a gauche
        else if (TouchLogicSwipe.swipeRight) {
            if (focus.name == "Jardinage")
                focus = GameObject.Find("Peche");
            else if (focus.name == "Peche")
                focus = GameObject.Find("Crepe");


            if (posCam > focus.transform.position.x) {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }

        }
	}

}