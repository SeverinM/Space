using UnityEngine;
using System.Collections;

public class BossBleu : BaseEnnemi {

	private bool avance;
	System.Collections.Generic.List<System.Action> liste;
	private System.Action choix;
	public GameObject projectileSimple;
	public GameObject projectileFrag;
	public GameObject barrePV; //Prefab pour afficher les PV
	private int nbprojectile;
	private GameObject barre;
	private bool passage = false;

	private float suivant; //Durée a partir de laquel on choisit le pattern suivant
	private float ecart; //Ecart entre deux pattern (qui peuvent etre les memes)
	private float vitesseProj;
	private float ligne; //Utilisé dans l'attaque "rideau" pour compter le nombre de ligne crée 

	private float angle; //Utilisé pour mesurer l'angle de tir actuel 

	// Use this for initialization
	void Awake () {
		vie = 50;

		barre = (GameObject)Instantiate (barrePV);
		barre.SetActive (false);

		this.tag = "Untagged";
		nbprojectile = 4;

		liste = new System.Collections.Generic.List<System.Action> ();
		liste.Add (AttaqueCirculaire);
		liste.Add (AttaqueCirculaire2);
		liste.Add (Rideau);

		avance = true;
		ligne = 0;

		score = 40;
		suivant = 0;
		ecart = 2.5f;
		vitesseProj = 0.06f;
		angle = Mathf.PI;
	}

	void Start(){
		transform.position = new Vector3 (0, 6, 0);
		probabonus = 100; //Un boss est sur de lacher un bonus a sa mort
	}
	
	// Update is called once per frame
	void Update () {
		if (ecart < 0.1f) //Par soucis de performance, on cap le boss
			ecart = 0.1f;
		
		if (avance)
			transform.position += new Vector3 (0, -0.005f, 0);
		
		if (transform.position.y < 1 && !(passage)) {
			GameObject.Find ("Musique").GetComponent<AudioSource> ().volume = 1;
			this.tag = "Ennemy";
			avance = false;
			barre.SetActive (true);
			barre.GetComponent<BarreVie> ().vaisseau = this.gameObject;
			barre.GetComponent<BarreVie> ().setVie (vie);
			passage = true;
		}

		if (!(avance) && choix == null && Time.fixedTime > suivant) { //Si aucune pattern n'est en cours, on en choisi un 
			choix = liste[Random.Range(0,liste.Count)];
		}

		if (choix != null && Time.fixedTime > suivant)
			choix ();
	}

	void AttaqueCirculaire(){
		for (int j = 0; j < 6; j++) {
			for (float i = Mathf.PI + 0.7f; i < Mathf.PI * 2; i += 0.7f) {
				float k = i + Random.Range (-0.5f, 0.5f);
				//GameObject gob = (GameObject)Instantiate (projectileSimple, transform.position, new Quaternion ());
				GameObject gob = SimplePool.Spawn(projectileSimple, transform.position, new Quaternion ());

				//GameObject gob2 = (GameObject)Instantiate (projectileSimple, transform.position, new Quaternion ());
				GameObject gob2 = SimplePool.Spawn(projectileSimple, transform.position, new Quaternion ());

				gob.tag = "EnnemyBullet";
				gob2.tag = "EnnemyBullet";

				gob.GetComponent<BaseProjectile> ().setDirection (new Vector2 (Mathf.Cos (k), Mathf.Sin (k)));
				gob2.GetComponent<BaseProjectile> ().setDirection (new Vector2 (-Mathf.Cos (k), Mathf.Sin (k)));

				gob.GetComponent<BaseProjectile> ().setDegat (1);
				gob.GetComponent<BaseProjectile> ().setVitesse (0.04f);

				gob2.GetComponent<BaseProjectile> ().setDegat (1);
				gob2.GetComponent<BaseProjectile> ().setVitesse (0.04f);
			}

			choix = null; //Le pattern est terminée, on en renvoit un autre
			suivant = Time.fixedTime + ecart;
		}
	}

	void UltraFrag(){
		//GameObject proj = (GameObject)Instantiate (projectileFrag,transform.position,new Quaternion());
		GameObject proj = SimplePool.Spawn(projectileFrag,transform.position, new Quaternion());
		proj.tag = "EnnemyBullet";

		proj.GetComponent<ProjectileFrag> ().setDegat (1);
		proj.GetComponent<ProjectileFrag> ().setDirection (Vector2.down);
		proj.GetComponent<ProjectileFrag> ().setVitesse (0.02f);
		proj.GetComponent<ProjectileFrag> ().setnbproj (nbprojectile);
		proj.GetComponent<ProjectileFrag> ().Projectile = projectileFrag;
		suivant = Time.fixedTime + (ecart * 3);

		choix = null; //Le pattern est finit, on en reprend un autre
	}

	void AttaqueCirculaire2(){
		GameObject gob = SimplePool.Spawn (projectileSimple, transform.position, new Quaternion ());
		gob.GetComponent<BaseProjectile> ().setDirection (new Vector2 (Mathf.Cos (angle + Random.Range(-0.05f,0.05f)), Mathf.Sin (angle)));
		gob.GetComponent<BaseProjectile> ().setDegat (1);
		gob.GetComponent<BaseProjectile> ().setVitesse (0.04f);
		gob.tag = "EnnemyBullet";

		gob = SimplePool.Spawn (projectileSimple, transform.position, new Quaternion ());
		gob.GetComponent<BaseProjectile> ().setDirection (new Vector2 (-Mathf.Cos (angle + Random.Range(-0.05f,0.05f)), Mathf.Sin (angle)));
		gob.GetComponent<BaseProjectile> ().setDegat (1);
		gob.GetComponent<BaseProjectile> ().setVitesse (0.04f);
		gob.tag = "EnnemyBullet";

		angle += 0.12f;

		suivant = Time.fixedTime + (ecart / 12);

		if (angle > Mathf.PI * 2) {
			angle = Mathf.PI;
			choix = null;
			suivant += ecart / 7;
		}
	}

	void Rideau(){
		Vector3 taille = GetComponent<SpriteRenderer> ().bounds.size;
		Vector3 centre = GetComponent<SpriteRenderer> ().bounds.center;

		for (float i = centre.x - (taille.x / 1.8f); i < centre.x + (taille.x / 1.8f); i += 0.5f) {
			GameObject gob = SimplePool.Spawn (projectileSimple, new Vector3 (i + (0.25f * (ligne % 2)), centre.y), new Quaternion());
			gob.GetComponent<SpriteRenderer> ().color = Color.white;
			gob.tag = "EnnemyBullet";


			gob.GetComponent<BaseProjectile> ().setDegat (1);
			gob.GetComponent<BaseProjectile> ().setDirection (Vector2.down);
			gob.GetComponent<BaseProjectile> ().setVitesse (0.04f);
		}

		suivant = Time.fixedTime + (ecart / 6);

		ligne++;

		if (ligne > 10) {
			choix = null;
			ligne = 0;
			suivant += ecart / 2;
		}



	}

	public override void perdreVie (int nb)
	{
		base.perdreVie (nb);
		ecart -= 0.02f;
		vitesseProj += 0.003f;
	}

	public override void Detruire ()
	{
		base.Detruire ();
		GameObject.Find ("Generateur").GetComponent<Charger> ().changerEtat ();
		GameObject.Find ("Generateur").GetComponent<Charger> ().FixerTemps ();
		Destroy (barre);
	}

	public override void Booster ()
	{
		ecart -= 0.1f;
		nbprojectile += 1;
		vie += 10;
		base.Booster ();
	}
}
