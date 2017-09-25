using UnityEngine;
using System.Collections;

public class Recommencer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Restart(){
		GameObject.Find ("Musique").GetComponent<AudioSource> ().volume = 1;
		UnityEngine.SceneManagement.SceneManager.LoadScene("niveau");
	}
}
