using UnityEngine;
using System.Collections;

public class ProjectileVitesseVariable : ProjectileSimple {

	//A chaque mise a jour, varie la vitesse avec une certaine valeur
	protected float variation;

	protected float temps;
	protected float dureee;

	// Use this for initialization
	void Start () {
		temps = Time.fixedTime;
	}
	
	// Update is called once per frame
	public void Update () {
		if (Time.fixedTime - temps <= dureee)
			vitesse += variation;
		transform.position += new Vector3 (direction.x * vitesse,direction.y * vitesse, 0);
	}

	public void AjouterAcceleration(float duree, float intensité){
		temps = Time.fixedTime;
		dureee = duree;
		variation = intensité / duree;
	}


}
