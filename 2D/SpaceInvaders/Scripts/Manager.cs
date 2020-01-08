using UnityEngine;
using System.Collections;

/*
 * Classe principale che gestisce Space Invaders
 * attaccata alla MainCamera
*/

public class Manager : MonoBehaviour {
	
	public int score = 0;// punteggio giocatore
	public int life = 4;// vite giocatore
	public bool morto = false;// controllo morte giocatore
	public int rimasti ; // conta i nemici rimasti
	public GameObject[] nemici; // array contenente i nemici
	public Texture2D[] vita;//vettore che contiene le texture per le vite
	public bool coin;//controlla se ci sono i soldi
	GameObject carro1,mothership; // istanza del respawn del giocatore
	private AudioSource source,sourcealiena,sourcemothership; // componente audio
	public AudioClip coinsound; // clip dell'inserimento dei soldi
	public AudioClip invasione; // clip invasione
	public GUIStyle scritte;//stile delle scritte della GUI
	float numMothership;//variabile che contiene il numero Random per decidere se spawnare la navicella rossa
	bool firstMothership = false;//controlla se è stata programmata la prima navicella rossa della partita
	public bool staPassando;//indica se la navicella rossa è attiva


	void Start () 
	{
		coin = false;//non ci sono soldi
		nemici = GameObject.FindGameObjectsWithTag("Alieno"); //inserisce elementi nell array nemici dalla hierarchy
		rimasti = nemici.Length; // assegna a rimasti il numero dei componenti dell'array
		Time.timeScale = 0;//gioco in pausa e non attivo

		// prende gli elementi AudioSource del gameobject relativo a questo script e li carica in source
		source = GetComponent<AudioSource>();
		//prende gli elementi AudioSource del gameobject "Sfondo" e li carica in sourcealiena
		sourcealiena = GameObject.FindGameObjectWithTag ("Sfondo").GetComponent<AudioSource> ();
		staPassando = false; // controlla se sta passando la navicella rossa
	}


	void Update () 
	{
		//se premi ESC... torni al museo
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Time.timeScale = 1;
		   Application.LoadLevel("Salagiochi");
		}

		//se non ci sono i soldi, e premi INVIO...
		if(!coin && Input.GetKeyDown(KeyCode.Return))
		{
			source.PlayOneShot(coinsound,1f);//attiva suono soldi
			coin = true;//i soldi sono inseriti
			Time.timeScale = 1;//attiva il gioco(Play)
		}

		// se il gioco è attivo, avvia la funzione di programmazione navicelle rosse
		if(coin)
			ScheduleMothership ();


		Respawn(); // Respawna il giocatore

		//controllo gameover: se le vite del giocatore arrivano a zero o se si eliminano tutti i nemici...
		if(rimasti == 0 || life == 0 || GameObject.FindGameObjectWithTag("Elimina").GetComponent<Invasione>().invasa == true)
		{
			Time.timeScale = 0;//ferma il gioco
			if(Input.GetKeyDown(KeyCode.R))//se premi il tasto R...
			{
				//ricarica il livello e attiva il gioco momentaneamente in pausa
			   Application.LoadLevel("SpaceInvaders");
				Time.timeScale = 1;
			}
			//altrimenti se premi ESC torni al museo
			else if(Input.GetKeyDown(KeyCode.Escape))
			{
				Time.timeScale = 1;
				Application.LoadLevel("Salagiochi");
			}
		}

	}

	/*
	 * Funzione che abilita la GUI
	 * */
	void OnGUI()
	{
		GUI.Label(new Rect(50,50,500,500),"SCORE <"+score+">",scritte);//punteggio a schermo
		GUI.Label(new Rect(50,710,50,50),"LIVES :",scritte);//vite a schermo
		for (int i = 0; i < life; i++)
			GUI.DrawTexture (new Rect (100 * (i+2), 710, 50, 25), vita [i]);//disegna le texture delle vite

		//se non si hanno più vite, o se gli alieni hanno raggiunto la soglia che determina l'invasione....
		if(life == 0 || GameObject.FindGameObjectWithTag("Elimina").GetComponent<Invasione>().invasa == true)
		{
			GUI.Label(new Rect(Screen.width/2 -100,Screen.height/2,50,50),"GAME OVER",scritte);//scritta Game Over
			//scritta che ti indica di premere R per rigiocare
			GUI.Label(new Rect(Screen.width/2 -(100*2),Screen.height/2 + 50,90,50),"PREMI  ' R '  PER  RIGIOCARE",scritte);
		}

		//se tutti gli alieni sono stati uccisi...
		if(rimasti == 0)
		{
			GUI.Label(new Rect(Screen.width/2 -100,Screen.height/2,50,50),"HAI  VINTO ! ! !",scritte);//scritta per indicarti che hai vinto
			GUI.Label(new Rect(Screen.width/2 -(100*2),Screen.height/2 + 50,90,50),"PREMI  ' R '  PER  RIGIOCARE",scritte);
		}

		//se non ci sono soldi...
		if(!coin)
			GUI.Label(new Rect(Screen.width/2 -100,Screen.height/2,50,50),"INSERT COIN",scritte);//scritta di inserimento monete
	}


	//funzione di respawn giocatore
	void Respawn()
	{
		if(morto && life > 0)//rispawna solo se morto e se le vite sono maggiori di zero
		{
			GameObject carro = Resources.Load<GameObject>("Tank"); //assegna a carro, il gameobject tank
			carro1 = (GameObject) Instantiate(carro);//istanzia il nuovo giocatore
			morto = false; // vivo
			StartCoroutine(Vulnerability()); // aspetta tot secondi prima di ritornare vulnerabile

		}
	}

	IEnumerator Vulnerability() // tempo di invulnerabilità
	{
		GameObject.Find("Tank(Clone)").GetComponent<BoxCollider2D>().enabled = false;//disabilita il box collider
		yield return new WaitForSeconds (3f);//dopo 3 secondi...
		GameObject.Find("Tank(Clone)").GetComponent<BoxCollider2D>().enabled = true; //abilita il box collider
	}


	//programma l'arrivo di una navicella rossa
	void ScheduleMothership()
	{
	if(!staPassando)
		numMothership =	Random.Range (1, 500);//numero random per dire se spawnare una navicella
		if (numMothership < 2 && firstMothership == false) 
		{
			mothership = Resources.Load<GameObject>("mothership");//ricerca il gameobject da istanziare
			Instantiate (mothership,new Vector3(-64.7f,34f,-0.1f), Quaternion.identity);//istanzia l'oggetto
			firstMothership = true;//indica che la prima navicella è stata istanziata
			staPassando = true;//indica che è attiva
		}
			//se la navicella non è attiva, e se la prima navicella è già stata istanziata...
			// esegui le istruzioni dell'if sopra
			if (numMothership < 2 && staPassando== false && firstMothership) 
			{
				mothership = Resources.Load<GameObject> ("mothership");
				Instantiate (mothership, new Vector3 (-64.7f, 34f, -0.1f), Quaternion.identity);
			staPassando = true;
			}

	}
}
