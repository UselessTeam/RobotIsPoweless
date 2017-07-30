using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapElement : MonoBehaviour {
	// Use this for initialization
	public Position p; //Please Initiate

	void Start () {
		
	}
	public bool ProcessTurn(){
		Debug.Log ("Erreur, la classe abstraite MapElement essaie de jouer son tour");
		return false;
	}
	// Update is called once per frame

	public abstract bool isFree ();
	public abstract bool isPushable ();
}
