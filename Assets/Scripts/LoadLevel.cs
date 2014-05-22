using UnityEngine;
using System.Collections;

public class LoadLevel : TouchLogic {

	void OnTouchBeganAnywhere(){
		Application.LoadLevel("a_crepe_1");
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
