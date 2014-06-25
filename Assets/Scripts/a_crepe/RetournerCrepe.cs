using UnityEngine;
using System.Collections;

public class RetournerCrepe : MonoBehaviour {

	public static float counter = 0.0f;
	public static bool isCook = false;
	public static bool isEtaler = false;
	public static bool mont = false ;  // monter descendre tablette pour sauter crepe 


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
						gameObject.renderer.material.color = new Color32(221,152,92,1);
					}
				else{
                    gameObject.renderer.material.color = new Color32(255, 228, 54, 1);

					if (Input.acceleration.y <= -0.95 ){
						mont = true ;
					}
					if(Input.acceleration.y >= -0.50 && mont ){
							
							animation.Play("RetournerCrepeAnim");
							counter = 0.0f; 	
						isCook = true;

						mont = false ;
							
						}
				}

				

			}
		}
	}
}
