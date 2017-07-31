using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MapElement {
	
	override public bool isFree(){return false;}
	override public bool isPushable(){return false;}

	public static Character instance;

	public LogicLink levelEndLink;

	public int energyMax = 4;
	public int energy;

	void Awake(){
		instance = this;
	}

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
		return output;
	}

	private void NextLevel(){
		GameHandler.instance.NextLevel ();
	}

	override public bool ProcessTurn (){
		if (energy <= 0) {
			//TODO GAMEOVER
		} else if (Move (getNextPos (p)) && InputHandler.instance.direction != Position.directions[(int)Direction.CENTER]) {
			energy -= 1;
		}
		return true;
	}

	public void Recharge(){
		energy = energyMax;
	}

	Position getNextPos(Position p){
		return p.Add(InputHandler.instance.direction);
	}
}
