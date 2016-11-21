using UnityEngine;
using System.Collections;

public abstract class BaseEnnemi : MonoBehaviour {

	/*Classe de base pour tous les ennemis du jeu
	 * 
	 * Le joueur ne peut toucher que les ennemis avec le tag 'Ennemy'
	 * Chaque ennemi a au minimum une valeur de score et un nombre positif de vie
	 * 
	 * 
	 * 
	 */
	protected int vie;
	protected int pvMax;

	protected float porteeexplo;
	protected int gain;
	protected int score;
	public GameObject gob2;
	public GameObject touche;
	public GameObject bonus;
	protected int probabonus = 20; //La probabilité qu'un bonus apparaisse a la mort 

	public AudioClip explosion; //Son enclenché quand un vaisseau se fait touché ou est detruit


	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

	public virtual void Detruire(){
		foreach (Transform child in transform) {
			if (child.name.StartsWith("Laser"))
				Destroy (child.gameObject);
		}

		GameObject.Find ("Score").GetComponent<ModifierScore> ().AugmenterScore (score);

		GameObject gameobj = (GameObject)Instantiate (gob2);

		gameobj.transform.parent = GameObject.Find ("Canvas").transform;
		gameobj.GetComponent<UnityEngine.UI.Text> ().text = " + " + System.Convert.ToString(score);
		GetComponent<SpriteRenderer> ().color = Color.white;
		Destroy (this.GetComponent<Rigidbody2D> ());
		Destroy (this.GetComponent<BaseEnnemi> ());

		this.GetComponent<Animator> ().Play ("Destruction");
		if (Random.Range (0, 100) < probabonus)
			SimplePool.Spawn(bonus, this.transform.position,new Quaternion());
	}

	void OnBecameInvisible(){
		Destroy (this.gameObject);
	}

	public virtual void perdreVie(int nb){

		vie -= nb;
		if (vie <= 0) {
			GameObject.Find ("Son").GetComponent<AudioSource> ().PlayOneShot (explosion,2f);
			Detruire ();
		}
		else {
			GameObject.Find ("Son").GetComponent<AudioSource> ().PlayOneShot (explosion,0.5f);
			float larg = GetComponent<SpriteRenderer> ().bounds.size.x;
			float haut = GetComponent<SpriteRenderer> ().bounds.size.y;
			GameObject gameobj2 = (GameObject)Instantiate (touche, new Vector3(transform.position.x + Random.Range(-larg / 2,larg / 2),transform.position.y + Random.Range(-haut / 2,haut / 2),0),new Quaternion());
			gameobj2.transform.SetParent (this.transform);
		}
	}

	public virtual void Booster(){
		Color col = GetComponent<SpriteRenderer> ().color;
		GetComponent<SpriteRenderer> ().color = new Color (col.r, col.g - 0.1f, col.b - 0.1f);
		probabonus += 2; //A chaque fois qu'un ennemi est boosté, il a 2% de chance en plus de lacher un bonus a sa mort
		pvMax = vie;
	}

	public int getVie(){
		return vie;
	}

	public virtual void Soigner(){
		if (vie < pvMax) {
			vie += 1;
			GetComponent<Animator> ().Play ("Soin");
		}
	}
}
