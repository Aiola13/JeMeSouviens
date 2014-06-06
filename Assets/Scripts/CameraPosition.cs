 using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour
{
	public float speed = 5.0f;         // The relative speed at which the camera will catch up.
	private Vector3 posCrepe = new Vector3(-4,2,-10);
	private Vector3 posPeche  = new Vector3(14,2,-10);
	private Vector3 posCam  = new Vector3(-4,2,-10);
	private float startTime;
	void Update() {
		float distCovered = (Time.time - startTime) * speed;
		if (TouchLogicSwipe.swipeRight) {
			//print(Vector3.Lerp(posCrepe, posPeche,  1000.0f));
				transform.Translate(Vector3.right * Time.deltaTime * speed); 
			//transform.position += Vector3.right * Time.deltaTime * speed;
			//transform.position += Vector3.Lerp(posCrepe, posPeche, 1.0f);
		}
		else if (TouchLogicSwipe.swipeLeft)
			transform.Translate(Vector3.left * Time.deltaTime * speed);
		// Move the object upward in world space 1 unit/second.
		// transform.Translate(Vector3.up * Time.deltaTime, Space.World);
	}

	void start() {
		startTime = Time.time;
      // private Vector3 posCam  = transform.position;
	}

	/*protected void CameraMoveTo(Vector3 posPeche, Vector3 posCrepe) {
		transform.position = Vector3.Lerp(posCrepe, posPeche,  percentageComplete);
			if(percentageComplete >= 1.0f)
			{
				transform.Translate(Vector3.right * Time.deltaTime * speed); 
				_isLerping = false;
			}
		// interpolation pour aller plus près du plan de travail
		float temps = 1000.0f;
		Camera.main.transform.position = Vector3.Lerp(transform.position, posPeche, temps);
	}*/
}