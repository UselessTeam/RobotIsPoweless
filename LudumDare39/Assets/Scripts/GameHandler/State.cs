using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour {

	void Start(){
		GameHandler.instance.SetMState(this);
	}

	public virtual void Launch () {}

	public virtual void Stop () {}
}
