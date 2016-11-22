using UnityEngine;
using System.Collections;

public class commencer : MonoBehaviour {

	public AudioClip battlemusic;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			GameObject.Find ("Musique").GetComponent<AudioSource> ().clip = battlemusic;
			GameObject.Find ("Musique").GetComponent<AudioSource> ().Play ();
			UnityEngine.SceneManagement.SceneManager.LoadScene ("niveau");
		}
	}
}
