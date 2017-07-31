using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicLink : MonoBehaviour {

	bool isActive = false;
	public bool IsActive(){	return isActive;	}

	public Position itemPosition = new Position(0,0);

	public void Change(){
		isActive = !isActive;
	}
}
