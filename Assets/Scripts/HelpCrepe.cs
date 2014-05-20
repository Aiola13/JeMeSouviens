using UnityEngine;
using System.Collections;

public class HelpCrepe : TouchLogic {

    public override void OnTouchBegan(){
        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        

        if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.tag == "Skypi")) {
            Debug.Log("Miaou!!");

            //curGameState = GameManagerCrepe.GameState.aideDeSkypi;
            //prevGameState = GameManagerCrepe.GameState.preparationPate;
        }
    }

}
