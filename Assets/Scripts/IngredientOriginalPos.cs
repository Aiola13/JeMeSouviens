using UnityEngine;
using System.Collections;

public class IngredientOriginalPos : MonoBehaviour {

	public Vector3 originalPos;

	void Awake () {
		originalPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
}
