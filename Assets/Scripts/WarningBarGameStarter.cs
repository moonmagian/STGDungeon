using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp.Assets.Scripts;
using UnityEngine;

public class WarningBarGameStarter : MonoBehaviour
{
	public GameObject fairy1;

	public GameObject fairy2;

	public GameObject player;

	public GameObject fconnection;
	// Use this for initialization
	void Start ()
	{
		fairy1.GetComponent<FairyOneBulletGenerator>().enabled = false;
		fairy1.GetComponent<FairyOneBehaviour>().enabled = false;
		fairy2.GetComponent<FairyTwoBehaviour>().enabled = false;
		fairy2.GetComponent<FairyTwoBulletGenerator>().enabled = false;
		fconnection.GetComponent<FairyConnection>().enabled = false;	
		player.GetComponent<PlayerBehaviour>().enabled = false;
		player.GetComponent<CardCore>().enabled = false;
		player.GetComponent<PlayerBuffManager>().enabled = false;
	}

	void startGame()
	{
		
		fairy1.GetComponent<FairyOneBulletGenerator>().enabled = true;
		fairy1.GetComponent<FairyOneBehaviour>().enabled = true;
		fairy2.GetComponent<FairyTwoBehaviour>().enabled = true;
		fairy2.GetComponent<FairyTwoBulletGenerator>().enabled = true;
		fconnection.GetComponent<FairyConnection>().enabled = true;	
		player.GetComponent<PlayerBehaviour>().enabled = true;
		player.GetComponent<CardCore>().enabled = true;
		player.GetComponent<PlayerBuffManager>().enabled = true;
		fconnection.GetComponent<AudioSource>().Play();
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
