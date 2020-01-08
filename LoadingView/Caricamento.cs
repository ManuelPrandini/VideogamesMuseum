using UnityEngine;
using System.Collections;

public class Caricamento : MonoBehaviour {

	public Texture2D barrabianca;
	public Texture2D barrarossa;
	public int orizzontale,verticale,larghezza,altezza;
	AsyncOperation operation;
	float caricamento;
	bool stop = false;
	// Use this for initialization
	void Awake()
	{
		caricamento = 0;
		stop = false;
	}
	void Start () 
	{

			StartCoroutine(loadAsync("Salagiochi"));
	}
	
	// Update is called once per frame
	void Update () 
	{
		StartCoroutine (Loading ());
	}

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(Screen.width/2-(larghezza/2),Screen.height-altezza-90,larghezza,altezza), barrabianca);
		GUI.DrawTexture(new Rect(Screen.width/2-(larghezza/2),Screen.height-altezza-90,caricamento,altezza), barrarossa);
	}

	IEnumerator Loading()
	{
		yield return new WaitForSeconds(1.09f);
		if(caricamento < larghezza)
			caricamento = caricamento + (larghezza/410)*2.2f;
	}

	private IEnumerator loadAsync(string levelName)
	{
		yield return new WaitForSeconds (3f);
		operation = Application.LoadLevelAsync(levelName);
		while(!operation.isDone) {
			yield return operation.isDone;
			Debug.Log("loading progress: " + operation.progress);
		}
		Debug.Log("load done");
		stop = true;
	}
}
