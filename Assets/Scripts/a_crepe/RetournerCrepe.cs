using UnityEngine;
using System.Collections;

public class RetournerCrepe : MonoBehaviour {

	public static float counter = 0.0f;
	public static bool isCook = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

       
		if(GameManagerCrepe.curGameState == GameManagerCrepe.GameState.cuissonCrepe){
            counter += Time.deltaTime;
            GameManagerCrepe.cuisson.Play();
			
			if (Input.acceleration.y <= -0.25 && counter >= 5.0f  && !isCook){
				animation.Play("RetournerCrepeAnim");
				isCook = true;
			}
		}
	}
}
