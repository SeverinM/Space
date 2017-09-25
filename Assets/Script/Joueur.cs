using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Joueur : MonoBehaviour {

	float posz;
	float ecart; //Cadence de tir du joueur
	float temps;
	public GameObject gob; //GameObject du tir du joueur
	private GameObject instance; //GameObject instancié
	public bool invincible;
	private bool suivre; //Si fixé sur vrai , le vaisseau suivra la position de la souris
	private float vie; //PV actuel du joueur
    float LerpPosition = 0;
	private float retard;
	private float duree; //Durée d'un bonus

	private float temps5; //Temps où un bonus "perforant" est terminé
	private float temps4; //Temps où un bonus "invincible" est terminé (inutilisé)
	private float temps3; //Temps où un bonus "puissance de feu" est terminé 
	private float temps2;
	public GameObject objtouche; //Object instancié quand le joueur est touché
	private bool boost; //Si fixé sur vrai , le bonus "puissance de feu" est en cours
	private bool boost2; //Si fixé sur vrai , le bonus "perforant" est en cours
	private bool boost3; //Si fixé sur vrai, le bonus "Boost" est en cours
	private int degat; //Les degats de chaque projectile

	public AudioClip sontir;
	public AudioClip sontouche;
	public AudioClip sonbonus;

	public AudioClip extraLife;
	public AudioClip perforating;
	public AudioClip laserUp;
	public AudioClip Shield;
	public AudioClip sonBoost;
	public AudioClip Destr;

	public GameObject affichageBonus; //Game object crée lorsque le joueur recoit un bonus
	public GameObject shield; //Objet instancié lorsque l'on recoit le bonus "invincible"

	public GameObject AffichageBoost;
	public GameObject AffichagePerforant;
	public GameObject AffichageLaser;
	public GameObject AffichageShield;

	// Use this for initialization
	void Start () {
		vie = 3;
		ecart = 0.2f;
		invincible = false;
		suivre = true;
		retard = 2f;
		temps2 = 0f;
		GameObject.Find ("vie").GetComponent<UnityEngine.UI.Text> ().text = System.Convert.ToString (vie);
		boost = false;
		boost2 = false;
		boost3 = false;
		duree = 7f;
		degat = 1;
	}
	
	// Update is called once per frame
	void Update () {

		if (suivre && (Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0)) { //Le joueur ne suit la souris que si un mouvement est detecté
            LerpPosition = 0;
			Cursor.visible = true;
			transform.position = new Vector3 (transform.position.x, transform.position.y, 0);
		}

        if (Cursor.visible)
        {
            LerpPosition += Time.fixedDeltaTime * 4;
            transform.position = Vector3.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), LerpPosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.position += new Vector3 (-Variables.sensiX, 0, 0);
			Cursor.visible = false;
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.position += new Vector3 (Variables.sensiX, 0, 0);
			Cursor.visible = false;
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			transform.position += new Vector3 (0,Variables.sensiY, 0);
			Cursor.visible = false;
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			transform.position += new Vector3 (0, -Variables.sensiY, 0);
			Cursor.visible = false;
		}

		if ((Input.GetMouseButton (0) || Input.GetKey(KeyCode.Space) || Variables.autoFire) && Time.fixedTime - temps >= ecart) {
			GameObject.Find ("Son").GetComponent<AudioSource> ().clip = sontir;
			GameObject.Find ("Son").GetComponent<AudioSource> ().Play ();

			temps = Time.fixedTime;

			//instance = (GameObject)(Instantiate (gob, transform.position , new Quaternion ()));
			instance = SimplePool.Spawn(gob, transform.position , new Quaternion ());
			instance.tag = "AllyBullet";
			instance.GetComponent<SpriteRenderer> ().color = Color.white;

			instance.transform.Rotate (Vector3.forward * -270);
			ProjectileSimple proj = instance.GetComponent<ProjectileSimple> ();
			proj.setVitesse (0.07f);
			proj.setDirection (new Vector2 (0, 0.1f));
			proj.setPosition (transform.position);
			proj.setDegat (degat);
			if (boost2)
				proj.setPerforant (true);
			else {
				proj.setPerforant (false);
				proj.setDegat (degat);
			}

			if (boost) {
				//instance = (GameObject)(Instantiate (gob, transform.position , new Quaternion ()));
				instance = SimplePool.Spawn(gob, transform.position , new Quaternion ());
				instance.transform.Rotate (Vector3.forward * -300);
				instance.tag = "AllyBullet";
				instance.GetComponent<SpriteRenderer> ().color = Color.white;

				proj = instance.GetComponent<ProjectileSimple> ();
				proj.setVitesse (0.07f);
				proj.setDirection (new Vector2 (0.05f, 0.1f));
				proj.setPosition (transform.position);
				proj.setDegat (degat);
				if (boost2)
					proj.setPerforant (true);
				else {
					proj.setPerforant (false);
					proj.setDegat (degat);
				}

				//instance = (GameObject)(Instantiate (gob, transform.position , new Quaternion ()));
				instance = SimplePool.Spawn(gob, transform.position , new Quaternion ());
				instance.tag = "AllyBullet";
				instance.GetComponent<SpriteRenderer> ().color = Color.white;

				instance.transform.Rotate (Vector3.forward * -240);
				proj = instance.GetComponent<ProjectileSimple> ();
				proj.setVitesse (0.07f);
				proj.setDirection (new Vector2 (-0.05f, 0.1f));
				proj.setPosition (transform.position);
				proj.setDegat (degat);
				if (boost2)
					proj.setPerforant (true);
				else {
					proj.setPerforant (false);
					proj.setDegat (degat);
				}
			}
		}

		if (Time.fixedTime > temps2 + retard && invincible) {
			ChangerEtat ();
		}

		if (temps3 < Time.fixedTime) { //A chaque frame on verifie que le bonus est terminé
			boost = false;
			ecart = 0.2f;
		}

		if (temps5 < Time.fixedTime && boost2) {
			degat = 1;
			boost2 = false;
		}
	}

	public void touche(){
		vie -= 1;
		GameObject.Find ("Son").GetComponent<AudioSource> ().PlayOneShot (sontouche,2f);

		GameObject.Find ("vie").GetComponent<UnityEngine.UI.Text> ().text = System.Convert.ToString (vie);
		if (vie <= 0) {
			GameOver ();
		}

		else {
			ChangerEtat ();
			float larg = GetComponent<SpriteRenderer> ().bounds.size.x;
			float haut = GetComponent<SpriteRenderer> ().bounds.size.y;

			GameObject gameobj2 = (GameObject)Instantiate (objtouche, new Vector3(transform.position.x + Random.Range(-larg / 2,larg / 2),transform.position.y + Random.Range(-haut / 2,haut / 2),0),new Quaternion());
			gameobj2.transform.SetParent (this.transform);
			Invincible (false);

		}
	}

	public void ChangerEtat(){
		invincible = !(invincible);
		temps2 = Time.fixedTime;

		if (invincible) {
			GetComponent<SpriteRenderer> ().color += new Color (0, 0, 0, -0.5f);
			this.gameObject.tag = "Untagged";
		} else {
			GetComponent<SpriteRenderer> ().color += new Color (0, 0, 0, 0.5f);
			this.gameObject.tag = "Player";
		}
	}

	public void GameOver(){
		suivre = false;
		tag = "Untagged";
		GetComponent<Animator> ().Play ("Destruction");
		GameObject.Find ("Generateur").GetComponent<Charger> ().changerEtat ();
		Destroy (GetComponent<Rigidbody> ());
		try{
			Destroy(GameObject.Find("BossBleu(Clone)"));
		}
		catch{}

		GameObject.Find ("Generateur").GetComponent<Charger> ().GameOver ();
	}

	public void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Ennemy")
			touche ();	
	}

	public void GetBonus(){ //Lorsque l'on a un bonus, on choisit un effet aleatoire
		switch (Random.Range (1, 7)) {
		case 2:
			if (Random.Range (1, 4) > vie) //Plus on a de vie et moins on a de chance d'avoir un bonus 'vie supplementaire'
				ExtraLife ();
			else
				GetBonus ();
			break;
		case 3:
			TirSup ();
			break;
		case 4:
			Destruction ();
			break;

		case 5:
			Perforant ();
			break;

		case 6:
			if (!(boost3))
				BoosterBonus ();
			else
				GetBonus ();
			break;
	
		default :
			if (transform.childCount == 0) //On ne peut avoir qu'un seul bouclier a chaque fois
				Invincible (true);
			else
				GetBonus (); //Dans ce cas on reroll le bonus
			break;
		}
	}

	private void ExtraLife(){
		GameObject.Find("Son").GetComponent<AudioSource>().PlayOneShot(extraLife);
		GameObject gameobj = (GameObject)Instantiate (affichageBonus,GameObject.Find("Canvas").transform);
		gameobj.GetComponent<UnityEngine.UI.Text> ().text = "Vie supplementaire";
		vie += 1;
		GameObject.Find ("vie").GetComponent<UnityEngine.UI.Text> ().text = System.Convert.ToString (vie);
		if (boost3) { //Quand le bonus est boosté , on donne egalement une invincibilité et une autre vie
			boost3 = false;
			Invincible (true);
			vie += 1;
			gameobj.GetComponent<UnityEngine.UI.Text> ().color = Color.yellow;
			AffichageBoost.GetComponent<UnityEngine.UI.Image> ().enabled = false;
		}
	}

	private void TirSup(){
		GameObject.Find("Son").GetComponent<AudioSource>().PlayOneShot(laserUp);
		GameObject gameobj = (GameObject)Instantiate (affichageBonus,GameObject.Find("Canvas").transform);
		gameobj.GetComponent<UnityEngine.UI.Text> ().text = "Puissance de feu";
		temps3 = Time.fixedTime + duree;
		boost = true;

		AffichageLaser.GetComponent<Cooldown> ().setCooldown (temps3 - Time.fixedTime);

		if (boost3) { //quand le bonus est boosté, on augmente la cadence de tir
			boost3 = false;
			ecart = 0.1f;
			gameobj.GetComponent<UnityEngine.UI.Text> ().color = Color.yellow;
			AffichageBoost.GetComponent<UnityEngine.UI.Image> ().enabled = false;
		}
	}

	private void Invincible(bool activate){
		GameObject.Find("Son").GetComponent<AudioSource>().PlayOneShot(Shield);
		GameObject gameobj = (GameObject)Instantiate (shield, this.gameObject.transform);
		gameobj.transform.position = this.gameObject.transform.position;

		if (!(activate)) {
			Destroy (gameobj, 2.5f);
		} else {
			GameObject gameobj2 = (GameObject)Instantiate (affichageBonus,GameObject.Find("Canvas").transform);
            gameobj2.transform.localScale *= 2;
			gameobj2.GetComponent<UnityEngine.UI.Text> ().text = "Bouclier";
			if (boost3) 
				gameobj2.GetComponent<UnityEngine.UI.Text> ().color = Color.yellow;
		}

		if (boost3 && activate) { //quand le bonus est boosté, l'invincibilité dure deux fois plus longtemps et renvoit les projectiles
			boost3 = false;
			Destroy (gameobj, duree * 1.5f);
			gameobj.GetComponent<Renvoyer> ().setRenvoyer (true);
			gameobj.GetComponent<SpriteRenderer> ().color = Color.yellow;
			AffichageShield.GetComponent<Cooldown> ().setCooldown (duree * 1.5f);
			AffichageBoost.GetComponent<UnityEngine.UI.Image> ().enabled = false;
		} else {
			Destroy (gameobj, duree);
			AffichageShield.GetComponent<Cooldown> ().setCooldown (duree);
		}
	}

	private void Destruction(){
		GameObject.Find("Son").GetComponent<AudioSource>().PlayOneShot(Destr);
		GameObject gameobj = (GameObject)Instantiate (affichageBonus,GameObject.Find("Canvas").transform);
		gameobj.GetComponent<UnityEngine.UI.Text> ().text = "Destruction";

		BaseEnnemi[] liste = GameObject.FindObjectsOfType<BaseEnnemi> ();
		foreach (BaseEnnemi enn in liste) {
			enn.perdreVie (10 + Random.Range(1,10));
			if (boost3) { //Quand l'effet est boosté, detruit tous les ennemis à l'exception du boss
				AffichageBoost.GetComponent<UnityEngine.UI.Image> ().enabled = false;
				gameobj.GetComponent<UnityEngine.UI.Text> ().color = Color.yellow;
				boost3 = false;
				foreach (BaseEnnemi enn2 in liste) {
					if (enn2.gameObject == GameObject.FindObjectOfType<BossBleu>())
						enn2.perdreVie (enn2.getVie());
				}
			}
		}
	}

	private void Perforant(){
		GameObject.Find("Son").GetComponent<AudioSource>().PlayOneShot(perforating);
		GameObject gameobj = (GameObject)Instantiate (affichageBonus,GameObject.Find("Canvas").transform);
		gameobj.GetComponent<UnityEngine.UI.Text> ().text = "Perforant";
		degat = 2;

		temps5 = Time.fixedTime + duree;

		if (boost3) { //l'effet dure plus longtemps et les degats sont encore augmenté
			boost3 = false;
			degat = 3;
			temps5 += duree * 1.5f;
			gameobj.GetComponent<UnityEngine.UI.Text> ().color = Color.yellow;
			AffichageBoost.GetComponent<UnityEngine.UI.Image> ().enabled = false;
		}

		AffichagePerforant.GetComponent<Cooldown> ().setCooldown (temps5 - Time.fixedTime);
		boost2 = true;
	}

	private void BoosterBonus(){
		GameObject.Find("Son").GetComponent<AudioSource>().PlayOneShot(sonBoost);
		GameObject gameobj = (GameObject)Instantiate (affichageBonus,GameObject.Find("Canvas").transform);
		gameobj.GetComponent<UnityEngine.UI.Text> ().text = "Bonus Boost";
		boost3 = true;
		AffichageBoost.GetComponent<UnityEngine.UI.Image> ().enabled = true;
	}
}
