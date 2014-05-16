using UnityEngine;
using System.Collections;

public class GameManagerCrepe : MonoBehaviour
{

    #region attributs

    public enum GameState
    {
        queteNoemie,
        preparationPate,
        etalerLeBeurre,
        cuissonCrepe
    }

    GameState curGameState;
    GameState prevGameState;

    GameObject[] listeIngQuete;

    GameObject[] listeIngSaladier;

    #endregion

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
