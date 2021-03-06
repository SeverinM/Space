using UnityEngine;
using System.Collections;

public class GUI_Highscore : MonoBehaviour {

	public GUIStyle test;
	private string etat = "Saisie";
	private string pseudo = "";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){
		GUI.Box (new Rect (400,200, 700, 500), "Score");
		GUI.Box (new Rect (400,200, 700, 500), "Score");
		GUI.Box (new Rect (400,200, 700, 500), "Score");


		if (GUI.Button (new Rect (400, 200, 20, 20), "X")) {
			GameObject.Find ("Button").GetComponent<UnityEngine.UI.Button> ().interactable = true;
			GameObject.Find ("Button2").GetComponent<UnityEngine.UI.Button> ().interactable = true;
			GameObject.Find ("Button3").GetComponent<UnityEngine.UI.Button> ().interactable = true;
			this.gameObject.SetActive (false);
		}

		switch (etat) {
		case "Saisie":
			saisie ();
			break;

		case "ScoreGlobal":
			ScoreGlobal();
			break;
		}
	}

	void saisie(){
		GUI.Label (new Rect (650, 300, 200, 30), "Saisissez votre pseudo");
		pseudo = GUI.TextField (new Rect (650,400, 200, 30), pseudo);
		if (GUI.Button (new Rect (650, 500, 200, 30), "Enregistrer") && pseudo != "" && pseudo.Length < 10 && !(pseudo.Contains("/")) && !(pseudo.Contains(";"))) {
			Variables.pseudo = pseudo;
			GameObject.Find ("Interface").GetComponent<InterfaceMySQL1> ().PosterScore ();
			GameObject.Find ("Interface").GetComponent<InterfaceMySQL1> ().GetClassement ();
			etat = "ScoreGlobal";
		}
	}

	void ScoreGlobal(){
		GUI.Label (new Rect (420, 250, 30, 30), "#");
		GUI.Label (new Rect (600, 250, 100, 30), "Pseudo");
		GUI.Label (new Rect (500, 250, 100, 30), "Score");
		GUI.Label (new Rect (700, 250, 100, 30), "Temps");
		GUI.Label (new Rect (800, 250, 100, 30), "Réalisé le :");

		for (int i = 1; i < 11; i++) {
			GUI.Label (new Rect (420, 250 + (i * 35), 50, 30), System.Convert.ToString (i));
		}

		int x = 0;
		int y = 0;

		foreach (string str in Variables.ChaineScore.Split(';')){
			x = 0;
			y += 1;
			foreach (string str2 in str.Split('/')){
				GUI.Label (new Rect (500 + (x * 100), 250 + (y * 35), 200, 30), str2);
				x += 1;
		}
	}

		Variables.score = GameObject.Find ("Score").GetComponent<ModifierScore> ().getScore ();
		Variables.temps = GameObject.Find ("Temps").GetComponent<Chrono> ().getTemp ();

		GUI.Label (new Rect (420, 635, 100, 30), Variables.position,test);
		GUI.Label (new Rect (600, 635, 100, 30), Variables.pseudo,test);
		GUI.Label (new Rect (500, 635, 100, 30), Variables.score,test);
		GUI.Label (new Rect (700, 635, 100, 30), Variables.temps,test);
		GUI.Label (new Rect (800, 635, 100, 30), "A l'instant",test);

		GUI.Label (new Rect (600, 650, 200, 30), Variables.nbJoueur + " scores enregistré");
}

	void Rien(int id){
	}
}
