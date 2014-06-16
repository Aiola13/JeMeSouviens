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
	
	public GameObject arrosoir;
	private Vector3 arrosoirOffset = new Vector3 (0, 1.2f, 0);
	private Vector3 ArrosoirOriginalPos = new Vector3 (-5, 2, 0);


	void Start() {
		_curState = ParcelleState.creuser;
		renderer.material = dirt;

		arrosoir = GameObject.FindGameObjectWithTag("Arrosoir");
		ArrosoirOriginalPos = arrosoir.transform.position;
	}


	public void AEteSelectionne() {
		(gameObject.GetComponent("Halo") as Behaviour).enabled = true;
		if (_curState == ParcelleState.arrosage)
			PositionnerArrosoir();
		isSelected = true;
	}


	public void AEteDeSelectionne() {
		(gameObject.GetComponent("Halo") as Behaviour).enabled = false;
		arrosoir.transform.position = ArrosoirOriginalPos;
		isSelected = false;
	}


	public void AEteCreuse() {
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


	public void AEteSeme(GUITexture leg) {
		renderer.material.color = new Color(0.3F, 0.5F, 0.3F, 0.3F);
		_legume = leg;
		PositionnerArrosoir();
		ChangeState(ParcelleState.graine, ParcelleState.arrosage);
	}


	public void AEteArrose() {
		renderer.material = grass;
		EnleverArrosoir();
		QueteJardin scriptQueteJardin = GameObject.Find("_GameManager").GetComponent<QueteJardin>();
		scriptQueteJardin.IncrementNbLegumesArroses();
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


	public void ResetParcelle() {
		_curState = ParcelleState.creuser;
		_prevState = ParcelleState.creuser;

		renderer.material = dirt;
		_legume = null;
		isSelected = false;
		_nbTimesDigged = 0;
		(gameObject.GetComponent("Halo") as Behaviour).enabled = false;
	}


	// Parameters: prev State, curr State
	public void ChangeState(ParcelleState prev, ParcelleState current) { 
		_curState = current;
		_prevState = prev;
	}


	void PositionnerArrosoir() {
		arrosoir.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z) + arrosoirOffset;
	}


	void EnleverArrosoir() {
		arrosoir.transform.position = ArrosoirOriginalPos;
	}
}
