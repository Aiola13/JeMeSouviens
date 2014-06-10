using UnityEngine;
using System.Collections;

public class MainMenu : TouchLogic {

    public override void OnTouchEndedAnywhere() {

		Ray ray ;
		RaycastHit hit;
		ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

		if (Physics.Raycast(ray, out hit)) {

			if (hit.collider.gameObject.name == "Play") {
				hit.collider.gameObject.renderer.material.color = Color.green;
				Application.LoadLevel("a_menu");
			}

			else if (hit.collider.gameObject.name == "Peche") {
				hit.collider.gameObject.renderer.material.color = Color.green;
				Application.LoadLevel("a_peche");
			}

			else if (hit.collider.gameObject.name == "Crepe") {
				hit.collider.gameObject.renderer.material.color = Color.green;
				Application.LoadLevel("a_crepe");
			} 
            
            else if (hit.collider.gameObject.name == "Jardinage") {
                hit.collider.gameObject.renderer.material.color = Color.green;
                Application.LoadLevel("a_jardin");
            }

			else if (hit.collider.gameObject.name == "Stats") {
				hit.collider.gameObject.renderer.material.color = Color.green;
				//Application.LoadLevel("stats");
			}

			else if (hit.collider.gameObject.name == "Quit") {
				hit.collider.gameObject.renderer.material.color = Color.red;
				Application.Quit();
			}
		}
	}

}
