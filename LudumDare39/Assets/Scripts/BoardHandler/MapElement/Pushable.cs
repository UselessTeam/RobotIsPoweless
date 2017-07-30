using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MapElement {
	override public bool isFree(){return false;}
	override public bool isPushable(){return true;}
	// Use this for initialization
//	public bool PushTo (Position i){
//		if (! GetComponent<Movement>().p.IsNeighbor (i)) {
//			Debug.Log ("Deplace à une position non adjascente");
//			return false; // Ce false mene a d'autres bugs. Fais gaffe!
//		} else if (BoardHandler.instance.PushableTile (i)) {
//	//		return GetComponent<Movement>().MoveTo();
//		}
//		return false;
//	}

	override public bool ProcessTurn (){
		return true;
	}

		
}
