using UnityEngine;
using System.Collections;

public class Parcelle : MonoBehaviour {

	public enum ParcelleState {
		creuser,
		graine,
		arrosage,
		maturation
	}
	
	public Material dirt;
	public Material plowDirt;
	public Material grass;

	public ParcelleState _curState;
	public ParcelleState _prevState;
	public GUITexture _legume;
	public bool isSelected = false;

	public int _nbTimesDigged = 0;

	public bool estMure = false;
	public float tpsPrEtreMure = 0.0f;
	private float tpsMaturite = 5.0f;

	public Transform arrosoir;
	public Transform arrosoirEmpty;
	private Vector3 arrosoirOffset = new Vector3 (0, 1.2f, 0);
	private Vector3 ArrosoirOriginalPos = new Vector3 (-5, 2, 0);


	void Start() {
		_curState = ParcelleState.creuser;
		renderer.material = dirt;

		arrosoir = GameObject.FindGameObjectWithTag("Arrosoir").transform;
		arrosoirEmpty = GameObject.FindGameObjectWithTag("ArrosoirEmpty").transform;

		arrosoir.gameObject.renderer.enabled = false;

		ArrosoirOriginalPos = arrosoir.transform.position;
	}


	void Update() {
		if (GetCurState() == ParcelleState.maturation) {
			// si le legume est en train de murir
			if (tpsPrEtreMure < tpsMaturite)
				tpsPrEtreMure += Time.deltaTime;
			// le legume est pret a etre mur
			else {
				if (!estMure)
					AMuri();

			}
		}
	}

	public void AEteSelectionne() {
		(gameObject.GetComponent("Halo") as Behaviour).enabled = true;
		if (_curState == ParcelleState.arrosage)
			PositionnerArrosoir();
		isSelected = true;
        GameManagerJardin.sndASSelect.Play();
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
        GameManagerJardin.sndASCreuse.Play();
	}


	public void AEteSeme(GUITexture leg) {
		renderer.material.color = new Color(0.3F, 0.5F, 0.3F, 0.3F);
		_legume = leg;
		PositionnerArrosoir();
		ChangeState(ParcelleState.graine, ParcelleState.arrosage);
        GameManagerJardin.sndASPousse.Play();
	}


	public void AEteArrose() {
		renderer.material = grass;
		EnleverArrosoir();
		QueteJardin scriptQueteJardin = GameObject.Find("_GameManager").GetComponent<QueteJardin>();
		scriptQueteJardin.IncrementNbLegumesArroses();
		ChangeState(ParcelleState.arrosage, ParcelleState.maturation);
        GameManagerJardin.sndASArrose.Play();
	}


	public void AMuri() {
		renderer.material.color = Color.white;
		renderer.material.mainTexture = _legume.texture;
		estMure = true;
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
		tpsPrEtreMure = 0.0f;
		estMure = false;
		(gameObject.GetComponent("Halo") as Behaviour).enabled = false;
	}


	// Parameters: prev State, curr State
	public void ChangeState(ParcelleState prev, ParcelleState current) { 
		_curState = current;
		_prevState = prev;
	}


	public ParcelleState GetCurState () {
		return _curState;
	}


	void PositionnerArrosoir() {
		arrosoir.gameObject.renderer.enabled = true;
		arrosoir.position = new Vector3(transform.position.x, transform.position.y, transform.position.z) + arrosoirOffset;

		arrosoir.eulerAngles = Camera.main.transform.eulerAngles;

		/*

		// trick pour que l'arrosoir ait la meme orientation que la camera
		// arrosoir devient le parent de arrosoirEmpty
		arrosoirEmpty.parent = arrosoir;
		// on reset le transform de arrosoirEmpty
		arrosoirEmpty.position = Vector3.zero;
		// on déparente arrosoirEmpty
		arrosoirEmpty.parent = null;
		// on rotate arrosoirEmpty pour s'ajuster a la camera
		arrosoirEmpty.eulerAngles = Camera.main.transform.eulerAngles;
		// arrosoirEmpty devient le parent de arrosoir
		arrosoir.parent = arrosoirEmpty;
		arrosoirEmpty.position = new Vector3(transform.position.x, transform.position.y, transform.position.z) + arrosoirOffset;

		//arrosoir.position = new Vector3(transform.position.x, transform.position.y, transform.position.z) + arrosoirOffset;
		arrosoir.eulerAngles = arrosoirEmpty.eulerAngles;
		*/

	}


	void EnleverArrosoir() {
		//arrosoirEmpty.parent = null;
		//arrosoir.position = new Vector3(ArrosoirOriginalPos.x, ArrosoirOriginalPos.y, ArrosoirOriginalPos.z);
		arrosoir.gameObject.renderer.enabled = false;
	}
}
