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

	public int level = 0;


	void Awake() {
		instance = this;
		LoadLevel ("map_test1");
	}

	public void LoadLevel(string str){
		mapLoader.Load (str);
		topographyMap = mapLoader.logicMap;
		size = new Position (mapLoader.height, mapLoader.width);
		mapElements = mapLoader.entities.ToArray ();
		InitiateDictionary ();
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

	public bool FreeTile(Position u){ 
		if (u.i >= 0 && u.i < size.i && u.j >= 0 && u.j < size.j) {
			List<MapElement> elementList;
			bool free = true;
			if (elementAt.TryGetValue (u, out elementList)) {
				foreach (MapElement el in elementList) {
					free = free && el.isFree ();
				}
				if (elementList.Count == 2) {
				}
			}
			return 	( topographyMap [u.i, u.j] && free );
		}
		return false;
	}

	public void Move(MapElement element, Position newPos){
		//First Remove
		List<MapElement> elementList;
		if (!elementAt.TryGetValue (element.p, out elementList)) {
			Debug.Log ("Probleme, l'objet à deplacer etait a une position vide");
		}else if(!elementList.Contains(element)){
			Debug.Log ("Probleme, l'objet à deplacer n'y etait pas");
		}
		elementList.Remove (element);
		//THen ADD at the new position
		if ( elementAt.TryGetValue(newPos , out elementList) ) {
			elementList.Add (element);
		} else {
			elementList = new List<MapElement> ();
			elementList.Add (element);
			elementAt.Add (newPos, elementList);
		}

	}

	public Pushable PushableTile(Position u){ 	//0 means not accessible	//1 means pushable	//2 means free!
		Pushable output = null;
		if (u.i >= 0 && u.i < size.i && u.j >= 0 && u.j < size.j) {
			List<MapElement> elementList;
			if (elementAt.TryGetValue (u, out elementList)) {
				foreach (MapElement el in elementList) {
					if (elementList [0].isPushable ()) {
						output = elementList [0].GetComponent<Pushable>();
					}
				}
			}
		}
		return output;
		//TODO
	}

	public bool IsThere (string type, Position u){
		List<MapElement> elementList;
		bool power = false;;
		if (elementAt.TryGetValue (u, out elementList)) {
			foreach (MapElement el in elementList) {
				power = power || el.name.Contains(type);
			}
			return power;
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
		return Voisins ().Contains (other) || Equals(other);
	}

	public Position Add (Position other){
		return new Position (i + other.i, j + other.j);
	}

	override public string ToString () {
		return (i + " " + j);
	}
}