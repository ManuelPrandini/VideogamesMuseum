using UnityEngine;
using System.Collections;

public class GhostMovement : MonoBehaviour {

	public Transform[] waypoints;
	public int cur = 0;
	public int tappa;
	public Vector3 PosIniziale = new Vector3 (0,0,0); 
	public float speed = 0.3f;


	// Use this for initialization
	void Start () 
	{
		PosIniziale = transform.position;
		tappa = cur;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

		if(GameObject.Find("Scena").GetComponent<Pacman>().giocoiniziato && GameObject.Find("Scena").GetComponent<Pacman>().vittoria == false)
		{
		// Waypoint not reached yet? then move closer
		if (transform.position != waypoints[cur].position) {
			Vector2 p = Vector2.MoveTowards(transform.position,
			                                waypoints[cur].position,
			                                speed);
			GetComponent<Rigidbody2D>().MovePosition(p);
		}
		// Waypoint reached, select next one
		else cur = (cur + 1) % waypoints.Length;
		}

		if (GameObject.Find ("Scena").GetComponent<Pacman> ().vittoria) 
		{
			transform.position = PosIniziale;
		}
		/*
		if(GameObject.Find("Scena").GetComponent<Pacman>().morto == true)
		{
			cur = tappa;
			gameObject.transform.position = PosIniziale;
		}
		*/
	}

	void OnTriggerEnter2D(Collider2D co) {
		if (co.name == "Pacman" && !co.GetComponent<controllerpacman>().SonoMangiabili)
		{
			Destroy(co.gameObject);
			GameObject.Find("Scena").GetComponent<Pacman>().lives--;
			GameObject.Find("Scena").GetComponent<Pacman>().source.PlayOneShot(GameObject.Find("Scena").GetComponent<Pacman>().mortoSound,1f);
			GameObject.Find("Scena").GetComponent<Pacman>().morto = true;
			StartCoroutine(Aspetta());
		}

		if (co.name == "Pacman(Clone)" && !co.GetComponent<controllerpacman>().SonoMangiabili)
		{
			Destroy(co.gameObject);
			GameObject.Find("Scena").GetComponent<Pacman>().lives--;
			GameObject.Find("Scena").GetComponent<Pacman>().source.PlayOneShot(GameObject.Find("Scena").GetComponent<Pacman>().mortoSound,1f);
			GameObject.Find("Scena").GetComponent<Pacman>().morto = true;
			StartCoroutine(Aspetta());
		}


	}

	IEnumerator Aspetta()
	{
		yield return new WaitForSeconds(3f);
		GameObject.Find("Inky").GetComponent<GhostMovement>().cur = GameObject.Find("Inky").GetComponent<GhostMovement>().tappa;
		GameObject.Find("Inky").transform.position = GameObject.Find("Inky").GetComponent<GhostMovement>().PosIniziale;
		GameObject.Find("Blinky").GetComponent<GhostMovement>().cur = GameObject.Find("Inky").GetComponent<GhostMovement>().tappa;
		GameObject.Find("Blinky").transform.position = GameObject.Find("Inky").GetComponent<GhostMovement>().PosIniziale;
		GameObject.Find("Speedy").GetComponent<GhostMovement>().cur = GameObject.Find("Inky").GetComponent<GhostMovement>().tappa;
		GameObject.Find("Speedy").transform.position = GameObject.Find("Inky").GetComponent<GhostMovement>().PosIniziale;
		GameObject.Find("Clyde").GetComponent<GhostMovement>().cur = GameObject.Find("Inky").GetComponent<GhostMovement>().tappa;
		GameObject.Find("Clyde").transform.position = GameObject.Find("Inky").GetComponent<GhostMovement>().PosIniziale;
	}
}
