using UnityEngine;
using System.Collections;

public class BrickDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D proiettile)
	{
		if(proiettile.gameObject.tag == "aliensparo")
		{
			Destroy(gameObject);
			Destroy(proiettile.gameObject);
		}
		if(proiettile.gameObject.tag == "Mysparo")
		{
			Destroy(gameObject);
			Destroy(proiettile.gameObject);
		}

		if (proiettile.gameObject.tag == "Alieno")
			Destroy (gameObject);
	}
}
