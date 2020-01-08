using UnityEngine;
using System.Collections;

public class SalvaPosizione : MonoBehaviour {
	float newx,newy,newz;
	// Use this for initialization
	public void Awake()
	{
		LoadPosition();
	}
	void Start () 
	{
	

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.S))
		{
			SavePosition();
		Debug.Log("Posizione Salvata con Successo");
		Debug.Log("Nuove cordinate giocatore: x = " + newx + " y = " + newy + " z = " + newz); 
		}
	}

	public void SavePosition()
	{
		PlayerPrefs.SetFloat("x",gameObject.transform.position.x);
			newx = PlayerPrefs.GetFloat("x");
		PlayerPrefs.SetFloat("y",gameObject.transform.position.y+0.1f);
			newy = PlayerPrefs.GetFloat("y");
		PlayerPrefs.SetFloat("z",gameObject.transform.position.z);
			newz = PlayerPrefs.GetFloat("z");
	}

	public void LoadPosition()
	{
		newx = PlayerPrefs.GetFloat("x");
		newy = PlayerPrefs.GetFloat("y");
		newz = PlayerPrefs.GetFloat("z");
		gameObject.transform.position = new Vector3 (newx,newy,newz);
	}
}
