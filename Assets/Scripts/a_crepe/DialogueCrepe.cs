using UnityEngine;
using System.Collections;

public class DialogueCrepe : TouchLogic {
	
	Vector3 camPreparationPatePos = new Vector3(0, 2, -3);
	Vector3 camCuissonPos = new Vector3(-2.7f, 2, -3);

    public GameObject crepe;
    public GameObject poele;

    protected Ray ray;
	protected RaycastHit hit;
    public static bool canRestartChrono = false;

	public GUITexture rejouer;
	public GUIText rejouerText;

	void Start() {
        crepe.renderer.enabled = false;

		GameManager.NePasAfficherTexture(rejouer, rejouerText);
	}
	
	public override void OnTouchEndedAnywhere () {

		// State quete noemie
		if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.queteNoemie) {
			CameraMove(camPreparationPatePos);
            GameManager.ActiverDrag();
			ChangeState(GameManagerCrepe.GameState.queteNoemie, GameManagerCrepe.GameState.preparationPate);
		}
		
		// State aide de skypi
		else if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.aideDeSkypi) {
            GameManager.ActiverDrag();
            canRestartChrono = true;
			ChangeState(GameManagerCrepe.GameState.aideDeSkypi, GameManagerCrepe.prevGameState);
		}
		
		// si on est en train de preparer la pate et qu'on a appuyer n'importe sur le bouton de validation
        else if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.preparationPate) {

			if (GameManagerCrepe.bQueteAccomplie) {
                ChangeState(GameManagerCrepe.GameState.preparationPate, GameManagerCrepe.GameState.etalerLaPate);
				CameraMove(camCuissonPos);
				RotateCat();
			}

            GameManager.ActiverDrag();
            canRestartChrono = true;
		}

        // State étaler la pate
        else if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.etalerLaPate) {
            
            if (EtalerPate.isEtaler) {
                etatCuisson();
                ChangeState(GameManagerCrepe.GameState.etalerLaPate, GameManagerCrepe.GameState.cuissonCrepe);
            }
            canRestartChrono = true;
        }

		// State cuisson
		else if (GameManagerCrepe.curGameState == GameManagerCrepe.GameState.cuissonCrepe) {
            canRestartChrono = true;
            if (RetournerCrepe.isCook && RetournerCrepe.counter >= 5.0f)
                ChangeState(GameManagerCrepe.GameState.cuissonCrepe, GameManagerCrepe.GameState.score);
		}

		// if we touch the cat
        if (GameManagerCrepe.curGameState != GameManagerCrepe.GameState.queteNoemie && 
            GameManagerCrepe.curGameState != GameManagerCrepe.GameState.score &&
            GameManagerCrepe.curGameState != GameManagerCrepe.GameState.aideDeSkypi) {
            ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.tag == "Skypi")) {
                GameManager.DesactiverDrag();
                switch (GameManagerCrepe.curGameState) {
                    case GameManagerCrepe.GameState.preparationPate:
                        ChangeState(GameManagerCrepe.GameState.preparationPate, GameManagerCrepe.GameState.aideDeSkypi);
                        break;
                    case GameManagerCrepe.GameState.cuissonCrepe:
                        ChangeState(GameManagerCrepe.GameState.cuissonCrepe, GameManagerCrepe.GameState.aideDeSkypi);
                        break;
                }
                GameManagerCrepe.nbAppelsAide++;
                GameManagerCrepe.miaulement.Play();
            }
        }
	}

    protected void RotateCat() {
		Transform t = GameObject.FindGameObjectWithTag("Skypi").transform;
		t.Rotate(t.rotation.x, -75, t.rotation.z);
	}

    protected void CameraMove(Vector3 pos) {
		// interpolation pour aller plus près du plan de travail
		float temps = 1000.0f;
		Camera.main.transform.position = Vector3.Lerp(transform.position, pos, temps);
	}

    void etatCuisson() {

        poele.transform.position = new Vector3(-2.751033f, 0.6469359f, -1.578083f);
        poele.transform.eulerAngles = new Vector3(0, 180, 0);

        crepe.renderer.enabled = true;
        crepe.transform.position = new Vector3(-2.751033f, 0.67359f, -1.578083f);

        CameraMove(new Vector3(-2.7f, 1.5f, -2.75f));

    }
	
	// Parameters: prev State, curr State
    protected void ChangeState(GameManagerCrepe.GameState prev, GameManagerCrepe.GameState current) { 
		GameManagerCrepe.curGameState = current;
		GameManagerCrepe.prevGameState = prev;
	}
}
