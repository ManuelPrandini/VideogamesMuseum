using UnityEngine;
using System.Collections;

public class BulletAlieno : MonoBehaviour {

	// Use this for initialization
	public float velocità ;
	public AudioClip explosion;
	private AudioSource source;
	void Start () 
	{
		source = GameObject.FindGameObjectWithTag ("Sfondo").GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 newposition = transform.position;
		newposition.y -=Time.deltaTime * velocità;
		transform.position = newposition;
		EliminaPro();
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			source.PlayOneShot(explosion,1f);
			GameObject.FindWithTag("MainCamera").GetComponent<Manager>().life --;
			Destroy(gameObject);
			Destroy(coll.gameObject);
			GameObject.FindWithTag("MainCamera").GetComponent<Manager>().morto = true;
		}
	
	}

	void EliminaPro()
	{
		if(gameObject.transform.position.y <=-24f)
		{
			Destroy(gameObject);
		}
	}

}
