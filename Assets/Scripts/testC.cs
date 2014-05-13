using UnityEngine;
using System.Collections;

public class testC : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 move = new Vector3();

		if(Input.GetKey(KeyCode.UpArrow))
			move.z += 0.1f;
		if(Input.GetKey(KeyCode.DownArrow))
			move.z -= 0.1f;

		transform.position += move;
	}
}
