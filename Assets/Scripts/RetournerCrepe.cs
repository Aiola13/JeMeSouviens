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


			if(counter >= 5.0f){
				if(isCook){
						gameObject.renderer.material.color = Color.black;
					}
				else{
					gameObject.renderer.material.color = Color.green;

					if (Input.acceleration.y <= -0.95 ){
							animation.Play("RetournerCrepeAnim");
							counter = 0.0f; 	
						isCook = true;
							
						}
				}
			}
		}
	}
}
