using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDictionary : MonoBehaviour {
	
	static public Dictionary<char,MapItem> m_Instance;
	public MapItem[] items;

	static public Dictionary<char,MapItem> charToItem { get { return m_Instance; } }

	void Awake () {
		if (m_Instance != null) {
			Destroy (this);
		} else {
			m_Instance = new Dictionary<char,MapItem> ();
		}
	}

	public static void generate() {
		foreach (MapItem item in this.items) {
			m_Instance.Add (item.getCode (), item);
		}

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
