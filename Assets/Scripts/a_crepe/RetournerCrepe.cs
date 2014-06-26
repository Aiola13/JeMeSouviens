using UnityEngine;
using System.Collections;

public class RetournerCrepe : MonoBehaviour {

	public static float counter = 0.0f;
	public static bool isCook = false;
	public static bool isEtaler = false;
	//public static bool mont = false ;  // monter descendre tablette pour sauter crepe 

    float AccYPrev = 0.0f;
    float AccZPrev = 0.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

       
		if(GameManagerCrepe.curGameState == GameManagerCrepe.GameState.cuissonCrepe){
            counter += Time.deltaTime;
            GameManagerCrepe.cuisson.Play();


			if(counter >= 3.0f){
				if(isCook){
						gameObject.renderer.material.color = new Color32(221,152,92,1);
					}
				else{

                    gameObject.renderer.material.color = new Color32(255, 228, 54, 1);

					if (Input.acceleration.y + AccYPrev <= -1.2 && Input.acceleration.z + AccZPrev >= -0.6) {
                        animation.Play("RetournerCrepeAnim");
                        isCook = true;
                        counter = 0.0f; 
                    }
                    else{
                        AccYPrev = Input.acceleration.y;
                        AccZPrev = Input.acceleration.z;
                    }
				}
			}
		}
	}
}
