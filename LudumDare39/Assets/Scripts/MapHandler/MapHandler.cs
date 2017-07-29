using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour {

	static public MapHandler instance;
	void Awake() {
		instance = this;
	}

	public TextAsset file;


	public Position size;
	public MapItem[,] map;


	void Start () {
		//generate ();
	}

	public void generate(){
		MapDictionary.instance.generate();
		string theWholeFileAsOneLongString = file.text;
		string[] lines = theWholeFileAsOneLongString.Split('\n');
		size = new Position (lines.Length, 0);
		if (size.i == 0) {
			Debug.Log ("Texte vide, map non generee");
			return;
		}
		size.j = lines [0].Length - ( (size.i==1)?0:1 );
		map = new MapItem[size.i , size.j];
		for (int i = 0; i < size.i; i++){
			if (lines [i].Length - ( (size .i==i+1)?0:1 ) != size.j) {
				Debug.Log ("Errer dans la map, Lignes non egales, Map non generee");
				return;
			}
			for (int j = 0; j < size.j; j++){
				char test = lines [i] [j];
				map [i,j] = MapDictionary.instance.get (test);
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
