using UnityEngine;
using System.Collections;

public class VaisseauSoin : BaseEnnemi {

	protected float vitesse;
	public float ecart;
	private float temps;
	private LineRenderer ligne;

	private GameObject vaisseau; //Le vaisseau qui doit etre soigné 

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (Random.Range (-4.5f, 4.7f), 3f, 0);
		vie = 5;
		score = 7;
		pvMax = vie;
		vitesse = Random.Range (0.3f, 1f) / 2;
		temps = Time.fixedTime;
		ligne = GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (0, -0.01f * vitesse, 0);

		try{
			ligne.SetPosition(1,vaisseau.transform.position);
			ligne.SetPosition(0,transform.position);
		}

		catch{
			ligne.SetPosition (0, new Vector3 (0, 0, 0));
			ligne.SetPosition (1, new Vector3 (0, 0, 0));
			if (GameObject.FindObjectsOfType<BaseEnnemi>().Length > 1)
				TrouverVaisseau ();

		}

		if (temps < Time.fixedTime) {
			temps = Time.fixedTime + ecart;
			try{
				vaisseau.GetComponent<BaseEnnemi> ().Soigner ();
			}
			catch{
				
			}
		}
	}

	public void setVaisseau(GameObject vaiss){ //Permet de chosir le vaisseau que l'on souhaite soigner
		vaisseau = vaiss;
	}

	public void TrouverVaisseau(){
		foreach (BaseEnnemi enn in GameObject.FindObjectsOfType<BaseEnnemi>()) {
			if (enn.gameObject != this.gameObject)
				setVaisseau (enn.gameObject);
		}
	}

	public override void Detruire ()
	{
		base.Detruire ();
	}

	public override void Booster ()
	{
		vie += 2;
		score += 1;
		ecart -= 0.25f;
		if (ecart < 0.75f)
			ecart = 0.75f;
		base.Booster ();
	}
}
