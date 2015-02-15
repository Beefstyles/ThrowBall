using UnityEngine;
using System.Collections;

public class RoundCounter : MonoBehaviour {

	static public int p1WinCounter;
	static public int p2WinCounter;
	static public int roundNoCounter;
	static public int roundNoMax;

	void Start(){
		DontDestroyOnLoad (this.gameObject);
		roundNoMax = 5;
	}

	public void Restart(){
		p1WinCounter = 0;
		p2WinCounter = 0;
		roundNoCounter = 0;
		Application.LoadLevel ("mainScene");
	}

	public void ExitToMainMenu(){
		Application.LoadLevel ("mainMenu");
	}
	

}
