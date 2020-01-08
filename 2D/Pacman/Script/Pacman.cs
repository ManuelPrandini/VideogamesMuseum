using UnityEngine;
using System.Collections;

public class Pacman : MonoBehaviour {

	public int score = 0;//indica il punteggio a schermo
	public int lives = 4;//vite pacman
	public GUIStyle stile,numeri;//stile delle scritte della GUI
	public Texture2D[] vite;//array contenente le texture delle vite
	bool coin;//controlla se sono stati inseriti i soldi
	GameObject player1;//indica il giocatore
	public bool giocoiniziato;//controlla se il gioco è partito
	public AudioSource source; // componente audio
	public AudioClip coinsound,startgame,mortoSound,MusicWin; // clip dell'inserimento dei soldi
	public bool morto = false;//controlla se pacman muore
	public int puntiTot = 0;//controlla se sono stati mangiati tutti i pallini
	public bool vittoria = false;

	//funzione di Start
	void Start () 
	{
		coin = false;
		giocoiniziato = false;
		Time.timeScale = 0;
		// prende gli elementi AudioSource del gameobject relativo a questo script e li carica in source
		source = GetComponent<AudioSource>();
	
	}
	
	//funzione che aggiorna gli stati ad ogni frame
	void Update () 
	{
		//se non ci sono i soldi e si preme invio...
		if(!coin && Input.GetKeyDown(KeyCode.Return))
		{
		coin = true;//abilita i soldi
		source.PlayOneShot(coinsound,1f);//riproduce suono soldi
			StartCoroutine(AspettaPrimadiIniziare());//tempo di attesa prima di iniziare la partita
		}

		//se pacman muore... 
		if(morto)
			StartCoroutine(spawnaPacman());//spawna pacman 

		//se si hanno zero vite...
		if(lives == 0)
		{
			Time.timeScale = 0;//ferma il gioco

			//se si preme R...
			if(Input.GetKeyDown(KeyCode.R))
				Application.LoadLevel("Pacman");//riavvia la scena

			//altrimenti se si preme ESC...
			else if(Input.GetKeyDown(KeyCode.Escape))
			{
				Time.timeScale = 1;//aziona il gioco
				Application.LoadLevel("Salagiochi");//ritorna al museo
			}
		}

		//se tutti i pallini sono stati mangiati...
		if(puntiTot == 166 && !vittoria)
		{

			vittoria = true;//abilita vittoria
			source.PlayOneShot(MusicWin,1f);//musica vittoria
			StartCoroutine(Wait());//serve per non mandare in loop la musica della vittoria
		}

		//se si vince...
		if (vittoria) 
		{
			//se si preme R...
			if(Input.GetKeyDown(KeyCode.R))
				Application.LoadLevel("Pacman");//riavvia la scena

			//altrimenti se si preme ESC...
			else if(Input.GetKeyDown(KeyCode.Escape))
			{
				Time.timeScale = 1;//aziona il gioco
				Application.LoadLevel("Salagiochi");//ritorna al museo
			}
		}


		//se si preme ESC mentre si sta giocando....
		if(Input.GetKeyDown(KeyCode.Escape))
		{
		Time.timeScale = 1;//aziona il gioco
		Application.LoadLevel("Salagiochi");//ritorna al museo
		}

	}

	//abilita le scritte a schermo
	void OnGUI()
	{
		GUI.Label(new Rect(50,50,50,50),"SCORE >>>",stile);//scritta punteggio a schermo
		GUI.Label(new Rect(225,55,50,50),""+score,numeri);//punteggio a schermo
		GUI.Label(new Rect(50,675,50,50),"LIVES ",stile);//scritta vita a schermo
		//se non ci sono soldi...
		if(!coin)
			GUI.Label(new Rect(Screen.width/2 -100,Screen.height/2,50,50),"INSERT COIN",stile);//scritta di inserimento monete
		//se sono stati inseriti i soldi... e il gioco deve iniziare...
		if(coin && !giocoiniziato)
			GUI.Label(new Rect(Screen.width/2 -100,Screen.height/2,50,50),"--READY!!!--",stile);//scritta del READY

		for (int i = 0; i < lives; i++)
			GUI.DrawTexture (new Rect (40 * (i+2), 700, 50, 25), vite [i]);//disegna texture vite a schermo

		//se si perdono tutte le vite...
		if(lives == 0)
		{
			GUI.Label(new Rect(Screen.width/2 -(50*2),Screen.height/2,50,50),"--GAME OVER!!!--",stile);//scritta GAME OVER
			GUI.Label(new Rect(Screen.width/2 -(100*2),Screen.height/2 + 50,90,50),"PREMI  ' R '  PER  RIGIOCARE",stile);
		}	
		//se si vince....
		if(vittoria)
		{
			GUI.Label(new Rect(Screen.width/2 -50,Screen.height/2,50,50),"--YOU WIN!!!--",stile);//scritta vittoria
			GUI.Label(new Rect(Screen.width/2 -(100*2),Screen.height/2 + 50,90,50),"PREMI  ' R '  PER  RIGIOCARE",stile);
		}
	}

	//routine che parte dopo l'inserimento delle monete
	IEnumerator AspettaPrimadiIniziare()
	{
		Time.timeScale = 1;
		source.PlayOneShot(startgame,1f);//musica iniziale
		yield return new WaitForSeconds (4f);//dopo 4 secondi...
		giocoiniziato = true;//abilita il gioco
	}

	//routine di respawn pacman
	IEnumerator spawnaPacman()
	{
		yield return new WaitForSeconds(3f);//aspetta tre secondi
		Respawn();//respawna il giocatore
	}

	//serve per non mandare il loop la musica
	IEnumerator Wait()
	{
		yield return new WaitForSeconds (5f);//aspetta 5 secondi
		Time.timeScale = 0;
		Time.timeScale = 1;
	}

	//funzione di respawn giocatore
	void Respawn()
	{
		if(morto && lives > 0)//rispawna solo se morto e se le vite sono maggiori di zero
		{
			GameObject player = Resources.Load<GameObject>("Pacman"); //assegna a player, il gameobject Pacman
			player1 = (GameObject) Instantiate(player);//istanzia il nuovo giocatore
			morto = false; // vivo
		}
	}
}
