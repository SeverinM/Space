using UnityEngine;
using System.Collections;

public class Charger : MonoBehaviour {

	public System.Collections.Generic.List<GameObject> liste;
	public float ecart;  //La frequence d'apparition d'un ennemi
	private float temps;
	public GameObject mechant1;
	public GameObject mechant2;
	public GameObject mechant3;
	public GameObject mechant4;
	public GameObject mechant5;
	public GameObject mechant6;
	public GameObject mechant7;
	public GameObject mechant8;

	private GameObject gob;
	private bool boss; //Auncun ennemi ne spawn normalement si cette variable est vrai
	public float ecart2; //La frequence d'apparition d'un boss
	private float temps2;
	public GameObject typeboss;
	public GameObject warning;
	public GameObject projectilesimple;
	public GameObject projectilejoueur;

	public AudioClip bossmusic;
	public AudioClip battlemusic;
	public AudioClip warningSound;

	private bool fin;

	private float probaboost;

	// Use this for initialization
	void Start () {

		fin = false;

		SimplePool.Preload (projectilesimple, 300);
		SimplePool.Preload (projectilejoueur, 100);

		probaboost = 0f;
		boss = false;
		liste = new System.Collections.Generic.List<GameObject>();
		temps = 0;
		temps2 = 0;
		liste.Add (mechant1);
		liste.Add (mechant2);
		liste.Add (mechant3);
		liste.Add (mechant4);
		liste.Add (mechant5);
		liste.Add (mechant6);
		liste.Add (mechant7);
		liste.Add (mechant8); 
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad - temps >= ecart && !(boss) && !(fin)) { //On crée un ennemi aleatoire
			temps = Time.timeSinceLevelLoad;
			gob = (GameObject)(Instantiate ((GameObject)liste [Random.Range (0, liste.Count)]));
			int i = 0;
			while (Random.Range (0, 100) < probaboost - (i * 10)) //Chaque boost a 10% de moins de chance de se produire que le precedent
			{
				gob.GetComponent<BaseEnnemi> ().Booster ();
				i += 1;
			}

			if (i == 0 && probaboost > 0) //Si un ennemi apparait non-boosté, on en fait apparaitre un autre
			{
				gob = (GameObject)(Instantiate ((GameObject)liste [Random.Range (0, liste.Count)]));
			}
		}

		if (!(boss) && Time.timeSinceLevelLoad - temps2 >= ecart2 && !(fin)) { //On crée un boss aleatoire
			GameObject.Find("Musique").GetComponent<AudioSource>().clip = bossmusic;
			GameObject.Find ("Musique").GetComponent<AudioSource> ().Play ();
			GameObject.Find ("Musique").GetComponent<AudioSource> ().volume = 0.5f;

			GameObject.Find ("Son").GetComponent<AudioSource> ().PlayOneShot (warningSound,1);

			changerEtat ();
			gob = (GameObject)Instantiate (typeboss);
			GameObject.Find ("Warning").GetComponent<Animator> ().Play ("Warning");
			for (int i = 0; i < probaboost; i += 5)
				gob.GetComponent<BaseEnnemi> ().Booster();
		}
	}

	public void changerEtat(){
		boss = !(boss);
	}

	public void FixerTemps(){ //Appellée quand un boss est detruit, est utilisé pour monter la difficulté 
		GameObject.Find("Musique").GetComponent<AudioSource>().clip = battlemusic;
		GameObject.Find ("Musique").GetComponent<AudioSource> ().Play ();

		temps2 = Time.timeSinceLevelLoad;
		ecart2 += Random.Range (-10f, 10f);
		if (ecart2 < 30f) //on laisse au minimum 30 secondes entre chaque boss
			ecart2 = 30f;

		ecart -= 0.4f;	//On augmente la difficulté quand un boss meurt
		probaboost += 15;

		if (probaboost >= 300f) //On met un cap
			probaboost = 300f;
		
		if (ecart < 1f)
			ecart= 1f;
	}

	public void GameOver(){
		GameObject.Find ("Button3").GetComponent<UnityEngine.UI.Button> ().interactable = true;
		GameObject.Find ("Button2").GetComponent<UnityEngine.UI.Button> ().interactable = true;
		GameObject.Find ("Button").GetComponent<UnityEngine.UI.Button> ().interactable = true;
		GameObject.Find ("GameOver").GetComponent<Animator> ().Play ("GameOver");
		GameObject.Find ("Temps").GetComponent<Chrono> ().Pause ();
		Variables.score = GameObject.Find ("Score").GetComponent<ModifierScore> ().getScore ();
		Variables.temps = GameObject.Find ("Temps").GetComponent<Chrono> ().getTemp ();
		GameObject.Find ("Interface").GetComponent<InterfaceMySQL1> ().Afficher ();
		GameObject.Find ("Interface").GetComponent<InterfaceMySQL1> ().GetNomb ();
		fin = true;
	}

}
