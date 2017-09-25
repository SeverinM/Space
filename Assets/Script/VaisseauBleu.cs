using UnityEngine;
using System.Collections;

public class VaisseauBleu : BaseEnnemi {

	public float ecart;
	private float temps;
	private GameObject gob;
	public GameObject obj;
	private float vitesseProj;
	protected float vitesse;

	// Use this for initialization
	void Awake () {
		transform.position = new Vector3 (Random.Range (-4.5f, 4.7f), 3f, 0);
		vitesse = Random.Range (0.3f, 1f);
		vie = 3;
		score = 2;
		vitesseProj = 0.05f;
		pvMax = vie;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (0, -0.01f * vitesse, 0);
		if (Time.fixedTime - temps >= ecart) {
			temps = Time.fixedTime;
			//gob = (GameObject)(Instantiate (obj, transform.position, new Quaternion ()));
			gob = SimplePool.Spawn(obj, transform.position, new Quaternion ());
			gob.transform.localScale = new Vector3 (0.6f, 0.6f, 1);

			ProjectileSimple proj = gob.GetComponent<ProjectileSimple> ();
			proj.setVitesse (vitesseProj);

			try{
			proj.setDirection (new Vector2 (GameObject.Find ("Joueur").transform.position.x - transform.position.x, GameObject.Find ("Joueur").transform.position.y - transform.position.y));
			}

			catch{proj.setDirection (new Vector2 (Random.Range (0, 5), Random.Range (0, 5)));}
		}
	}

	public override void Booster ()
	{
		base.Booster ();
		vitesseProj *= 1.2f;
		ecart /= 1.1f;
		score += 1;
		vie += 1;
		vitesse *= 1.2f;
	}
}
