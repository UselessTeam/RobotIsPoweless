using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicDoor : MapElement {

	bool closed = false;
	bool previousState = false;

	override public bool isFree(){return !closed;}
	override public bool isPushable(){return true;}

	public LogicLink link;

	override public bool ProcessTurn (){
		closed = link.IsActive();
		//it (previousState != closed) TODO afficher le changement d'etat Vas y quentin!!!
		previousState = closed;
		return true;
	}

}
