using UnityEngine;
using System.Collections;

public class ExitFromMuseum : MonoBehaviour {
	public GUIStyle stile_scritte;
	bool nellarea = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider personaggio)
	{
		nellarea = true;
		Debug.Log("Esci Dal Museo?");
			

	}

	void OnTriggerStay(Collider personaggio)
	{
		if(Input.GetKeyDown(KeyCode.Return))
		{

			GameObject.FindGameObjectWithTag("Player").GetComponent<SalvaPosizione>().SavePosition();
			Debug.Log("Uscito dal Museo");
			Application.Quit();
		}

	}

	void OnTriggerExit(Collider personaggio)
	{
		nellarea = false;
	}
	void OnGUI()
	{
		if(nellarea)
			GUI.Label(new Rect((Screen.currentResolution.width/2) - 500,Screen.currentResolution.height/2,1000,500),"Premi 'ENTER' per uscire dal Museo",stile_scritte);
	}
}
