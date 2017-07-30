using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHandler : MonoBehaviour {

	static public BoardHandler instance;

	static private Character character;//TODO
	static private Ennemi[] ennemis;//TODO


	void Awake() {
		instance = this;
	}

	void NewTurn(){
		foreach (Ennemi ennemi in ennemis) {
			ennemi.Move ();
		}
		//TODO
	}

	public bool FreeTile(Position u){
		if (u.i >= 0 && u.i < MapHandler.instance.size.i && u.j >= 0 && u.j < MapHandler.instance.size.j)
			return MapHandler.instance.map [u.i, u.j];
		return false;
	}

	public int[,] GiveEmptyMap(int fill){
		Position size= MapHandler.instance.size;
		int[,] array = new int[size.i,size.j];
		for (int i = 0; i < size.i; i++)
			for (int j = 0; j < size.j; j++)
				array [i,j] = fill;
		return array;
	}
		
}

public struct Position {
	public int i; 
	public int j;

	public Position(int i,int j){
		this.i = i;
		this.j = j;
	}

	public List<Position> Voisins (){
		List<Position> output = new List<Position> ();
		output.Add (new Position (i+1, j));
		output.Add (new Position (i-1, j));
		output.Add (new Position (i, j+1));
		output.Add (new Position (i, j-1));
		return output;
	}

	public bool Equals (Position other){
		return other.i==i && other.j==j;
	}

	public bool IsNeighbor( Position other){
		return Voisins ().Contains (other);
	}

	public string toString () {
		return (i + " " + j);
	}
}