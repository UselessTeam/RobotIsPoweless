using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {
	
	static private GameHandler m_Instance;
	static public GameHandler instance { get { return m_Instance; } }

	private State m_state;
	public State state{get{return m_state;}}
	public int curState;
	public string[] states;
	public SoundHandler soundHandler;

	private int level = 0;
	public void NextLevel(){
		level++;
		BoardHandler.instance.LoadLevel ("map_level" + level);//TODO Donner un nom aux map de niveau
	}//CECI EST UN TEST (cette methode est utilisée par character











	void Awake(){
		DontDestroyOnLoad (transform.gameObject);
		if (m_Instance != null) {
			Destroy (this.gameObject);
		} else {
			m_Instance = this;
		}
	}

	public void SetState(string name){
		for (int k=0; k<states.Length;k++) {
			if (name == states[k]) {
				curState = k;
				StartState ();
			}
		}
	}

	void StartState(){
		if (m_state != null) {
			m_state.Stop ();
		}
		SceneManager.LoadScene (states [curState]);
	}

	public void SetMState(State s){
		m_state = s;
	}



}
