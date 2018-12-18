using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortableBomb : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void anmiDestroy()
	{
		Destroy(gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "enemyBullet" || other.gameObject.tag == "BulletReflecter")
		{
			Destroy(other.gameObject);
		}
	}
}
