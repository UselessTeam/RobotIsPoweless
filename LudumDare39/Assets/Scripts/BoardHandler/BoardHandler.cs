using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHandler : MonoBehaviour {

	static public BoardHandler instance;

	public Position size;
	public bool[,] topographyMap;//TODO
//	public Pushable[,] pushableTiles;//TODO
	public Dictionary<Position,MapElement> elementAt;

//	private Character character;//TODO
	private MapElement[] mapElements;//TODO



	void Awake() {
		instance = this;
	}

	void Start(){

	}

	void InitiateDictionary(){
	//	for mapElements
		//TODO
	}

	void NewTurn(){
		foreach (MapElement element in mapElements) {
			element.ProcessTurn ();
		}
		//TODO
	}

	public bool FreeTile(Position u){
		if (u.i >= 0 && u.i < size.i && u.j >= 0 && u.j < size.j) {
			MapElement element;
			if (elementAt.TryGetValue (u, out element)) {
				return 	topographyMap [u.i, u.j] && element.isFree();
			}
			return 	topographyMap [u.i, u.j];
		}
		return false;
	}
//	public bool PushableTile(Position u){
//		if (u.i >= 0 && u.i < size.i && u.j >= 0 && u.j < size.j)
//			return pushableTiles [u.i, u.j];
//		return false;
//	}

	public int[,] GiveEmptyMap(int fill){
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