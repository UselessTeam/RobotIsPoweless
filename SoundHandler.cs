using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour {

	static private SoundHandler m_Instance;
	static public SoundHandler instance { get { return m_Instance; } }

	// Use this for initialization
	public Dictionary<string,AudioClip> musics = new Dictionary<string,AudioClip>();
	public Dictionary<string,AudioClip> sounds = new Dictionary<string,AudioClip>();
	public AudioClip[] musiques;
	public AudioClip[] sons;
	public AudioSource musicPlayer;

	private const int NUMBER_SOUNDS = 3;
	private int currentSound = 0;
	public AudioSource[] soundPlayers = new AudioSource[NUMBER_SOUNDS] ;

	void Awake () {
		if (m_Instance != null) {
			Destroy (this);
		} else {
			m_Instance = this;
		}
	}
	void Start(){
		foreach(AudioClip audioClip in musiques){
			musics.Add (audioClip.name, audioClip);
		}
		foreach(AudioClip audioClip in sons){
			sounds.Add (audioClip.name, audioClip);
		}

		/*for (int i = 0; i < NUMBER_SOUNDS; i++) {
			soundPlayers [i] = new AudioSource ();
		}*/
	}


	public void playMusic (string name){
		try{
		musicPlayer.clip = musics[name];
		musicPlayer.Play ();
		} catch (KeyNotFoundException e) {
			print (name + " notfound");
		}
	}

	public void playSound (string name){
		try {
			soundPlayers [currentSound].clip = sounds[name];
			soundPlayers [currentSound].Play ();
		} catch (KeyNotFoundException e) {
			print (name + " notfound");
		}
		//soundPlayers [currentSound].PlayOneShot (sounds[name]);
		currentSound = (currentSound + 1) % NUMBER_SOUNDS;
	}
}
