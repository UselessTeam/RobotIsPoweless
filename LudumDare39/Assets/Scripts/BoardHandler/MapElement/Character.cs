using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MapElement {
	
	override public bool isFree(){return false;}
	override public bool isPushable(){return false;}

	// Use this for initialization
	void Start () {
		
	}

	public bool Move (Position destination){
		bool output = GetComponent<Movement> ().MoveTo (destination);
		if (!output) {
			//Try to PUSH
			MapElement element;
			if (BoardHandler.instance.elementAt.TryGetValue (destination, out element)) {
				if (element.isPushable ()) {
					element.GetComponent<Movement> ().MoveTo (getNextPos (element.p));
				}
			}
		}
		return output;
	}

	new public bool ProcessTurn (){
		return true;
	}

	Position getNextPos(Position p){
		return p; //TODO
	}
}
