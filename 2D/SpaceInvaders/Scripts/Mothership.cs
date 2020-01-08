using UnityEngine;
using System.Collections;

public class Mothership : MonoBehaviour {

	public float velocità;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(Vector2.right * velocità * Time.deltaTime);
		if (gameObject.transform.position.x > 64.5f) 
		{
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Manager>().staPassando = false;
			Destroy (gameObject);
		}
	}


}

