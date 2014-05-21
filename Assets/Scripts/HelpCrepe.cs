using UnityEngine;
using System.Collections;

public class HelpCrepe : TouchLogic {

    public override void OnTouchBeganAnywhere(){
        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        

        if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.tag == "Skypi")) {
			// Passage à létat suivant
			GameManagerCrepe.curGameState = GameManagerCrepe.GameState.aideDeSkypi;
			GameManagerCrepe.prevGameState = GameManagerCrepe.GameState.preparationPate;

			print ("INHELPCREPE  cur : " + GameManagerCrepe.curGameState + "    prev :  " + GameManagerCrepe.prevGameState);
        }
    }

}
