using UnityEngine;
using System.Collections;

public class ProjectileTeteChercheuse : ProjectileSimple {

	protected float intensite;
	public GameObject cible;
	private float ecart = 1;
	private float temps;
	public float intensiteprogression; //La perte d'aimantation a chaque frame

	public override void Awake(){
		intensite = 0.5f;
		base.Awake ();
		intensiteprogression = 2;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (cible != null) {
			setDirection (direction * vitesse + new Vector2 ((cible.transform.position.x - transform.position.x) * intensite, (cible.transform.position.y - transform.position.y) * intensite));
		} else
			setDirection (new Vector2 (0, 1));
		
		transform.position += new Vector3 ((direction.x / 100) * vitesse , (direction.y / 100) * vitesse, 0);

		if (Time.fixedTime - temps >= ecart) {
			temps = Time.fixedTime;
			intensite /= intensiteprogression;
		}
	}

	public virtual void setIntensite(float nb){
		intensite = nb;
	}

	public float getIntensite(){
		return intensite;
	}

	public virtual void setCible(GameObject gameobj){
		cible = gameobj;
	}

	public void perdreProgression(float nb){
		intensiteprogression -= nb;
		if (intensiteprogression < 1)
			intensiteprogression = 1.1f;
	}
}
