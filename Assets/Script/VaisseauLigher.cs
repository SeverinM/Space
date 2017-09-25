using UnityEngine;
using System.Collections;

public class VaisseauLigher : BaseEnnemi {
	
	protected float vitesse;
	public float ecart;
	private float temps;

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
		if (Time.fixedTime - temps >= ecart) {
			temps = Time.fixedTime;
			transform.GetChild (0).gameObject.SetActive (!(transform.GetChild (0).gameObject.activeSelf));
		}
		transform.position += new Vector3 (0, -0.01f * vitesse, 0);
	}

	public override void Booster ()
	{
		base.Booster ();
		ecart /= 1.2f;
		score += 1;
	}

	public override void Detruire ()
	{
		base.Detruire ();
	}
}
