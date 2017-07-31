using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHandler : MonoBehaviour {

	static public BoardHandler instance;

	public Position size;
	private bool[,] topographyMap;
//	public Pushable[,] pushableTiles;
	public Dictionary<Position,List<MapElement>> elementAt;

	public MapLoaderScript mapLoader;

//	private Character character;
	private MapElement[] mapElements;



	void Awake() {
		instance = this;
		mapLoader.Load ("map_test1");
		topographyMap = mapLoader.logicMap;
		size = new Position (mapLoader.height, mapLoader.width);
		mapElements = mapLoader.entities.ToArray();
		InitiateDictionary ();
	}

	void Start(){

	}

	void InitiateDictionary(){
		elementAt = new Dictionary<Position,List<MapElement>> ();
		foreach (MapElement element in mapElements) {
			List<MapElement> elementList;
			if ( elementAt.TryGetValue(element.p , out elementList) ) {
				elementList.Add (element);
			} else {
				elementList = new List<MapElement> ();
				elementList.Add (element);
				elementAt.Add (element.p, elementList);
			}
		}
	}

	public void NewTurn(){
		foreach (MapElement element in mapElements) {
			element.ProcessTurn ();
		}
		//TODO
	}

	public bool FreeTile(Position u){ 	//0 means not accessible	//1 means pushable	//2 means free!
		if (u.i >= 0 && u.i < size.i && u.j >= 0 && u.j < size.j) {
			List<MapElement> elementList;
			bool free = true;
			if (elementAt.TryGetValue (u, out elementList)) {
				while (elementList.Count > 0 && free) {
					free = free && elementList [0].isFree ();
					elementList.RemoveAt (0);
				}
			}
			return 	( topographyMap [u.i, u.j] && free );
		}
		return false;
	}

	public Pushable PushableTile(Position u){ 	//0 means not accessible	//1 means pushable	//2 means free!
		Pushable output = null;
		if (u.i >= 0 && u.i < size.i && u.j >= 0 && u.j < size.j) {
			List<MapElement> elementList;
			bool pushable = false;
			if (elementAt.TryGetValue (u, out elementList)) {
				while (elementList.Count > 0 && !pushable) {
					if (elementList [0].isPushable ()) {
						pushable = true;
						output = elementList [0].GetComponent<Pushable>();
					}
					elementList.RemoveAt (0);
				}
			}
		}
		return output;
		//TODO
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

	public Position Add (Position other){
		return new Position (i + other.i, j + other.j);
	}

	override public string ToString () {
		return (i + " " + j);
	}
}