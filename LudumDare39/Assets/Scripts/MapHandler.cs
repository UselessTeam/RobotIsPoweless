using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour {

	public TextAsset file;


	private int[] size;
	private MapItem[][] map;


	void Start () {
		generate ();
	}

	void generate(){
		MapDictionary.generate();
		string theWholeFileAsOneLongString = file.text;
		string[] lines = theWholeFileAsOneLongString.Split("\n"[0]);
		size= new int[2];
		size [0] = lines.Length;
		size [1] = lines [0].Length;
		
		for (int i = 0; i < size[0]; i++){
			if (lines [i].Length != size [1]) {
				Debug.Log ("Errer dans la map, Lignes non egales, Map non generee");
				return;
			}
			for (int j = 0; j < size[1]; j++){
				if( MapDictionary.charToItem.TryGetValue(lines[i][j], out map[i][j]) )
					print (lines [i] [j]);
				else{
					Debug.Log ("Erreur dans la map, character non reconnu : " + lines [i] [j]);
					return;
				}
					
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
