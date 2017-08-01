using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicButton: MapElement {

	override public bool isFree(){return true;}
	override public bool isPushable(){return true;}

	public LogicLink link;
	public int n = 0;

	void Start(){
		this.GetComponent<SpriteRenderer> ().sortingOrder++;
	}

	override public bool ProcessTurn (){
		return true;
	}

	public override void SteppedOn(MapElement by){
		n++;
		if (n == 1) {
			link.Change ();
		}
	}

	/*
	public override void SteppedOff(MapElement by){
		n--;
		if (n == 0) {
			link.Change ();
		}
	}*/
}
