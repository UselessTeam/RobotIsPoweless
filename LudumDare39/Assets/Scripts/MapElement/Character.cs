using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MapElement {
	
	override public bool isFree(){return false;}
	override public bool isPushable(){return false;}

	public LogicLink levelEndLink;


	public bool Move (Position destination){
		bool output = GetComponent<Movement> ().MoveTo (destination);
		if (!output) {
			//Try to PUSH
			Pushable element = BoardHandler.instance.PushableTile(destination);
			if (! (element == null) ) {
				element.GetComponent<Movement> ().MoveTo (getNextPos (element.p));
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
		return Move (getNextPos (p));
	}

	Position getNextPos(Position p){
		return p.Add( InputHandler.instance.direction );
	}
}
