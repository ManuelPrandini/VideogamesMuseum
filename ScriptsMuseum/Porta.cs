using UnityEngine;
using System.Collections;

public class Porta : MonoBehaviour {
	public GUIStyle stile_scritte;
	bool aperto = false;
	Vector3 portaperta = new Vector3(0,-90,0);
	Vector3 portachiusa = new Vector3(0,90,0);
	bool nellarea = false;
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider oggetto)
	{
		nellarea = true;
	}
	void OnTriggerStay(Collider oggetto)
	{

		if(Input.GetKeyDown(KeyCode.Return) && !aperto )
		{
			nellarea = false;
			GUI.enabled = false;
			gameObject.transform.Rotate(portaperta);
			aperto = true;
			Debug.Log("Porta aperta");
		}

	}
	
	void OnTriggerExit(Collider oggetto)
	{
		nellarea = false;
		if(aperto)
		{
		gameObject.transform.Rotate(portachiusa);
			aperto = false;
		Debug.Log ("Porta chiusa");
		}
	}

	void OnGUI()
	{
		if(nellarea)
			GUI.Label(new Rect((Screen.currentResolution.width/2)- 500,Screen.currentResolution.height/2,1000,500),"Premi 'ENTER' per aprire",stile_scritte);
	}

}
