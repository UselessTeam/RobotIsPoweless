using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MapElement {

	override public bool isFree(){return false;}
	override public bool isPushable(){return true;}
	
	override public bool ProcessTurn (){
		List<MapElement> elementList;
		if (!BoardHandler.instance.elementAt.TryGetValue (p, out elementList)) {
			Debug.Log ("Erreur, l'interupteur se trouve à un emplacement vide selon le dictionnaire 'elmentAt' ");
			return false;
		}
		if (elementList.Count > 1) {
			Activate ();
		} else {
			Desactivate ();
		}
		return true;
	}
	public Logic logic;

	bool Activate(){
		return false; //TODO
	}
	bool Desactivate(){
		return false; //TODO
	}

}
