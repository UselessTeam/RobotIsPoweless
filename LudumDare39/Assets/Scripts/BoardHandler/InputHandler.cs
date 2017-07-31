using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

	static public InputHandler instance;
	void Awake() {	instance = this;	}

	private bool newTurn = false;

	public Position direction = new Position (0, 0);

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Up")) {
			direction = new Position (-1, 0);
			newTurn = true;
		}
		if (Input.GetButtonDown ("Down")) {
			direction = new Position (1, 0);
			newTurn = true;
		}
		if (Input.GetButtonDown ("Left")) {
			direction = new Position (0, -1);
			newTurn = true;
		}
		if (Input.GetButtonDown ("Right")) {
			direction = new Position (0, 1);
			newTurn = true;
		}
		if (Input.GetButtonDown ("Wait")) {
			direction = new Position (0, 0);
			newTurn = true;
		}

		if (newTurn) {
			BoardHandler.instance.NewTurn();
			print ("hi");
			newTurn = false;
		}

	}
}
