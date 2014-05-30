using UnityEngine;
using System.Collections;

public class Peche : MonoBehaviour {

    public float timer;
    public int dureeProchainPoisson;
    bool aMordu;

	// Use this for initialization
	void Start () {
        
        timer = 0.0f;
        dureeProchainPoisson = Random.Range(3, 7);
        aMordu = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (GameManagerPeche.curGameState == GameManagerPeche.GameState.pecher) {
            
            timer += Time.deltaTime;

            if (timer >= dureeProchainPoisson) {
                Handheld.Vibrate();
                this.transform.rotation = Quaternion.Euler(40, 0, 0);
                aMordu = true;
            }

            if (aMordu) {

            }
        }



	}
}
