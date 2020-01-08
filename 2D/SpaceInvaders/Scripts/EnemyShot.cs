using UnityEngine;
using System.Collections;

public class EnemyShot : MonoBehaviour {

	float probSparo;
	GameObject bulletAlien,bulletAlien1;
	void Update () 
	{
		Shot();
	}

	void Shot()
	{
		if(GameObject.FindWithTag("MainCamera").GetComponent<Manager>().coin == true || GameObject.FindGameObjectWithTag("Elimina").GetComponent<Invasione>().invasa == true)
		{
		probSparo = Random.Range(1f,100f);
		if(probSparo < 1.1f && GameObject.FindWithTag("MainCamera").GetComponent<Manager>().life != 0)
		{	
			bulletAlien = Resources.Load<GameObject>("SparoAlieno");
			Instantiate(bulletAlien,gameObject.transform.position,Quaternion.identity);
		}
		}
	}


}
