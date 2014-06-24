using UnityEngine;
using System.Collections;

public class PulseArrow : MonoBehaviour {

	private bool isGrowing = true;
	private float pTime = 0.0f;
	private float pulseTime = 0.5f;


	void Update () {

		if (isGrowing && pTime < pulseTime) {
			GrowScale();
			pTime += Time.deltaTime;
		}

		else if (isGrowing && pTime > pulseTime) {
			isGrowing = false;
			pTime = 0.0f;
		}

		else if (!isGrowing && pTime < pulseTime) {
			ShrinkScale();
			pTime += Time.deltaTime;
		}

		else if (!isGrowing && pTime > pulseTime) {
			isGrowing = true;
			pTime = 0.0f;
		}
	}




	void GrowScale() {
		transform.localScale += new Vector3(0.0005f, 0.0005f, 0);
	}


	void ShrinkScale() {
		transform.localScale -= new Vector3(0.0005f, 0.0005f, 0);
	}
}
