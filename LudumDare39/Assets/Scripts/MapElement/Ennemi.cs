using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi : MapElement {
	override public bool isFree(){return false;}
	override public bool isPushable(){return false;}

	public List<Position> checkPoints;
	List<Position> chemin;

	public int energyMax = 4; //TODO Define this variable
	public int energy;

	void Start () {
		MAJChemin ();
		energy = energyMax;
	}

	override public bool ProcessTurn (){
		//Movement debugMov = GetComponent<Movement> ();
		if (energy > 0) {
			energy  -= 1;
			bool output = true;
			if (chemin != null) {
				output = GetComponent<Movement> ().MoveTo (chemin [0]);
				chemin.RemoveAt (0);
				if (chemin [0].Equals (checkPoints [0])) {
					Position current = checkPoints [0];
					checkPoints.RemoveAt (0);
					checkPoints.Add (current);
					MAJChemin ();
				}
			}
			if (BoardHandler.instance.IsThere("power", p)){
				energy = energyMax;
			}
			return output;
		} else {
			//TODO Eventuellement que faire si je n'ai plus d'energie?
			return false;
		}

	}

	void MAJChemin(){
		Position objectif = checkPoints [0];
		List<Position> cheminOutput = new List<Position>(); 
		int[,] d = BoardHandler.instance.GiveEmptyMap (1000);
		int[,] visited = BoardHandler.instance.GiveEmptyMap (0);
		Position s = BoardHandler.instance.size;
		Position[,] last = new Position[s.i,s.j];
		d[p.i,p.j] = 0;

		List<Position> Q = new List<Position>();
		Q.Add(p);
		while (Q.Count != 0) {
			Position u = Q[0];
			Q.Remove (u);
			if (visited[u.i,u.j]==0) {
				visited [u.i,u.j] = 1;
				foreach (Position v in u.Voisins()) {
					if (BoardHandler.instance.FreeTile (v) && visited[v.i,v.j]==0 ) {
						d [v.i, v.j] = Mathf.Min (d [v.i, v.j], d [u.i, u.j] + 1);
						Q.Add (v);
						last [v.i, v.j] = u;

						if ( v.Equals(objectif) ) {
							//ON TERMINE TOUT
							u = v;
							while (!u.Equals (p)) {
								cheminOutput.Add (u);
								u = last [u.i, u.j];
							}
							cheminOutput.Reverse ();
							chemin = cheminOutput;
							return;
						}
					}
				}
			}
		}
		Debug.Log ("Chemin Bloqué");
		return; //Bloké TODO

	}


}
