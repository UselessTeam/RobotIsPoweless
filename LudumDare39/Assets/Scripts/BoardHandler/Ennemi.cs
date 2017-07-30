﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi : MonoBehaviour {
	

	List<Position> checkPoints;//TODO
	List<Position> chemin;

	void Start () {
		Position p = this.GetComponent<Movement> ().p;
		MapHandler.instance.generate ();
		checkPoints = new List<Position> ();
		chemin = MAJChemin (p,new Position (2, 2));//TODO
	}

	public bool Move (){
		bool output = GetComponent<Movement> ().MoveTo (chemin [0]);
		chemin.RemoveAt (0);
		if (chemin [0].Equals(checkPoints[0]) ){
			Position current = checkPoints [0];
			checkPoints.RemoveAt (0);
			checkPoints.Add (current);
			MAJChemin (current, checkPoints [0]);
		}
		return output;
	}

	List<Position> MAJChemin(Position p, Position objectif){
		List<Position> cheminOutput = new List<Position>(); 

		int[,] d = BoardHandler.instance.GiveEmptyMap (1000);
		int[,] visited = BoardHandler.instance.GiveEmptyMap (0);
		Position s = MapHandler.instance.size;
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
							print ("test Maj chemin " + cheminOutput.Count);
							return cheminOutput;
						}
					}
				}
			}
		}
		return null; //Bloké

	}


}