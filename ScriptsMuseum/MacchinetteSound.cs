using UnityEngine;
using System.Collections;

public class MacchinetteSound : MonoBehaviour {

	public AudioClip macchinette;
	private AudioSource source;
	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource>();
	}
	void OnTriggerStay(Collider oggetto)
	{
		source.enabled = true;
		source.PlayOneShot(macchinette,0.05f);
	}

	void OnTriggerExit(Collider oggetto)
	{
		source.enabled = false;
	}

}
