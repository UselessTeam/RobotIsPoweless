using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi : MonoBehaviour {
	
	Position p;
	List<Position> checkPoints;

	void Start () {
		MapHandler.instance.generate ();
		p = new Position (0, 0);
		checkPoints = new List<Position> ();
		MAJChemin (new Position (2, 2));
	}

	List<Position> MAJChemin(Position objectif){
		List<Position> cheminOutput = new List<Position>(); 

		int[,] d = BoardHandler.instance.giveEmptyMap (1000);
		int[,] visited = BoardHandler.instance.giveEmptyMap (0);
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
					if (BoardHandler.instance.freeTile (v) && visited[v.i,v.j]==0 ) {
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
		print ("triste");
		return null; //Bloké

	}


}
