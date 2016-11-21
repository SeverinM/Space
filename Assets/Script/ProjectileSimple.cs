using UnityEngine;
using System.Collections;

public class ProjectileSimple : BaseProjectile {


	public override void Awake(){
		base.Awake ();
	}

	// Use this for initialization
	void Start () {

		GetComponent<SpriteRenderer> ().color = Color.white;


	}
	
	// Update is called once per frame
	public void Update () {
		transform.position += new Vector3 (direction.x * vitesse,direction.y * vitesse, 0);
	}

	public virtual void OnTriggerEnter2D(Collider2D col){
		//Le joueur touche un ennemi
		if (col.gameObject.tag == "Ennemy" && this.gameObject.tag == "AllyBullet") {
			if (!(perforant)) //Si le laser n'est pas perforant, il est detrit au premier ennemi rencontré 
				Destroy(this.gameObject);
			col.gameObject.GetComponent<BaseEnnemi> ().perdreVie (degat);
		}


		//l'ennemie touche le joueur
		if (col.gameObject.tag == "Player" && this.gameObject.tag == "EnnemyBullet"){
			if (!(perforant))
				Destroy (this.gameObject);
			GameObject.Find ("Joueur").GetComponent<Joueur> ().touche ();
		}

	}
}