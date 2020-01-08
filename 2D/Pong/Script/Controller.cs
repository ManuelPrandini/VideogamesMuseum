using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour 
{
	Rigidbody2D newPalla;//Oggetto di riferimento
	public float velocità = 2f;
	public KeyCode su;
	public KeyCode giù;
	bool up,down;
	Avvio avvio;
	bool pausa;

	
	// Update is called once per frame
	void Update () 
	{

		{
			if(Input.GetKey(su))
			{
				transform.Translate(Vector2.right * velocità * Time.deltaTime);
			}


			if(Input.GetKey(giù))
			{
				transform.Translate(-Vector2.right * velocità * Time.deltaTime);
			}

		}


		//SE VIENE PREMUTO IL TASTO ESC TORNA AL MENU PRINCIPALE
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(Time.timeScale == 0)
				Time.timeScale = 1;
			Application.LoadLevel("Salagiochi");
	    }


	}
	
}

