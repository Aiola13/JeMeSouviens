using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Peche : MonoBehaviour {

    float timerAMordu;
    float timerGele;
    float timer;
    int dureeProchainPoisson;

    public bool poissonPeche;
    public string infoPoisson;
    public bool solGele = false;
    bool sonCanneApeche = false;

    GameObject poi_sebaste;
    GameObject poi_morue;
    GameObject poi_saumon;
    GameObject poi_eperlant;
    GameObject poi_turbot;

    GameObject canneAPeche;

    public GameObject poisson;

    float AccYPrev = 0.0f;
    float AccZPrev = 0.0f;

	// Use this for initialization
	void Start () {
        
        timer = 0.0f;
        timerAMordu = 0.0f;
        timerGele = 0.0f;
        dureeProchainPoisson = Random.Range(3, 7);
        poissonPeche = false;
        infoPoisson = "";

        poi_eperlant = GameObject.FindGameObjectWithTag("poi_eperlant");
        poi_turbot = GameObject.FindGameObjectWithTag("poi_turbot");
        poi_saumon = GameObject.FindGameObjectWithTag("poi_saumon");
        poi_morue = GameObject.FindGameObjectWithTag("poi_morue");
        poi_sebaste = GameObject.FindGameObjectWithTag("poi_sebaste");
        canneAPeche = GameObject.FindGameObjectWithTag("CanneAPeche");

        poisson = new GameObject();
	}
	
	// Update is called once per frame
	void Update () {

        if (GameManagerPeche.curGameState == GameManagerPeche.GameState.pecher && poissonPeche == false) {
            
            timer += Time.deltaTime;
            timerGele += Time.deltaTime;

            if (timer >= dureeProchainPoisson) {
                if (!sonCanneApeche) {
                    SonCannePeche();
                    sonCanneApeche = true;
                }
                //Handheld.Vibrate();
                //canneAPeche.animation.Play();
                Debug.Log("Un poisson a mordu!!");
                
                timerAMordu += Time.deltaTime;

                if (GestureTirerCanneAPeche()) {
                    Debug.Log("a peche un poisson");
                    poissonPeche = true;
                    sonCanneApeche = false;
                    GarderPoisson();
                    AccYPrev = 0.0f;
                    AccZPrev = 0.0f;
                    timer = 0.0f;
                    timerAMordu = 0.0f;
                } 
                else if (timerAMordu >= 2.0f) {
                    Debug.Log("Le poisson c'est echape");
                    timer = 0.0f;
                    timerAMordu = 0.0f;
                    sonCanneApeche = false;
                }
            } 
            else if (timerGele >= 30.0f) {

                solGele = true;
                timerGele = 0.0f;
                timer = 0.0f;
                sonCanneApeche = false;
            }
        }

	}

    void GarderPoisson() {

        Vector3 positionPoisson = new Vector3(116, 129, 83);
        Quaternion rotationPoisson = Quaternion.identity;
        rotationPoisson.eulerAngles = new Vector3(0, 90, 20);
        int randomPoisson = Random.Range(1, 6);

        switch (randomPoisson) {
            case 1:
                poisson = (GameObject)Instantiate(poi_eperlant, positionPoisson, rotationPoisson);
                break;
            case 2:
                poisson = (GameObject)Instantiate(poi_turbot, positionPoisson, rotationPoisson);
                break;
            case 3:
                poisson = (GameObject)Instantiate(poi_saumon, positionPoisson, rotationPoisson);
                break;
            case 4:
                poisson = (GameObject)Instantiate(poi_morue, positionPoisson, rotationPoisson);
                break;
            case 5:
                poisson = (GameObject)Instantiate(poi_sebaste, positionPoisson, rotationPoisson);
                break;
        }

        string nomPoisson = poisson.tag;

        switch (nomPoisson) {

                case "poi_eperlant":
                    nomPoisson = "eperlant";
                    break;
                case "poi_turbot":
                    nomPoisson = "turbot";
                    break;
                case "poi_morue":
                    nomPoisson = "morue";
                    break;
                case "poi_saumon":
                    nomPoisson = "saumon";
                    break;
                case "poi_sebaste":
                    nomPoisson =  "sebaste";
                    break;
            }

        infoPoisson = "Le poisson que tu vient de pêcher est un " + nomPoisson + ".\n";
        infoPoisson += "Veux-tu le garder?";


    }

    void SonCannePeche() {
        GameManagerPeche.canneApeche.Play();
    }

    // Fonction du bled
    bool GestureTirerCanneAPeche(){
        /*
        print("AXE Y :   " + (Mathf.Abs(Input.acceleration.y) - Mathf.Abs(AccYPrev)));
        print("AXE Z :   " + (Mathf.Abs(Input.acceleration.z) - Mathf.Abs(AccZPrev)));

        if (Mathf.Abs(Input.acceleration.y) - Mathf.Abs(AccYPrev) > 0.1 && Mathf.Abs(Input.acceleration.z) - Mathf.Abs(AccZPrev) > 0.1) {
            AccYPrev = Input.acceleration.y;
            AccZPrev = Input.acceleration.z;
            return false;
        }
        */
        if (Input.acceleration.y + AccYPrev <= -1.2 && Input.acceleration.z + AccZPrev >= -0.6) {
            return true;
        }
        else{
            AccYPrev = Input.acceleration.y;
            AccZPrev = Input.acceleration.z;
            return false;
        }
    }

}
