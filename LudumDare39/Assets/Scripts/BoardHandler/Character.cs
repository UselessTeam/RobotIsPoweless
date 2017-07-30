using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Movement {
	

	// Use this for initialization
	void Start () {
		
	}

	public bool Move (Position destination){
		bool output = GetComponent<Movement> ().MoveTo (destination);
		if (!output) {
			output = PushTo (destination);
		}
		return output;
	}
	public bool PushTo(Position destination){
		return false; //TODO
	}


}
