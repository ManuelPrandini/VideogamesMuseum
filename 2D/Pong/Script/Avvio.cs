using UnityEngine;
using System.Collections;

public class Avvio : MonoBehaviour {

	bool pallapresente;	//booleana per controllare se la palla è presente nel campo
	bool giocoiniziato;//booleana per controllare se il gioco è iniziato e quindi, se uno dei due giocatori ha vinto un round
	bool winblu,winred;//booleana per controllare se vince il blu o il rosso
	bool gameover;
	public bool soldi;
	Rigidbody2D newPalla;//Oggetto di riferimento
	int angolazione,potenza;//indicano l'angolazione e la potenza della palla
	float battitura,sceltangolazione;//float di scelta per la battitura e per la scelta dell'angolazione della palla
	static int Giocatore1,Giocatore2;//PUNTEGGI PLAYERS
	public GUIStyle stilePunteggio;// font punteggio
	public GUIStyle stilePausa;//font scritte
	public bool pausa;// gioco in pausa
	private AudioSource source; // componente audio
	public AudioClip coin; // clip dell'inserimento dei soldi
	public AudioClip gameoversound; // clip gameover
	public Texture2D frecciasu,frecciagiu;

	void Awake ()
	{
		Giocatore1 = 0;
		Giocatore2 = 0;
		pausa = false;
		winblu = false;
		winred = false;
		pallapresente = false;
		giocoiniziato = false;
		soldi = false;
		gameover = false;
		source = GetComponent<AudioSource>();
	}

	void Start()
	{

	}

	//Funzione di aggiornamento e correzione tra un frame e l'altro
	void Update () 
	{

		if(soldi)
		{
			//se la palla non  è presente...
			if(!pallapresente)
			{
				StartCoroutine(spawnapalla());
				pallapresente = true;//setta a true il controllo della palla in campo
			}
		}
		
		
		if(Input.GetKeyDown(KeyCode.Return) && !soldi)
		{
			soldi = true;
			source.PlayOneShot(coin,1f);		
		}
		//se il gioco deve iniziare
		if(!giocoiniziato)
		{

			scelta();
			if(battitura < 0.5f)
				newPalla.AddForce(new Vector2(potenza,angolazione),ForceMode2D.Force);
			else if(battitura > 0.5f)
				newPalla.AddForce(new Vector2(-potenza,angolazione),ForceMode2D.Force);
			giocoiniziato = true;
		}

		//se il gioco è iniziato
		if(giocoiniziato)
		{
			//se ha vinto il blu
			if(winblu)
			{
				scelta();
				newPalla.AddForce(new Vector2(potenza,angolazione),ForceMode2D.Force);
				winblu = false;
			}

			//se ha vinto il rosso
			if(winred)
			{
				scelta();
				newPalla.AddForce(new Vector2(-potenza,angolazione),ForceMode2D.Force);
				winred = false;
			}
		}

		// RESET
		if(Input.GetKeyDown(KeyCode.R))
		{
			Reset();
		}

		//PAUSA
		if(Input.GetKeyDown(KeyCode.P))
		{
			if(!pausa)
			{
				Time.timeScale = 0;
				pausa = true;
			}
			else
			{
				Time.timeScale = 1;
				pausa = false;
			}
		}
		Vittoria();//funzione dei criteri di vittoria sempre in aggiornamento

		//se la palla non è più presente
		if(!pallapresente)
		{
			Start();//riavvia il gioco
		}
	}
	
	 
	/*
	 * Controlla i criteri di vittoria di entrambi i giocatori
	 * */
	void Vittoria()
	{
		//Se la palla oltrepassa la barretta rossa
		if(newPalla.position.x > 10.4)
		{
			winblu = true;//attiva la booleana di vittoria giocatore blu
			Giocatore1++;//incrementa lo score del giocatore blu
			Debug.Log("Giocatore1 vince!");
			Debug.Log("punteggio: " +Giocatore1+ " A " +Giocatore2); 
			newPalla.GetComponent<ControlloPalla>().Rimuovi();//Richiama la funzione per eliminare dalla hierarchy la palla che non serve più
			pallapresente = false;//setta a false la booleana per il controllo della palla in campo

		}
		//Se la palla oltrepassa la barretta blu
		if(newPalla.position.x < -10.4)
		{
			winred = true;//indica che vince il giocatore rosso
			Giocatore2++;//incrementa lo score del giocatore blu
			Debug.Log("Giocatore2 vince!");
			Debug.Log("punteggio: " +Giocatore1+ " A " +Giocatore2);
			newPalla.GetComponent<ControlloPalla>().Rimuovi();//Richiama la funzione per eliminare dalla hierarchy la palla che non serve più
			pallapresente = false;//setta a false la booleana per il controllo della palla in campo

		}
	
		//controllo vittoria e fine gioco
		if(Giocatore1 == 10 || Giocatore2 == 10)
		{
			gameover = true;
			soldi = false;
			Giocatore1 = 0;
			Giocatore2 = 0;
		}
		if(gameover)
		{
			source.PlayOneShot(gameoversound,1f);
			StartCoroutine(Wait());
		}

	}

	/*
	 *Funzione di alcune scelte di gioco 
	 * */ 
	void scelta()
	{

		battitura = Random.Range (0f,1f);//scelta di chi batte
		potenza = 750;
		sceltangolazione = Random.Range (0f,1f);//scelta dell'angolazione della pallina
		if(sceltangolazione < 0.5f)
		{
			angolazione = 650 * -1;
		}
		else if(sceltangolazione > 0.5f)
		{
			angolazione = 650;
		}

	}

	/*
	 * Punteggio a schermo
	 * */
	void OnGUI()
	{
		GUI.Label(new Rect(Screen.width/2 -100,Screen.height/4,0,0),""+Giocatore1,stilePunteggio);
		GUI.Label(new Rect(Screen.width/2 +100,Screen.height/4,0,0),""+Giocatore2,stilePunteggio);
		if(pausa)
			GUI.Label(new Rect(Screen.width/2 -200,Screen.height/2,100,100),"GIOCO IN PAUSA",stilePausa);
		if(gameover)
		{
			GUI.Label(new Rect(Screen.width/2 -200,Screen.height/2,100,100),"GAME OVER",stilePausa);
		}
		if(!soldi && !gameover)
			GUI.Label(new Rect(Screen.width/2 -200,Screen.height/2,100,100),"INSERT COIN",stilePausa);
	}
		
	//Prima di spawnare la palla aspetta tre secondi
	IEnumerator spawnapalla()
	{

		yield return new WaitForSeconds(3);//aspetta tre secondi
		spawn();//spawna la palla
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(2);//aspetta due secondi
		Time.timeScale = 0;
		Time.timeScale = 1;
		gameover = false;
	}
	

	/*
	 * Spawna la pallina
	 * */
	void spawn()
	{
		Rigidbody2D pallina = Resources.Load<Rigidbody2D>("Palla");
		newPalla = (Rigidbody2D) Instantiate(pallina);
	}

	/*
	 * funzione di reset
	 */
	void Reset()
	{
		newPalla.GetComponent<ControlloPalla>().Rimuovi();//Richiama la funzione per eliminare dalla hierarchy la palla che non serve più
		pallapresente = false;
		giocoiniziato = false;
	}

}
