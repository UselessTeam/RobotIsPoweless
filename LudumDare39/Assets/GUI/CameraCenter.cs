using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCenter : MonoBehaviour {

	private Camera cam;

	void Awake(){
		cam = GetComponent<Camera> ();
	}

	void Update () {
		if (Character.instance == null) {
			return;
		}
		Position size = BoardHandler.instance.size;
		Vector3 characterPosition = Character.instance.transform.position;

		float aimX;
		float aimY;

		if(size.j<16){
			//center
			aimX = size.j/2;
		} else {
			//follow
			aimX = Mathf.Max(8,Mathf.Min(size.j-8,characterPosition.x));
		}

		if(size.i<16){
			//center
			aimY = size.i/2;
		} else {
			//follow
			aimY = Mathf.Max(8,Mathf.Min(size.i-8,-characterPosition.y/0.75f));
		}
		cam.transform.position = new Vector2 (aimX + 0.5f, -0.75f * (aimY + 0.5f));
	}
}
