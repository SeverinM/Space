using UnityEngine;
using System.Collections;

public class GUI_Window : MonoBehaviour {

	private bool fenetre1 = true;
	public GUIStyle test;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnGUI(){
		if (fenetre1)
			MyWindow ();
		else
			MyWindow2 ();
		
	}

	private void MyWindow(){
		if (GUI.Button (new Rect (20, 20, 30, 30), "->"))
			fenetre1 = false;
	}

	private void MyWindow2(){
		Variables.autoFire = GUI.Toggle (new Rect (40, 60, 250, 20), Variables.autoFire, "Tir auto");
		
		GUI.Box (new Rect (20, 20, 250, 150), "Options");

		Variables.sensiX = GUI.HorizontalSlider (new Rect (70, 100, 100, 20), Variables.sensiX, 0.01f, 0.07f);
		Variables.sensiY = GUI.HorizontalSlider (new Rect (70, 140, 100, 20), Variables.sensiY, 0.01f, 0.07f);
		GUI.Label (new Rect (20, 95, 100, 30), "Sensi X");
		GUI.Label (new Rect (20, 135, 100, 30), "Sensi Y");

		if (GUI.Button (new Rect (20, 20, 30, 30), "<-"))
			fenetre1 = true;
	}

	private void Rien(int id){}
}
