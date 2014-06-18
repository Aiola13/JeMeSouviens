using UnityEngine;
using System.Collections;

public class EtalerPate : MonoBehaviour {
	
	public static bool isEtaler = false;
	public static bool Nord = false;  // point de control
	public static bool Est = false;
	public static bool Sud = false;
	public static bool Ouest = false;

	public static bool NordEst = false;
	public static bool SudEst = false;
	public static bool NordOuest = false;
	public static bool SudOuest = false;

	public GameObject pateFreez ;
	public GameObject[] pateFreezList ;
	public GameObject pate ;

	public Vector3 centreFreez;
	public Vector3 centreDep;

	public int spawnCrepe = 5 ;

	public float distance1 ;
	public float distance2 ;

	// Use this for initialization
	void Start () {
		centreFreez = pateFreez.transform.position ;
		distance1 = 0.129236F ;
		
	} 
	
	float Distance (float xa, float ya, float xb, float yb){
		float calcul1 ;
		float calcul2 ;

		calcul1 = xb-xa ;
		calcul1 = calcul1*calcul1 ;

		calcul2 = yb-ya ;
		calcul2 = calcul2 * calcul2 ;

		calcul1 = calcul1 + calcul2 ; 

		return Mathf.Sqrt(calcul1);

	}

	// Update is called once per frame
	void Update () {

					

		if(GameManagerCrepe.curGameState == GameManagerCrepe.GameState.etalerLaPate && !isEtaler){
			
			spawnCrepe -=1 ;

			if(spawnCrepe <= 0){
				Instantiate(pateFreez, pate.transform.position, Quaternion.identity);
				spawnCrepe = 20 ;
			}




			distance2 = Distance(centreFreez.x, centreFreez.z, pate.transform.position.x + Input.acceleration.x/100, pate.transform.position.z);
			if (distance1>distance2 ) {
			 	transform.Translate(Input.acceleration.x/100, 0, 0) ;
			}

			distance2 = Distance(centreFreez.x, centreFreez.z, pate.transform.position.x, pate.transform.position.z + Input.acceleration.y/100);
		
			if (distance1>distance2 ) {
				transform.Translate(0,0,Input.acceleration.y/100) ;
			}
			
			if (pate.transform.position.z > -1.69) {
				
				Nord = true ;
				
			}
			if (pate.transform.position.x > -2.3) {
				
				Est = true ;
				
			}
			if (pate.transform.position.z < -1.92) {
				
				Sud = true ;
				
			}
			if (pate.transform.position.x < -2.52) {
				
				Ouest = true ;
				
			}
			if (pate.transform.position.x > -2.35 && pate.transform.position.z > -1.7 ) {
				
				NordEst = true ;
				
			}
			if (pate.transform.position.x > -2.35 && pate.transform.position.z < -1.85 ) {
				
				SudEst = true ;
				
			}
			if (pate.transform.position.x < -2.45 && pate.transform.position.z > -1.7 ) {
				
				NordOuest = true ;
				
			}
			if (pate.transform.position.x < -2.45 && pate.transform.position.z < -1.85 ) {
				
				SudOuest = true ;
				
			}

			
			if( Nord && Est && Sud && Ouest && NordEst && SudEst && NordOuest && SudOuest ){
				isEtaler = true ;
				pateFreezList = GameObject.FindGameObjectsWithTag("PateFreeze") ;
				
				foreach( GameObject PateF in pateFreezList){

					DestroyObject(PateF);
					DestroyObject(pate);
				}
				
			}
				
		
		}
	
	}
}
