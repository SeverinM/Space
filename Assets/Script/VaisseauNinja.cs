using UnityEngine;
using System.Collections;

public class VaisseauNinja : BaseEnnemi {

	private float posx;
	public GameObject obj;
	private GameObject gob;
	public float ecart;
	private float temps;
	protected float vitesse;

	// Use this for initialization
	void Awake () {
		vie = 3;
		score = 1;
		if (Random.Range (0, 4) >= 2) {
			posx = -6f;
			vitesse = Random.Range (0.007f, 0.017f);
		}

		else {
			posx = 6f;
			vitesse = Random.Range (-0.017f, -0.007f);
		}

		transform.position = new Vector3 (posx, Random.Range (0, 2), 0);
		pvMax = vie;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (vitesse, 0, 0);
		if (Time.fixedTime - temps >= ecart) {
			temps = Time.fixedTime;

			//On tire dans les 4 directions
			//gob = (GameObject)(Instantiate(obj,transform.position,new Quaternion()));
			gob = SimplePool.Spawn(obj,transform.position,new Quaternion());
			gob.transform.localScale = new Vector3 (0.6f, 0.6f, 1);
			ProjectileSimple proj = gob.GetComponent<ProjectileSimple>();
			proj.setVitesse (0.02f);
			proj.setDirection (new Vector2 (0, -1));
			proj.setDegat (1);

			//gob = (GameObject)(Instantiate(obj,transform.position,new Quaternion()));
			gob = SimplePool.Spawn(obj,transform.position,new Quaternion());
			gob.transform.localScale = new Vector3 (0.6f, 0.6f, 1);
			proj = gob.GetComponent<ProjectileSimple>();
			proj.setVitesse (0.02f);
			proj.setDirection (new Vector2 (0, 1));
			proj.setDegat (1);

			//gob = (GameObject)(Instantiate(obj,transform.position,new Quaternion()));
			gob = SimplePool.Spawn(obj,transform.position,new Quaternion());
			gob.transform.localScale = new Vector3 (0.6f, 0.6f, 1);
			proj = gob.GetComponent<ProjectileSimple>();
			proj.setVitesse (0.02f);
			proj.setDirection (new Vector2 (1, 0));
			proj.setDegat (1);

			//gob = (GameObject)(Instantiate(obj,transform.position,new Quaternion()));
			gob = SimplePool.Spawn(obj,transform.position,new Quaternion());
			gob.transform.localScale = new Vector3 (0.6f, 0.6f, 1);
			proj = gob.GetComponent<ProjectileSimple>();
			proj.setVitesse (0.02f);
			proj.setDirection (new Vector2 (-1, 0));
			proj.setDegat (1);
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
