using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class MapLoaderScript {

	public string tmxName;
	XmlReader xml;

	public int width;
	public int height;
	public GameObject graphics;
	public MapItem[,] map;
	public GameObject[] entities;

	public Dictionary<string,int> tilesets;

	public MapLoaderScript(string name){
		tmxName = name;
		tilesets = new Dictionary<string,int>();
	}

	private void LoadLayer(){
		string name = xml.GetAttribute ("name");
		xml.Read ();

//		Debug.Log(xml.Name);
		string wut = xml.ReadInnerXml();

//		Debug.Log(wut);
		xml.Skip ();
	}

	private void LoadDimentions(){
		width = int.Parse(xml.GetAttribute ("width"));
		height = int.Parse(xml.GetAttribute ("height"));
	}

	private void LoadTileset(){
		string[] parts = xml.GetAttribute ("source").Split('/');
		string name = parts [parts.Length-1].Split ('.') [0];
		int value = int.Parse (xml.GetAttribute ("firstgid"));
		tilesets [name] = value;
	}

	private void LoadObjects(){

	}

	public void Load(){
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
//		Debug.Log (tilesets);
	}
}
