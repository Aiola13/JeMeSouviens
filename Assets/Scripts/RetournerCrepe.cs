using UnityEngine;
using System.Collections;

public class RetournerCrepe : MonoBehaviour {

	float counter = 0.0f;
	//bool time = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

       
		if(GameManagerCrepe.curGameState == GameManagerCrepe.GameState.etalerLeBeurre){
            counter += Time.deltaTime;
            print("counter" + counter);
			
			if (Input.acceleration.y <= -0.25 && counter >= 5.0f){
				animation.Play("RetournerCrepeAnim");
			}
		}
	}
}
