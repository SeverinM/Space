using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ModifierScore : MonoBehaviour {
	private int score;

	// Use this for initialization
	void Start () {
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Text> ().text = System.Convert.ToString (score);

	}

	public void AugmenterScore(int nb){
		score += nb;
	}

	public string getScore(){
		return System.Convert.ToString(score);
	}
}
