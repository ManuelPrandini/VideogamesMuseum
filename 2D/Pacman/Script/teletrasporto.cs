using UnityEngine;
using System.Collections;

public class teletrasporto : MonoBehaviour {

	Vector2 LatoSinistro = new Vector2(-0.997051f,1.031083f);
	Vector2 LatoDestro = new Vector2(0.997051f,1.031083f);
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D area)
	{
		if(area.gameObject.name == "Pacman" && gameObject.tag == "right" || area.gameObject.name == "Pacman(Clone)" && gameObject.tag == "right")
		{
			area.transform.position =  LatoSinistro;
		}
		if(area.gameObject.name == "Pacman" && gameObject.tag == "left" || area.gameObject.name == "Pacman(Clone)" && gameObject.tag == "left")
		{
			area.transform.position =  LatoDestro;
		}
	}

}
