using UnityEngine;
using System.Collections;

public class InterfaceMySQL1 : MonoBehaviour {

	public string addScoreURL = "http://localhost/unity/addscore.php?";
	public string highscoreURL = "http://localhost/unity/display.php";
	public string GetPosition = "http://localhost/unity/AfficherPosition.php?";
	public string GetNombre = "http://localhost/unity/NombreJoueur.php";
	private WWW hs_get;
	private WWW hs_get2;
	private WWW hs_get3;

	void Start(){
		
	}

	public void Afficher()
	{
		StartCoroutine (GetScores ());
	}

	public void PosterScore()
	{
		StartCoroutine(PostScores(Variables.pseudo,Variables.score,Variables.temps));
	}

	public void GetClassement(){
		StartCoroutine (GetPos ());
	}

	public void GetNomb(){
		StartCoroutine (GetNb ());
	}

	// remember to use StartCoroutine when calling this function!
	IEnumerator PostScores(string name, string score,string temps)
	{
		//This connects to a server side php script that will add the name and score to a MySQL DB.
		// Supply it with a string representing the players name and the players score.

		string post_url = addScoreURL + "name=" + Variables.pseudo + "&score=" + score + "&temps=" + temps;

		// Post the URL to the site and create a download object to get the result.
		WWW hs_post = new WWW(post_url);
		yield return hs_post; // Wait until the download is done

		if (hs_post.error != null)
		{
			print("There was an error posting the high score: " + hs_post.error);
		}
	}

	// Get the scores from the MySQL DB to display in a GUIText.
	// remember to use StartCoroutine when calling this function!
	IEnumerator GetScores()
	{
		hs_get = new WWW (highscoreURL);
		yield return hs_get;

		if (hs_get.error != null)
		{
			print("There was an error getting the high score: " + hs_get.error);
		}
		else
		{
			Variables.ChaineScore = hs_get.text;
		}
	}

	IEnumerator GetPos(){
		hs_get2 = new WWW (GetPosition + "score=" + Variables.score);
		yield return hs_get2;

		Variables.position = hs_get2.text;
	}

	IEnumerator GetNb(){
		hs_get3 = new WWW(GetNombre);
		yield return hs_get3;

		Variables.nbJoueur = hs_get3.text;
	}

}

