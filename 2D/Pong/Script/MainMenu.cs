using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public GameObject Player;
	public GameObject Respawn;
	public bool uscitodalgioco = false;
	void OnGUI()
	{
		//se viene premuto il tasto Nuova partita avvia la scena pong
		if(GUI.Button(new Rect(Screen.width* .420f,Screen.height* .400f,Screen.width/6,Screen.height/8),"Nuova Partita"))
		{
			Application.LoadLevel("pong");
		}

		if(GUI.Button(new Rect(Screen.width* .420f,Screen.height* .550f,Screen.width/6,Screen.height/8),"Istruzioni"))
		{
		}
		if(GUI.Button(new Rect(Screen.width* .420f,Screen.height* .700f,Screen.width/6,Screen.height/8),"ESCI"))
		{
			Application.LoadLevel("Salagiochi");
			uscitodalgioco = true;
		}
	}
}

