using UnityEngine;
using System.Collections;

public class testA : MonoBehaviour {



	void Update () {

		transform.Translate(20*Time.deltaTime*Input.GetAxisRaw("Horizontal"),0,0);

	}
}
