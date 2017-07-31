using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MapElement {

	override public bool isFree(){return true;}
	override public bool isPushable(){return true;}


	override public bool ProcessTurn (){
		return true; //TODO
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
