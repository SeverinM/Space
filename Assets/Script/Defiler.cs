using UnityEngine;
using System.Collections;

public class Defiler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (0, -0.006f, 0);
	}

	void OnBecameInvisible(){
		transform.position += new Vector3 (0, (float)(GetComponent<SpriteRenderer>().bounds.size.y * 2), 0);
	}
}
