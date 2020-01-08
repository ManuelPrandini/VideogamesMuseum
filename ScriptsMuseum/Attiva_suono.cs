using UnityEngine;
using System.Collections;

public class Attiva_suono : MonoBehaviour {

	public AudioClip suono;
	private AudioSource source;
	// Use this for initialization

	void Start () 
	{
		source = GetComponent<AudioSource>();
	}
	void OnTriggerEnter(Collider oggetto)
	{
		if(oggetto.gameObject.tag == "Player")
		{
		source.enabled = true;
		source.Play();
		}
	}

	void OnTriggerExit(Collider oggetto)
	{
		source.enabled = false;
	}

}
