using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	public bool pushable;
	public Animator animator;
	private float cur;
	private Position oldPos;

	void Awake(){
		animator = GetComponent<Animator> ();
		cur = -1f;
	}
		
	private void Going(Direction d){
		if ((int)d >= 4) {
			return;
		}
		animator.SetInteger ("direction", (int)d);
		animator.SetTrigger ("moving");
		oldPos = getPosition ();
		cur = 0f;
	}

	void Update(){
		Position p = getPosition ();
		if (cur < 1f && cur >= 0f) {
			if (cur == 0f) {
				this.GetComponent<SpriteRenderer> ().sortingOrder = 4*(Mathf.Max(oldPos.i,p.i) + (int)(transform.parent.position.z)) + 2;
			}
			transform.position = new Vector3 (oldPos.j * (1f - cur) + p.j * cur, -0.75f * (oldPos.i * (1f - cur) + p.i * cur), p.i);
			cur += Time.deltaTime * 3f;
		} else if (cur >= 1f) {
			this.GetComponent<SpriteRenderer> ().sortingOrder = 4*(p.i + (int)(transform.parent.position.z)) + 1;
			transform.position = new Vector3 (p.j, -0.75f * p.i, p.i);
			cur = -1f;
		}
	}

	public bool MoveTo (Position i){
		if (!getPosition ().IsNeighbor (i)) {
			Debug.Log ("Nope! Deplacé à une position non adjascente");
			return false; // Ce false peut mener a d'autres bugs. Fais gaffe!
		}

		if (BoardHandler.instance.FreeTile (i)) {
			//Dire au board Handler ou je suis
			BoardHandler.instance.Move(this.GetComponentInParent<MapElement>(), i);

			//AFFicher le deplacement
			Going (getPosition().ToDirection(i));

			setPosition (i);

			//Dire au board Handler ou je suis
			return true;
		}
		return false;
	}

	private Position getPosition (){
		return GetComponent<MapElement> ().p;
	}
	private void setPosition (Position newP){
		GetComponent<MapElement> ().p = newP;
	}
}
