using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour {

	// Use this for initialization
	public float velocità ;
	public int nemici_rim;
	public AudioClip invaderkilled;
	private AudioSource source;
	void Start () 
	{
		nemici_rim = GameObject.FindWithTag("MainCamera").GetComponent<Manager>().nemici.Length;
		source = GameObject.FindGameObjectWithTag ("Sfondo").GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 newposition = transform.position;
		newposition.y += Time.deltaTime * velocità;
		transform.position = newposition;
		EliminaPro();

	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Alieno")
		{
			source.PlayOneShot(invaderkilled,1f);
			GameObject.FindWithTag("MainCamera").GetComponent<Manager>().score +=100;
			Destroy(gameObject);
			Destroy(coll.gameObject);
			GameObject.FindWithTag("MainCamera").GetComponent<Manager>().rimasti--;
		}
		if (coll.gameObject.tag == "mothership") 
		{
			source.PlayOneShot(invaderkilled,1f);
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Manager>().staPassando = false;
			GameObject.FindWithTag("MainCamera").GetComponent<Manager>().score +=400;
			Destroy(gameObject);
			Destroy(coll.gameObject);
		}
	}

	void EliminaPro()
	{
		if(gameObject.transform.position.y >=38f)
		{
			Destroy(gameObject);
		}
	}


}
