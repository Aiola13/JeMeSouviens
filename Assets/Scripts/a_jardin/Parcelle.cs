using UnityEngine;
using System.Collections;

public class Parcelle : MonoBehaviour {

	public enum ParcelleState {
		creuser,
		graine,
		arrosage,
		mature
	}

	private Color originalColor;

	public ParcelleState _curState;
	public ParcelleState _prevState;
	public GUITexture _legume;
	public bool isSelected = false;

	void Awake() {
		_curState = ParcelleState.creuser;

		originalColor = renderer.material.color;
	}


	void Update() {
		if (isSelected)
			renderer.material.color = Color.red;
		else
			renderer.material.color = originalColor;
	}


	// Parameters: prev State, curr State
	public void ChangeState(ParcelleState prev, ParcelleState current) { 
		_curState = current;
		_prevState = prev;
	}

}
