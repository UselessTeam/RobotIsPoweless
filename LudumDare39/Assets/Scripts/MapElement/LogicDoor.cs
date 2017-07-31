using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicDoor : MapElement {

	bool closed = false;
	public bool defaultState = false;

	override public bool isFree(){return !closed;}
	override public bool isPushable(){return true;}

	public LogicLink link;

	override public bool ProcessTurn (){
		closed = (link.IsActive () != defaultState);

		return true;
	}

}
