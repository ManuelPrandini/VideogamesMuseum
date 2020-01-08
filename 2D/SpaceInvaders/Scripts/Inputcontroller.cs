using UnityEngine;
using System.Collections;

public class Inputcontroller : MonoBehaviour {

	public float velocità;
	GameObject bullet;
	public bool can;
	private AudioSource source; // componente audio // da spostare su qualcosa di statico tipo lo sfondo
	public AudioClip shotsound; // clip dell'inserimento dei soldi

	// Use this for initialization
	void Start () 
	{
		can = true;
		source = GameObject.FindGameObjectWithTag("Sfondo").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.RightArrow) && transform.position.x < 26.5f)
			transform.Translate(Vector2.right * velocità * Time.deltaTime);

	if(Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -26.5f)
			transform.Translate(-Vector2.right * velocità * Time.deltaTime);

	if(Input.GetKeyDown(KeyCode.Space) && can)
		{
			bullet = Resources.Load<GameObject>("Sparo");
			source.PlayOneShot(shotsound,1f);
			Instantiate(bullet,transform.position,Quaternion.identity); 
			StartCoroutine(wait());
		}
	}

	IEnumerator wait()
	{
		can = false;
		yield return new WaitForSeconds (0.5f);
		can = true;
	}


}
