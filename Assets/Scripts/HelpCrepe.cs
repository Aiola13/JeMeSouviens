using UnityEngine;
using System.Collections;

public class HelpCrepe : TouchLogic {

    public bool canTouch = true;
   // private GameManagerCrepe gmCrepe;

	void Start () {

        //gmCrepe = GetComponent<GameManagerCrepe>();
	}


    public override void OnTouchBegan()
    {
        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.tag == "Skypi")) {
            Debug.Log("saladier");
            
            //curGameState = GameManagerCrepe.GameState.aideDeSkypi;
            //prevGameState = GameManagerCrepe.GameState.preparationPate;
        }
        else if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.tag == "Saladier")) {
            Debug.Log("saladier");
        }
    }

}
