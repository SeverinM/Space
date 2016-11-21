using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VaisseauVert : BaseEnnemi {

	public float ecart;
	private float temps;
	private GameObject gob;
	public GameObject obj;
	private int nbproj;
	private float vitesseProj;
	protected float vitesse;


	// Use this for initialization
	void Awake () {
		transform.position = new Vector3 (Random.Range (-4.5f, 4.7f), 3f, 0);
		vitesse = Random.Range (0.3f, 1f) / 2;
		vie = 8;
		score = 7;
		nbproj = 20;
		vitesseProj = 0.015f;
		pvMax = vie;
	}

	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (0, -0.01f * vitesse, 0);
		if (Time.fixedTime - temps >= ecart) {
			temps = Time.fixedTime;
			// gob = (GameObject)(Instantiate (obj, transform.position, new Quaternion ()));
			gob = SimplePool.Spawn(obj, transform.position, new Quaternion ());

			ProjectileFrag proj = gob.GetComponent<ProjectileFrag> ();
			proj.setVitesse (vitesseProj);
			proj.setDirection (new Vector2 (0, -1));
			proj.setDuree (2);
			proj.setnbproj (nbproj);
		}
	}

	public override void Booster ()
	{
		base.Booster ();
		ecart /= 1.05f;
		vitesseProj *= 1.5f;
		nbproj += 15;
		score += 1;
		vie += 1;
		vitesse *= 1.2f;
	}
}
