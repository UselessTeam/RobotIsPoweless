using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGUI : MonoBehaviour {

	private SpriteRenderer spriteRenderer;

	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	public void Set(int i){
		spriteRenderer.sprite = BoardHandler.instance.energies [i];
	}
}
