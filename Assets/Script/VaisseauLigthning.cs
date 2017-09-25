using UnityEngine;
using System.Collections;

public class VaisseauLigthning : BaseEnnemi {

	protected float vitesse;
	public float ecart;
	private float temps;
	public GameObject gameobj;

	// Use this for initialization
	void Start () {
		vie = 7;
		vitesse = Random.Range (0.3f, 1f) * 0.8f;
		score = 6;
		temps = 0;
		transform.position = new Vector3 (Random.Range (-4.5f, 4.7f), 3f, 0);
		pvMax = vie;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (0, -0.01f * vitesse, 0);
	}

	public override void Booster ()
	{
		vie += 2;
		score += 1;
		GameObject gob = (GameObject)Instantiate (gameobj, this.transform);
		base.Booster ();
	}
}
