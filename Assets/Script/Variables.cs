using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;

public static class Variables{
	//Variables globale utilisé pour l'ensemble du jeu 

	public static float sensiX = 0.025f; //Vitesse de deplacement X au clavier
	public static float sensiY = 0.025f; //Vitesse de deplacement Y au clavier 
	public static bool autoFire = false; //Permet de tirer automatiquement

	public static string pseudo;
	public static string temps;
	public static string score;

	public static string ChaineScore = "Erreur"; //Contient le top 10, necessite d'etre traité a part
	public static string position = "Err."; //La position du joueur
	public static string nbJoueur = "Err."; //Le nombre de joueur

}
