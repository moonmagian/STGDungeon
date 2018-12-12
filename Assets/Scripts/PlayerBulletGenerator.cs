using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletGenerator : MonoBehaviour {
	public bool genEnabled = false;
	public GameObject bullet;
	private float bgenTime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(genEnabled) {
			bgenTime += Time.deltaTime;
			if (bgenTime >= 0.1f)
			{
				// Change code here to generate different kinds of bullets.
				GameObject blt = Instantiate(bullet, transform.position + bullet.transform.position, Quaternion.Euler(0f, 180f, 0f));
				GameObject blt2 = Instantiate(bullet, transform.position + bullet.transform.position, Quaternion.Euler(0f, 180f, 0f));
				GameObject blt3 = Instantiate(bullet, transform.position + bullet.transform.position, Quaternion.Euler(0f, 180f, 0f));
				blt.transform.Translate(new Vector3(-0.3f, 0f));
				blt2.transform.Translate(new Vector3(0.3f, 0f));
				blt.GetComponent<Rigidbody>().velocity = new Vector3(3f, 0f, 20.0f);
				blt3.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 20.0f);
				blt2.GetComponent<Rigidbody>().velocity = new Vector3(-3f, 0f, 20.0f);
				bgenTime = 0;
			}
			} else {
			bgenTime = 0.1f - 1e-8f;
		}
	}
}
