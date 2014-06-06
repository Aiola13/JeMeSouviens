 using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour
{
	public float speed = 5.0f;         // The relative speed at which the camera will catch up.
	private float posCrepe = -4.0f;
	private float posPeche  = 14.0f;
	private float posCam;



	void Update() {
		posCam = transform.position.x;

		if (TouchLogicSwipe.swipeLeft && posCam < posPeche) {
			transform.Translate(Vector3.right * Time.deltaTime * speed);
		}
		else if (TouchLogicSwipe.swipeRight && posCam > posCrepe)
			transform.Translate(Vector3.left * Time.deltaTime * speed);
	}

	void start() {
	}
}