using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MapElement {

	override public bool isFree(){return true;}
	override public bool isPushable(){return false;}

	override public bool ProcessTurn (){
		return true; //TODO
	}
}
