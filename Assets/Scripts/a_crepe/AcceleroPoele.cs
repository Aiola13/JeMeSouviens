using UnityEngine;
using System.Collections;

public class AcceleroPoele : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManagerCrepe.curGameState == GameManagerCrepe.GameState.etalerLaPate){

			if (transform.rotation.eulerAngles.x >= 335 || (transform.rotation.eulerAngles.x >= 325 && Input.acceleration.x >= 0))
	        {

	            transform.Rotate(Input.acceleration.x, 0, 0);

	        }

	        if (transform.rotation.eulerAngles.x <= 30 || (transform.rotation.eulerAngles.x <= 45 && Input.acceleration.x <= 0))
	        {

	            transform.Rotate(Input.acceleration.x, 0, 0);

	        }
	        if (transform.rotation.eulerAngles.z <= 30 || (transform.rotation.eulerAngles.z <= 45 && Input.acceleration.y <= 0))
	        {

	            transform.Rotate(0, 0, Input.acceleration.y);

	        }
	        if (transform.rotation.eulerAngles.z >= 335 || (transform.rotation.eulerAngles.z >= 325 && Input.acceleration.y >= 0))
	        {

	            transform.Rotate(0, 0, Input.acceleration.y);

	        }
		}
	}
}
