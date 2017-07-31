using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MapElement {
	
	override public bool isFree(){return false;}
	override public bool isPushable(){return false;}

	public LogicLink levelEndLink;

	public int energyMax = 4; //TODO Define this variable
	public int energy;

	void Start() {
		energy = energyMax;
	}

	public bool Move (Position destination){
		bool output = GetComponent<Movement> ().MoveTo (destination);
		if (!output) {
			//Try to PUSH
			Pushable element = BoardHandler.instance.PushableTile(destination);
			if (! (element == null) ) {
				element.GetComponent<Movement> ().MoveTo (getNextPos (element.p));
				output = true;
			}

		}
		if (p.Equals (levelEndLink.itemPosition)) {
			NextLevel ();
		}
		return output;
	}

	private void NextLevel(){
		GameHandler.instance.NextLevel ();
	}

	override public bool ProcessTurn (){
		if (Move (getNextPos (p))) {
			energy -= 1;
		}
		if (BoardHandler.instance.IsThere("power", p)){
			energy = energyMax;
		}
		if (energy == 0) {
			//TODO GAMEOVER
		}
		return true;
	}

	Position getNextPos(Position p){
		return p.Add( InputHandler.instance.direction );
	}
}
