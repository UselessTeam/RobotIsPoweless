using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicDoor : MapElement {

	bool activated = false;
	public bool defaultState = false;

	override public bool isFree(){return !activated;}
	override public bool isPushable(){return true;}

	public LogicLink link;

	void Start(){
		this.GetComponent<SpriteRenderer> ().sortingOrder++;
		ProcessTurn ();
	}

	override public bool ProcessTurn (){
		bool newState = (link.IsActive () != defaultState);
		if (activated != newState) {
			activated = newState;
			GetComponent<Animator> ().SetBool ("activated", activated);
		}
		return true;
	}

}
