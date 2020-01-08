using UnityEngine;
using System.Collections;


public class ControlloPalla : MonoBehaviour {

	private AudioSource source1;
	public AudioClip pong;

	void Awake()
	{
		source1 = GetComponent<AudioSource>();
	}

	public void Rimuovi()
	{
		Destroy(gameObject);
	}


	void OnCollisionEnter2D(Collision2D colla)
	{
		source1.PlayOneShot(pong,1f);
	}

		
}
