using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	public bool pushable;

	public bool MoveTo (Position i){
		if (!getPosition ().IsNeighbor (i)) {
			Debug.Log ("Nope! Deplacé à une position non adjascente");
			return false; // Ce false mene a d'autres bugs. Fais gaffe!
		} else if (BoardHandler.instance.FreeTile (i)) {
			setPosition (i);
			return true;
		}
		return false;
	}

	private Position getPosition (){
		return GetComponent<MapElement> ().p;
	}
	private void setPosition (Position newP){
		GetComponent<MapElement> ().p = newP;
	}




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
