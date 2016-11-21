using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Chrono : MonoBehaviour {
	private float temps;
	private string minutes;
	private string secondes;
	private string centiemes;
	private bool pause;

	// Use this for initialization
	void Start () {
		pause = false;
		temps = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (!(pause)) {
			temps = Time.timeSinceLevelLoad;
			minutes = Convert.ToString ((int)(temps / 60));
			secondes = Convert.ToString ((int)(temps % 60));
			if ((int)(temps % 60) < 10)
				secondes = "0" + secondes;
			centiemes = Convert.ToString (-((int)temps - temps));
			if (centiemes.Length >= 4)
				centiemes = centiemes.Substring (2, 2);
		
			GetComponent<Text> ().text = minutes + " : " + secondes + ":" + centiemes;
		}
	}

	public void Pause(){
		pause = true;
	}

	public string getMinutes(){
		return minutes;
	}

	public string getSecondes(){
		return secondes;
	}

	public string getCentiemes(){
		return centiemes;
	}

	public string getTemp(){
		return minutes + ":" + secondes + ":" + centiemes;
	}
}
