using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicButton: MapElement {

	override public bool isFree(){return false;}
	override public bool isPushable(){return true;}

	void Awake(){
	//	transform.parent.GetComponent<LogicLink> ().Start ();
	}

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
	public LogicLink link;

	bool Activate(){
		return false; //TODO
	}
	bool Desactivate(){
		return false; //TODO
	}

}
