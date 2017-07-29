using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour {

	public TextAsset file;


	private int[] size;
	private MapItem[,] map;


	void Start () {
		generate ();
	}

	void generate(){
		MapDictionary.instance.generate();
		string theWholeFileAsOneLongString = file.text;
		string[] lines = theWholeFileAsOneLongString.Split('\n');
		size= new int[2];
		size [0] = lines.Length;
		if (size [0] == 0) {
			Debug.Log ("Texte vide, map non generee");
			return;
		}
		size [1] = lines [0].Length - ( (size [0]==1)?0:1 );
		map = new MapItem[size [0],size [1]];
		for (int i = 0; i < size[0]; i++){
			if (lines [i].Length - ( (size [0]==i+1)?0:1 ) != size [1]) {
				Debug.Log ("Errer dans la map, Lignes non egales, Map non generee");
				return;
			}
			for (int j = 0; j < size[1]; j++){
				char test = lines [i] [j];
				map [i,j] = MapDictionary.instance.get (test);
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
