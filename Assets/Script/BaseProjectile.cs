using UnityEngine;
using System.Collections;

public abstract class BaseProjectile : MonoBehaviour {

	protected float vitesse;
	protected Vector2 direction;
	protected Vector2 position;
	protected int degat;
	protected bool perforant; //Le projectile peut il passer le bouclier et les ennemis ?

	public Vector2 Direction {
		get{return this.direction;}
	}
		

	public virtual void Awake(){
		perforant = false; //Par defaut, aucun tir n'est perforant
		vitesse = 0; //Par defaut, un projectile est immobile
		degat = 1;
		position = Vector2.zero;
		direction = Vector2.zero;
	}

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().color = Color.white;
		tag = "EnnemyBullet";

	}
	
	// Update is called once per frame
	public void Update () {
	
	}

	void OnBecameInvisible(){
		SimplePool.Despawn (this.gameObject);
	}

	public virtual void setDirection(Vector2 dir){
		float longueur = Mathf.Sqrt ((dir.x * dir.x) + (dir.y * dir.y));
		dir.x /= longueur;
		dir.y /= longueur;
		direction = dir;
	}

	public virtual void setVitesse(float vit){
		vitesse = vit;
	}

	public virtual void setPosition(Vector2 pos){
		position = pos;
	}

	public virtual void setDegat(int dgt){
		degat = dgt;
	}

	public virtual void setPerforant(bool valeur){
		perforant = valeur;
	}

	public bool getPerforant(){
		return perforant;
	}

	public float getVitesse(){
		return vitesse;
	}
		
}
