using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour {

	private float duree; //La duree necessaire pour aller de 1 à 0
	private bool actif;
	private float temps;

	// Use this for initialization
	void Start () {
		actif = false;
		temps = 0;
		GetComponent<Image> ().fillAmount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.fixedTime > temps)
			actif = false;

		if (actif) {
			GetComponent<Image> ().fillAmount = (temps - Time.fixedTime) / (temps - (temps - duree));
		}
	}

	public void setCooldown(float dur){
		duree = dur;
		actif = true;
		temps = Time.fixedTime + duree;
	}
}
