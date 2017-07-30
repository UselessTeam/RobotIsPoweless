using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.IO;
using System;
using System.Linq;

public class MapLoaderScript : MonoBehaviour {

	void Awake(){
		Load ("map_test1");
	}

	[Serializable]
	public class TileSheet {
		public string name;
		public int start;
		public int height;
		public int width;

		public bool[] blockings;
		public bool[] passages;
		public Sprite[] sprites;

		public Item GetItem(int i){
			int j = i;//(i % height) * width + (i / height);
			Item item;
			item.blocking = j<blockings.Length ? blockings [j] : false ;
			item.passage = j<passages.Length ? passages [j] : false ;
			item.sprite = j<sprites.Length ? sprites [j] : null;
			return item;
		}
	}

	[Serializable]
	public struct Item {
		public Sprite sprite;
		public bool blocking;
		public bool passage;
	}


	string tmxName;
	XmlReader xml;

	public GameObject basicTile;

	[HideInInspector]
	public int width;
	[HideInInspector]
	public int height;
	public bool[,] logicMap;
	[HideInInspector]
	public GameObject[] entities;

	public TileSheet[] tilesheets;
		
	private TileSheet GetTilesheet(int i){
		int pos = 0;
		TileSheet cur = tilesheets[0];
		foreach (TileSheet sheet in tilesheets) {
			if (sheet.start <= i && sheet.start > pos) {
				pos = sheet.start;
				cur = sheet;
			}
		}
		return cur;
	}

	private Item GetItem(int i){
		TileSheet tilesheet = GetTilesheet (i);
		int num = i - tilesheet.start;
		Item item = tilesheet.GetItem (num);
		return item;
	}

	private void LoadLayer(){
		string name = xml.GetAttribute ("name");
		Transform layer = transform.Find (name);
		xml.Read ();
		xml.IsStartElement ();
		string content = xml.ReadInnerXml();

		int[,] imap = new int[height, width];
		if (logicMap == null) {
			logicMap = new bool[height, width];
		}

		string[] lines = content.Split ('\n');
		for (int i = 0; i < height; i++) {
			string[] elements = lines [i + 1].Split (',');
			for (int j = 0; j < width; j++) {
				int value = int.Parse(elements [j]);
				if (value == 0) { 
					continue;
				}
				Item item = GetItem (value);

				GameObject tile = Instantiate (basicTile, new Vector3 (32*j,-24*i,i), Quaternion.identity, layer);
				tile.GetComponent<SpriteRenderer> ().sprite = item.sprite;
				logicMap [i, j] = !item.blocking && (item.passage || logicMap [i, j]);
				imap [i, j] = value;
			}
		}



		xml.Skip ();
	}

	private void LoadDimentions(){
		height = int.Parse(xml.GetAttribute ("height"));
		width = int.Parse(xml.GetAttribute ("width"));
	}

	private TileSheet tilesheetNamed(string s){
		foreach (TileSheet sheet in tilesheets) {
			if (sheet.name == s) {
				return sheet;
			}
		}
		Debug.AssertFormat(false,"Error, tileset named {0} was not found",s);
		return tilesheets [0];
	}

	private void LoadTileset(){
		string[] parts = xml.GetAttribute ("source").Split('/');
		string name = parts [parts.Length-1].Split ('.') [0];
		int value = int.Parse (xml.GetAttribute ("firstgid"));
		tilesheetNamed(name).start = value;
	}

	private void LoadObjects(){
		xml.Skip ();
	}

	public void Load(string tmxName){
		tmxName = Application.dataPath + "/Maps/Levels/" + tmxName + ".tmx";
		xml = XmlReader.Create (tmxName);

		int n = 0;
		while (!xml.EOF && n<1000) {
			n++;
			if (!xml.IsStartElement ()) {
				continue;
			}
			switch (xml.LocalName.ToLower ()) {
			case "map":
				LoadDimentions ();
				break;
			case "tileset" :
				LoadTileset ();
				break;
			case "layer" :
				LoadLayer ();
				break;
			case "objectgroup" :
				LoadObjects ();
				break;
			default :
				Debug.LogWarningFormat ("Error loading map line {0}", xml.LocalName);
				xml.Skip ();
				break;
			}
			xml.Read ();
		}
		Debug.Log (tilesheets);
	}
}
