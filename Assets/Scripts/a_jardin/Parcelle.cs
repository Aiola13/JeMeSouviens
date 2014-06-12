using UnityEngine;
using System.Collections;

public class Parcelle : MonoBehaviour {

	public enum ParcelleState {
		creuser,
		graine,
		arrosage,
		mature
	}
	
	public Material dirt;
	public Material plowDirt;
	public Material grass;

	public ParcelleState _curState;
	public ParcelleState _prevState;
	public GUITexture _legume;
	public bool isSelected = false;



	void Awake() {
		_curState = ParcelleState.creuser;
		renderer.material = dirt;
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
		renderer.material = plowDirt;
		ChangeState(ParcelleState.creuser, ParcelleState.graine);
	}

	public void AEtePlante() {
		renderer.material.color = Color.gray;
		ChangeState(ParcelleState.graine, ParcelleState.arrosage);
	}

	public void AEteArroser() {
		renderer.material = grass;
		ChangeState(ParcelleState.arrosage, ParcelleState.mature);
	}

	public void EstMature() {
		renderer.material.color = Color.white;
		renderer.material.mainTexture = _legume.texture;
	}

	// Parameters: prev State, curr State
	public void ChangeState(ParcelleState prev, ParcelleState current) { 
		_curState = current;
		_prevState = prev;
	}

}
