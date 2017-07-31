using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapElement : MonoBehaviour {
	// Use this for initialization
	public Position p; //Please Initiate

	void Start () {
		
	}
	public abstract bool ProcessTurn ();
	// Update is called once per frame

	public abstract bool isFree ();
	public abstract bool isPushable ();

}
