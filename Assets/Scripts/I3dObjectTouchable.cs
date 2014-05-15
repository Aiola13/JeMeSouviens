using UnityEngine;
using System.Collections;

public interface I3dObjectTouchable {

	void OnTouchBegan();
	void OnTouchEnded();
	void OnTouchMoved();
	void OnTouchStationary();


	void OnTouchEndedAnywhere();
	void OnTouchMovedAnywhere();
	void OnTouchStationaryAnywhere();
}
