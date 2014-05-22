using UnityEngine;
using System.Collections;

public class MainMenu : TouchLogic {

	public override void OnTouchBeganAnywhere(){

		Ray ray ;
		RaycastHit hit;
		ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

		if (Physics.Raycast(ray, out hit)) {

			if (hit.collider.gameObject.name == "Play") {
				hit.collider.gameObject.renderer.material.color = Color.green;
				Application.LoadLevel("a_crepe_1");
			}
			else {
				hit.collider.gameObject.renderer.material.color = Color.red;
				Application.Quit();
			}
		}

	}

}
