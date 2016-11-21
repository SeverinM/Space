using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VaisseauViolet : BaseEnnemi {

	public float ecart;
	protected float temps;
	public GameObject obj;
	private GameObject gob;
	protected float vitesse;

	// Use this for initialization
	void Awake () {
		transform.position = new Vector3 (Random.Range (-4.5f, 4.7f), 3f, 0);
		vitesse = Random.Range (0.3f, 1f) / 2;
		temps = 0;
		vie = 7;
		score = 4;
		pvMax = vie;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (0, -0.01f * vitesse, 0);
		if (Time.fixedTime - temps >= ecart) {
			temps = Time.fixedTime;
			Shoot ();
		}
	}

	protected void Shoot(){
		float angle = Mathf.PI / 8;
		float vitproj = Random.Range (0.02f, 0.03f);
		for (float i = 0; i < Mathf.PI * 2; i += angle) {
			gob = SimplePool.Spawn (obj, transform.position, new Quaternion ());
			gob.transform.localScale = new Vector3 (0.6f, 0.6f, 1);
			BaseProjectile proj = gob.GetComponent<BaseProjectile> ();
			proj.setDegat (1);
			proj.setDirection (new Vector2 (Mathf.Cos (i), Mathf.Sin (i)));
			proj.setVitesse (vitproj);
			gob.GetComponent<ProjectileVitesseVariable> ().AjouterAcceleration (5, -0.0007f);
		
		}

	}

	public override void Booster ()
	{
		base.Booster ();
		ecart /= 1.2f;
		score += 1;
		vie += 1;
		vitesse *= 1.2f;
	}
}
