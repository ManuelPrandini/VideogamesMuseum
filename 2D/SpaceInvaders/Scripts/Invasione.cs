﻿using UnityEngine;
using System.Collections;

public class Invasione : MonoBehaviour {

	public bool invasa = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Alieno")
		{
			invasa = true;
		}
	}
}
