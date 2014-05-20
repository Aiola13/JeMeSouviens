using UnityEngine;
using System.Collections;

public class HelpCrepe : TouchLogic {

    private GameManagerCrepe gmCrepe;

	void Start () {

        gmCrepe = GetComponent<GameManagerCrepe>();
	}

    public override void OnTouchBeganAnywhere()
    {
        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.tag == "Skypi"))
        {
            
            Debug.Log("Miaou!!");
            
            gmCrepe.curGameState = GameManagerCrepe.GameState.aideDeSkypi;
            gmCrepe.prevGameState = GameManagerCrepe.GameState.preparationPate;
        }

    }

}
