using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapElement : MonoBehaviour {
	// Use this for initialization
	public Position p;

	void Start () {
		
	}
	public abstract bool ProcessTurn ();
	// Update is called once per frame

	public abstract bool isFree ();
	public abstract bool isPushable ();

	public virtual void SteppedOn (MapElement by) {}
	public virtual void SteppedOff (MapElement by) {}
}
