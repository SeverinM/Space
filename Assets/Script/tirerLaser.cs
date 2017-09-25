using UnityEngine;
using System.Collections;

public class tirerLaser : MonoBehaviour {

	public GameObject tir;
	private float vitesse;
	private Vector3 vecteurLaser;
	private float angle;

	// Use this for initialization
	void Start () {
		angle = Random.Range (3.5f, 6f);
		vitesse += 0.01f;
	}

	// Update is called once per frame
	void Update () {
		angle += vitesse;
		if (angle > Mathf.PI * 2.25f || angle < Mathf.PI / 1.25f)
			vitesse = -vitesse;

		vecteurLaser = new Vector3 (Mathf.Cos (angle), Mathf.Sin (angle),0);

		GetComponent<LineRenderer> ().SetPosition (0, transform.parent.position);
		GetComponent<LineRenderer> ().SetPosition (1, transform.parent.position + (vecteurLaser * 6));

		foreach (RaycastHit2D hit in Physics2D.RaycastAll(transform.parent.position,new Vector2(vecteurLaser.x,vecteurLaser.y)))
		{
			if (hit.collider.gameObject.tag == "Player") {
				//GameObject gob = (GameObject)Instantiate (tir, transform.parent.position, new Quaternion());
				GameObject gob = SimplePool.Spawn(tir, transform.parent.position, new Quaternion());
				gob.transform.localScale = new Vector3 (0.6f, 0.6f, 1);
				gob.GetComponent<BaseProjectile> ().setDirection (new Vector2 (vecteurLaser.x, vecteurLaser.y));
				gob.GetComponent<BaseProjectile> ().setVitesse (0.15f);
			}
		}
	}
}
