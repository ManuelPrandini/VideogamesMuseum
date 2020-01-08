using UnityEngine;
using System.Collections;

public class AlienoMovimento : MonoBehaviour {

	bool go_left = false;
	public float velocità ;
	float posx,posy,restartx,restarty;
	// Use this for initialization
	void Awake()
	{
		posx = gameObject.transform.position.x;
		posy = gameObject.transform.position.y;
	}
	public void Start ()
	{
		restartx = posx;
		restarty = posy;
	}
	void Update () 
	{
		if(go_left)
			transform.Translate(-Vector2.right * velocità * Time.deltaTime);
	else
			transform.Translate(Vector2.right * velocità * Time.deltaTime);
	}

	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D muro) 
	{
	if(muro.gameObject.name =="Muros")
		{
			go_left = false;
		transform.Translate((-Vector2.up*10) * velocità * Time.deltaTime);
		}
	else if(muro.gameObject.name =="Murod")
		{
			go_left = true;
		transform.Translate((-Vector2.up*10) * velocità * Time.deltaTime);
		}
	}
}
