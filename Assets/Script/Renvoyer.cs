using UnityEngine;
using System.Collections;

public class Renvoyer : MonoBehaviour {
	// Use this for initialization

	private bool renvoyer = false; //Si fixée a vrai , renvoit les projectiles, sinon les detruit

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "AllyBullet" && transform.parent.tag == "Ennemy" && !(col.gameObject.GetComponent<BaseProjectile>().getPerforant())) { //un ennemi renvoit un projectile du joueur
			col.gameObject.GetComponent<BaseProjectile> ().setDirection (-(col.gameObject.GetComponent<BaseProjectile>().Direction));
			col.gameObject.GetComponent<SpriteRenderer> ().color = Color.red;
			col.gameObject.transform.Rotate (Vector3.forward, 180f);
			col.tag = "EnnemyBullet";
		}

		if (col.tag == "EnnemyBullet" && transform.parent.tag == "Player") {//Le bouclier du joueur ne renvoit pas les projectiles mais les detruit
			if (!(renvoyer))
				SimplePool.Despawn (col.gameObject);
			else {
				col.gameObject.GetComponent<BaseProjectile> ().setDirection (-(col.gameObject.GetComponent<BaseProjectile> ().Direction));
				col.gameObject.GetComponent<SpriteRenderer> ().color = Color.red;
				col.gameObject.transform.Rotate (Vector3.forward, 180f);
				col.tag = "AllyBullet";
			}
		}
	}

	public void setRenvoyer(bool valeur){
		renvoyer = valeur;
	}
}
