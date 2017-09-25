using UnityEngine;
using System.Collections;

public class VaisseauRond : BaseEnnemi {

	public float ecart;
	private float temps;
	private GameObject gob;
	public GameObject obj;
	private float vitesseProj;
	protected float vitesse;
	private float progression;


	// Use this for initialization
	void Awake () {
		vie = 5;
		transform.position = new Vector3 (Random.Range (-4.5f, 4.7f), 3f, 0);
		vitesse = Random.Range (0.3f, 1f);
		score = 4;
		vitesseProj = 1.2f;
		progression = 2;
		pvMax = vie;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (0, -0.01f * vitesse, 0);
		if (Time.fixedTime - temps >= ecart) {
			temps = Time.fixedTime;
			//gob = (GameObject)(Instantiate (obj, transform.position, new Quaternion ()));
			gob = SimplePool.Spawn (obj, transform.position, new Quaternion ());

			ProjectileTeteChercheuse proj = gob.GetComponent<ProjectileTeteChercheuse> ();
			proj.intensiteprogression = progression;
			proj.setVitesse (vitesseProj);

			try {
			proj.setDirection (new Vector2 (GameObject.Find ("Joueur").transform.position.x - transform.position.x, GameObject.Find ("Joueur").transform.position.y - transform.position.y));
			}

			catch{}
			proj.setIntensite (0.2f);
			proj.setCible (GameObject.Find ("Joueur"));
			proj.setDegat (1);
		}
	}

	public override void Booster ()
	{
		base.Booster ();
		vitesseProj *= 1.3f;
		ecart /= 1.1f;
		score += 1;
		vie += 1;
		vitesse *= 1.2f;
		base.Booster ();

		progression -= 0.1f;
		if (progression < 1)
			progression = 1.1f;
	}
}
