using UnityEngine;
using System.Collections;

public class BossGris : BaseEnnemi {

	private bool avance;
	private LineRenderer line;
	private float frequence;
	private float nombreTir;
	private float temps;
	private bool pret;
	public GameObject balle;
	System.Collections.Generic.List<Vector2> liste;

	// Use this for initialization
	void Awake () {
		liste = new System.Collections.Generic.List<Vector2> ();
		frequence = 2f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.fixedTime - temps >= frequence) {
			temps = Time.fixedTime;
		}
	}

	void Preparer(){
		for (int i = 0; i < nombreTir; i++) {
			Vector2 vec = new Vector2 (transform.position.x - GameObject.Find ("Joueur").transform.position.x, transform.position.y - GameObject.Find ("Joueur").transform.position.y);
			line = new LineRenderer ();
			line.material = (Material)UnityEngine.Resources.Load ("Material/Sprites-Default.mat"); 
			line.SetPosition (0, new Vector3 (transform.position.x, transform.position.y, 0));
			line.SetPosition (1, new Vector3 (transform.position.x + (vec.x * 1000), transform.position.y + (vec.y * 1000)));
			line.SetColors (Color.red, Color.red);
			line.SetWidth (0.1f, 0.1f);
			liste.Add (vec);
		}
		pret = true;
	}

	void Tirer(){
		foreach (LineRenderer line in gameObject.GetComponents<LineRenderer>())
			Destroy (line);

		foreach (Vector2 vec in liste) {
			GameObject gob = (GameObject)Instantiate (balle, transform.position, new Quaternion ());
			gob.GetComponent<BaseProjectile> ().setDirection (vec);
			gob.GetComponent<BaseProjectile> ().setDegat (1);
			gob.GetComponent<BaseProjectile> ().setVitesse (0.08f);
		}

		pret = false;
	}
}
