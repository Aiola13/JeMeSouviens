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

	public int _nbTimesDigged = 0;


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
		if (_nbTimesDigged == 1)
			renderer.material.color = new Color(1.0F, 1.0F, 1.0F, 0.5F);
		else if (_nbTimesDigged == 2)
			renderer.material.color = new Color(0.6F, 0.6F, 0.6F, 0.6F);
		else if (IsFullyDigged()) {
			renderer.material.color = new Color(0.3F, 0.3F, 0.3F, 0.8F);
			ChangeState(ParcelleState.creuser, ParcelleState.graine);
		}
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
	
	public void IncrementDigged() {
		// we need 3 little swipes to fully plow a plot
		if (_nbTimesDigged < 3)
			_nbTimesDigged++;
	}

	// returns true if we have fully digged the plot, else return false
	public bool IsFullyDigged() {
		if (_nbTimesDigged == 3)
			return true;
		else
			return false;
	}

	// Parameters: prev State, curr State
	public void ChangeState(ParcelleState prev, ParcelleState current) { 
		_curState = current;
		_prevState = prev;
	}

}
