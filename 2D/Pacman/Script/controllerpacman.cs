using UnityEngine;
using System.Collections;

public class controllerpacman : MonoBehaviour {
	
	bool right,left,up,down,enableX;
	public bool SonoMangiabili;
	Animator anim;
	public AudioSource source; // componente audio
	public AudioClip mangia,eatghost;
	public Sprite mangiabile,blinky,clyde,inky,speedy;
	float vel = 2.5f;//più aumenti e più pacman è lento
	void Awake()
	{
		right = false;
		left= false;
		up = false;
		down = false;
		anim = gameObject.GetComponent<Animator> (); 
		enableX = true;
		SonoMangiabili = false;

	}
	void Start () 
	{
		source = GetComponent<AudioSource>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(GameObject.Find("Scena").GetComponent<Pacman>().giocoiniziato && GameObject.Find("Scena").GetComponent<Pacman>().vittoria == false)
		{
		comandi ();

		if (right) {
			transform.Translate ((Vector2.right/vel) * Time.deltaTime);
			enableX = true;
			if(enableX)
			{
				anim.SetFloat("DirY",0);
			anim.SetFloat ("DirX", 1f);
			}
		} else if (left) {
			transform.Translate ((-Vector2.right/vel) * Time.deltaTime);
			enableX = true;
			if(enableX)
			{
				anim.SetFloat("DirY",0);
			anim.SetFloat ("DirX", -1f);
			}
		} else if (up) {
			transform.Translate ((Vector2.up/vel) * Time.deltaTime);
			enableX = false;
			if(!enableX)
			{
				anim.SetFloat("DirX",0);
				anim.SetFloat ("DirY", 1f);
			}
		} else if (down) 
		{
			transform.Translate ((-Vector2.up/vel) * Time.deltaTime);
			enableX = false;
			if(!enableX)
			{
				anim.SetFloat("DirX",0);
			anim.SetFloat("DirY",-1f);
			}
		}
		}
	}

	void comandi()
	{
		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			right = true;
			left = false;
			up = false;
			down = false;
			enableX = true;
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			right = false;
			left = true;
			up = false;
			down = false;
			enableX = true;
		}
		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			up = true;
			down = false;
			left = false;
			right = false;
			enableX = false;
		}
		if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			down = true;
			up = false;
			left = false;
			right = false;
			enableX = false;
		}
	}


	void OnTriggerEnter2D(Collider2D collider)
	{

		if(collider.gameObject.tag == "punti")
		{
			source.PlayOneShot(mangia,1f);
			GameObject.Find("Scena").GetComponent<Pacman>().score += 20;
			GameObject.Find("Scena").GetComponent<Pacman>().puntiTot++;
			Destroy(collider.gameObject);
		}

		if(collider.gameObject.tag == "Bpunti")
		{
			source.PlayOneShot(mangia,1f);
			GameObject.Find("Scena").GetComponent<Pacman>().score += 100;
			GameObject.Find("Scena").GetComponent<Pacman>().puntiTot++;
			Destroy(collider.gameObject);
			StartCoroutine(CambiaSprite());
		}

		if(collider.gameObject.tag == "fantasma" && SonoMangiabili)
		{
			source.PlayOneShot(eatghost,1f);
			GameObject.Find("Scena").GetComponent<Pacman>().score += 1000;

			if(collider.gameObject.name == "Blinky")
			{
				collider.GetComponent<BoxCollider2D>().enabled = false;
				collider.GetComponent<GhostMovement>().cur = collider.GetComponent<GhostMovement>().tappa;
				collider.GetComponent<GhostMovement>().transform.position = collider.GetComponent<GhostMovement>().PosIniziale;
				collider.GetComponent<SpriteRenderer>().sprite = blinky;
			}
			if(collider.gameObject.name == "Speedy")
			{
				collider.GetComponent<BoxCollider2D>().enabled = false;
				collider.GetComponent<GhostMovement>().cur = collider.GetComponent<GhostMovement>().tappa;
				collider.GetComponent<GhostMovement>().transform.position = collider.GetComponent<GhostMovement>().PosIniziale;
				collider.GetComponent<SpriteRenderer>().sprite = speedy;
			}
			if(collider.gameObject.name == "Inky")
			{
				collider.GetComponent<BoxCollider2D>().enabled = false;
				collider.GetComponent<GhostMovement>().cur = collider.GetComponent<GhostMovement>().tappa;
				collider.GetComponent<GhostMovement>().transform.position = collider.GetComponent<GhostMovement>().PosIniziale;
				collider.GetComponent<SpriteRenderer>().sprite = inky;
			}
			if(collider.gameObject.name == "Clyde")
			{
				collider.GetComponent<BoxCollider2D>().enabled = false;
				collider.GetComponent<GhostMovement>().cur = collider.GetComponent<GhostMovement>().tappa;
				collider.GetComponent<GhostMovement>().transform.position = collider.GetComponent<GhostMovement>().PosIniziale;
				collider.GetComponent<SpriteRenderer>().sprite = clyde;
			}
		}


	}
		IEnumerator CambiaSprite()
		{
		SonoMangiabili = true;
			GameObject.Find("Inky").GetComponent<SpriteRenderer>().sprite = mangiabile;
			GameObject.Find("Blinky").GetComponent<SpriteRenderer>().sprite = mangiabile;
			GameObject.Find("Speedy").GetComponent<SpriteRenderer>().sprite = mangiabile;
			GameObject.Find("Clyde").GetComponent<SpriteRenderer>().sprite = mangiabile;
			yield return new WaitForSeconds(7f);
			GameObject.Find("Inky").GetComponent<SpriteRenderer>().sprite = inky;
			GameObject.Find("Blinky").GetComponent<SpriteRenderer>().sprite = blinky;
			GameObject.Find("Speedy").GetComponent<SpriteRenderer>().sprite = speedy;
			GameObject.Find("Clyde").GetComponent<SpriteRenderer>().sprite = clyde;
		GameObject.Find("Inky").GetComponent<BoxCollider2D>().enabled = true;
		GameObject.Find("Blinky").GetComponent<BoxCollider2D>().enabled = true;
		GameObject.Find("Speedy").GetComponent<BoxCollider2D>().enabled = true;
		GameObject.Find("Clyde").GetComponent<BoxCollider2D>().enabled = true;
		SonoMangiabili = false;
		}

		
	}

