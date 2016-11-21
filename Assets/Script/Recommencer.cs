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
		UnityEngine.SceneManagement.SceneManager.LoadScene("niveau");
	}
}
