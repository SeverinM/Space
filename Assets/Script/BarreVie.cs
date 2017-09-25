using UnityEngine;
using System.Collections;

public class BarreVie : MonoBehaviour {

	private float troncon; //La longueur qui equivaut a 1 PV
	public GameObject vaisseau; //l'ennemmi auquel on montre la vie qu'il a
	private LineRenderer ligne;
	private int vie; //PV max de l'ennemie

	// Use this for initialization
	void Start () {
		ligne = GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		ligne.SetPosition(1,new Vector3(-4f + (troncon * vaisseau.GetComponent<BaseEnnemi> ().getVie ()),2.5f,0));
	}

	public void setVie(int nb){
		vie = nb;
		troncon = vie / 8;
		troncon = 1 / troncon;
	}
}
