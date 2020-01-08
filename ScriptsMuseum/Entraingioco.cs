using UnityEngine;
using System.Collections;

public class Entraingioco : MonoBehaviour {
	public GUIStyle stile_scritte;
	SalvaPosizione Save;
	bool entrato = false;
	public GameObject player;
	public string level;
	bool nellarea = false;
	void OnTriggerEnter(Collider oggetto)
	{

		if (oggetto.gameObject.name == "Giocatore_1")
		{
			nellarea = true;
		Debug.Log ("entrato nell'area di attivazione");
		entrato = true;
		}
	}
	void OnTriggerStay(Collider oggetto)
	{
		if (oggetto.gameObject.name == "Giocatore_1") 
		{
			if (Input.GetKeyDown (KeyCode.Return) && entrato) 
			{
				player.GetComponent<SalvaPosizione>().SavePosition();
				Application.LoadLevel (level);
			}
		}
	}

	void OnTriggerExit(Collider oggetto)
	{
		nellarea = false;
		entrato = false;
		Debug.Log ("Sei uscito dall'area di attivazione");
	}

	void OnGUI()
	{
		if(nellarea)
			GUI.Label(new Rect((Screen.currentResolution.width/2) - 500,Screen.currentResolution.height/2,1000,500),"Premi 'Enter' per fare una partita",stile_scritte);
	}
}
