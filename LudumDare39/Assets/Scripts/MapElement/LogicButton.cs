using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicButton: MapElement {

	override public bool isFree(){return true;}
	override public bool isPushable(){return true;}

	public LogicLink link;


	override public bool ProcessTurn (){
		List<MapElement> elementList;
		if (!BoardHandler.instance.elementAt.TryGetValue (p, out elementList)) {
			Debug.Log ("Erreur, l'interupteur se trouve à un emplacement vide selon le dictionnaire 'elmentAt' ");
			return false;
		}
		if (elementList.Count > 2) {
			link.Activate ();
		} else {
			link.Desactivate ();
		}
		return true;
	}



}
