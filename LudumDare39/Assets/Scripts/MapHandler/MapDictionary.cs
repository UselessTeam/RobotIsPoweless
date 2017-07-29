using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDictionary : MonoBehaviour {
	
	static public MapDictionary m_Instance;
	static public MapDictionary instance { get { return m_Instance; } }

	static Dictionary<char,MapItem> charToItem;

	public MapItem[] items;

	void Awake () {
		if (m_Instance != null) {
			Destroy (this);
		} else {
			m_Instance = this;
		}
	}

	public void generate() {
		charToItem = new Dictionary<char,MapItem> ();
		foreach (MapItem item in this.items) {
			charToItem.Add (item.code, item);
		}
	}
	public MapItem get(char key){
		MapItem output = null;
		if( ! charToItem.TryGetValue(key, out output )  )
			Debug.Log ("Erreur dans la map, character non reconnu : " + key);
		return output;
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
