using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.IO;
using System;
using System.Linq;

public class MapLoaderScript : MonoBehaviour {

	public GameObject[] mapElementPrefabs;
	public GameObject linkPrefab;

	[Serializable]
	public class IntSet : List<int> {};

	[Serializable]
	public class TileSheet {
		public string name;
		[HideInInspector]
		public int start;

		public int[] blockings;
		public int[] passages;
		public Sprite[] sprites;

		public Item GetItem(int i){
			Item item;
			item.blocking = blockings.Contains(i);
			item.passage = passages.Contains(i);
			item.sprite = i<sprites.Length ? sprites [i] : null;
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
	public List<MapElement> entities;

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

				GameObject tile = CreateTile (item.sprite, layer, i, j);
				tile.name = name + String.Format("({0},{1})",i,j);
				logicMap [i, j] = !item.blocking && (item.passage || logicMap [i, j]);
				imap [i, j] = value;
			}
		}
		xml.Skip ();
	}

	public GameObject CreateTile(Sprite sprite, Transform parent, int i, int j, int push=0, GameObject model = null){
		if (model == null) {
			model = basicTile;
		}
		GameObject tile = Instantiate (model, new Vector3 (j, -0.75f * i, i + push), Quaternion.identity, parent); //Transofrm
		SpriteRenderer sr = tile.GetComponent<SpriteRenderer> ();
		sr.sprite = sprite;
		sr.sortingOrder = i + (int)(parent.position.z) + push;
		return tile;
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

	private bool NextContent(){
		if (!xml.Read ()) {
			return false;
		}
		xml.MoveToContent ();
		return true;
	}
		
	//VERY IMPORTANT TODO 


	private GameObject CreateLink(Transform layer){
		GameObject link = CreateTile (null,layer,0,0,2,linkPrefab); //Initialize Logic Link
		return link;
	}


	private void CreateLogic(Transform layer, string type, int gid, int i, int j, Dictionary<string,string> properties){
		GameObject tile;
		switch (type) {
		case "character":
			tile = CreateTile (GetItem (gid).sprite, layer, i, j, 0, mapElementPrefabs [0]);
			break;
		case "ennemy": //And ennemy
			string[] strPositions = properties ["path"].Split ('-');
			List<Position> checkPoints = new List<Position>();
			for (int k = 0; k < strPositions.Length; k++) {
				string[] strPair = strPositions [k].Substring (1, strPositions [k].Length - 2).Split (',');
				checkPoints.Add( new Position (int.Parse (strPair [1]), int.Parse (strPair [0]))  );
			}
			tile = CreateTile (GetItem (gid).sprite, layer, i, j, 0, mapElementPrefabs [1]);
			tile.GetComponent<Ennemi> ().checkPoints = checkPoints;
			break;
		case "power": //The thing that gives you back energy
			tile = CreateTile (GetItem (gid).sprite, layer, i, j, 0, mapElementPrefabs [2]);
			break;
		case "button+": //Button  
		case "button": //Button  //TODO Diff entre les boutons
			tile = CreateTile (GetItem (gid).sprite, layer, i, j, 0, mapElementPrefabs [3]);
			LogicButton logicButton = tile.GetComponent<LogicButton>();
			logicButton.link = layer.GetComponent<LogicLink> ();
			break;
		case "pillar": //The thing that comes out of the ground and acts like a wall when it's activated 
			tile = CreateTile (GetItem (gid).sprite, layer, i, j, 0, mapElementPrefabs [4]);
			LogicDoor logicPillar = tile.GetComponent<LogicDoor>();
			logicPillar.link = layer.GetComponent<LogicLink> ();
			break;
		case "wire": //The blue thing on the ground
			tile = CreateTile (GetItem (gid).sprite, layer, i, j, 0, mapElementPrefabs [5]);
			Wire logicWire = tile.GetComponent<Wire>();
			logicWire.link = layer.GetComponent<LogicLink> ();
			break;
		case "pushable": //The blue thing on the ground
			tile = CreateTile (GetItem (gid).sprite, layer, i, j, 0, mapElementPrefabs [6]);
			break;
		case "default":
		default:
			tile = CreateTile (GetItem (gid).sprite, layer, i, j); //Initialize Logic Item
			Debug.Log( "Un objet est cree mais ne correspond à rien" );
			break;
		}
		tile.name = type + String.Format("({0},{1})",i,j);
		MapElement logicElement = tile.GetComponent<MapElement>();
		logicElement.p = new Position (i, j);
		entities.Add ( tile.GetComponent<MapElement>());
	}

	private void UpdateProperties(ref Dictionary<string,string> properties){
		NextContent ();//First property
		while (xml.NodeType != XmlNodeType.EndElement) {
			string proprietyName = xml.GetAttribute ("name");
			string proprietyValue = xml.GetAttribute ("value");
			properties [proprietyName] = proprietyValue;
			NextContent ();//Next property
		}
	}

	private void LoadObjects(){
		string name = xml.GetAttribute ("name");
		Dictionary<string,string> groupProperties = new Dictionary<string,string> ();
		groupProperties ["type"] = "default";

		Transform layer = transform.Find ("Entities");
		GameObject logic;
		if (name == "Link") {
			logic = CreateLink (layer);
		} else {
			logic = CreateTile (null,layer,0,0,2);
		}
		logic.name = name;
		NextContent ();
		//NextContent ();
		while (xml.NodeType!=XmlNodeType.EndElement) {
			if (xml.Name == "properties") {
				UpdateProperties (ref groupProperties);
			} else if (xml.Name == "object") {
				string type = xml.GetAttribute("type");
				if(type==null || type==""){
					type=groupProperties ["type"];
				}
				Dictionary<string,string> properties = new Dictionary<string,string>();
				int i = int.Parse (xml.GetAttribute ("y")) / 24 - 1;
				int j = int.Parse (xml.GetAttribute ("x")) / 32;
				int gid = int.Parse (xml.GetAttribute ("gid"));
				//If has properties
				if (!xml.IsEmptyElement) {
					xml.ReadToDescendant ("properties");
					UpdateProperties (ref properties);
					NextContent ();//Exit properties
				}
				CreateLogic (logic.transform, type, gid, i, j, properties);
			}
			NextContent ();//Next logic element
		}
		//Finishes on end element
	}

	public void Load(string tmxName){
		tmxName = Application.dataPath + "/Maps/Levels/" + tmxName + ".tmx";
		xml = XmlReader.Create (tmxName);

		int n = 0;
		xml.MoveToContent ();
		while (!xml.EOF && n<1000) {
			n++;
			switch (xml.Name.ToLower ()) {
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
				NextContent ();
				break;
			}
			do {
				NextContent ();
			} while (xml.NodeType == XmlNodeType.EndElement);
		}
		Debug.Log (n);
	}
}
