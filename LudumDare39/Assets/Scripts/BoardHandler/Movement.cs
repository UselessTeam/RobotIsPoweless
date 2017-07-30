using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	public Position p = new Position(0,0);

	public bool MoveTo (Position i){
		if (!p.IsNeighbor (i)) {
			Debug.Log ("Deplace à une position non adjascente");
			return false; // Ce false mene a d'autres bugs. Fais gaffe!
		} else if (BoardHandler.instance.FreeTile (i)) {
			p = i;
			return true;
		}
		return false;
		//TODO
	}




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
