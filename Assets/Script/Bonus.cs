using UnityEngine;
using System.Collections;

public class Bonus : BaseProjectile {


	// Use this for initialization
	void Start () {
		vitesse = 0.05f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (0, -0.1f * vitesse, 0);
	}

	public void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<Joueur> ().GetBonus ();
			Destroy (this.gameObject);
		}
	}
}
