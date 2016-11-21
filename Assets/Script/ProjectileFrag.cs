using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileFrag : ProjectileSimple{

	public int nbproj;
	private float temps;
	public float duree;
	public GameObject projectile;
	private GameObject gob;

	public GameObject Projectile{
		set {projectile = value;}
	}

	void Awake(){
		nbproj = 16;
	}

	// Use this for initialization
	void Start () {
		temps = Time.fixedTime;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (direction.x * vitesse,direction.y * vitesse, 0);
		if (Time.fixedTime - temps >= duree)
			Fragmentation ();
	}

	public void setnbproj(int nb){
		nbproj = nb;
	}

	void Fragmentation(){
		int nombre = 360 / nbproj;
		for (int i = 0; i < nbproj; i++) {
			// gob = (GameObject)(Instantiate(projectile,transform.position,new Quaternion()));
			gob = SimplePool.Spawn(projectile,transform.position,new Quaternion());
			ProjectileSimple proj = gob.GetComponent<ProjectileSimple>();
			proj.tag = "EnnemyBullet";
			proj.GetComponent<SpriteRenderer> ().color = Color.white;

			proj.setVitesse(0.02f);
			proj.setDirection (new Vector2 (Mathf.Cos (Mathf.PI * (i * nombre) / 180), Mathf.Sin (Mathf.PI * (i * nombre) / 180)));
			proj.transform.localScale = this.gameObject.transform.localScale / 1.5f;
		}

		Destroy (this.gameObject);
	}

	public void setDuree(float nb){
		duree = nb;
	}
}
