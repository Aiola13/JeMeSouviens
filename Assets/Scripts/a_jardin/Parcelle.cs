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
	private Color curColor;


	public ParcelleState _curState;
	public ParcelleState _prevState;
	public GUITexture _legume;
	public bool isSelected = false;

	void Awake() {
		_curState = ParcelleState.creuser;

		originalColor = renderer.material.color;
		curColor = originalColor;
	}


	public void AEteSelectionne() {
		(gameObject.GetComponent("Halo") as Behaviour).enabled = true;
		isSelected = true;
	}

	public void AEteDeSelectionne() {
		(gameObject.GetComponent("Halo") as Behaviour).enabled = false;
		isSelected = false;
	}


	public void AEteCreuser() {
		renderer.material.color = Color.gray;
		curColor = renderer.material.color;
		ChangeState(ParcelleState.creuser, ParcelleState.graine);
	}

	public void AEtePlante() {
		renderer.material.color = Color.cyan;
		curColor = renderer.material.color;
		ChangeState(ParcelleState.graine, ParcelleState.arrosage);
	}

	public void AEteArroser() {
		renderer.material.color = Color.green;
		curColor = renderer.material.color;
		ChangeState(ParcelleState.arrosage, ParcelleState.mature);
	}

	public void EstMature() {
		renderer.material.color = Color.green;
		curColor = renderer.material.color;
	}

	// Parameters: prev State, curr State
	public void ChangeState(ParcelleState prev, ParcelleState current) { 
		_curState = current;
		_prevState = prev;
	}

}
