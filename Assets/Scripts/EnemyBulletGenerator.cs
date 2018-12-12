using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletGenerator : MonoBehaviour {
	public float generateDuration1;
	public float generateDuration2;
	public int generateStatus = 1;
	public GameObject bulletType1;
	public GameObject bulletType2;
	private float dtime1;
	private float dtime2;
	private float stime;
	// Use this for initialization
	void Start () {
		dtime1 = 0;
		dtime2 = 0;
		stime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		dtime1 += Time.deltaTime;
		dtime2 += Time.deltaTime;
		if(dtime1 >= generateDuration1 && generateStatus != 0) {
			dtime1 = 0;
			// For rotating bullet
			//GameObject blt1 = Instantiate(bulletType1, transform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
			//GameObject blt2 = Instantiate(bulletType1, transform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
			//GameObject blt3 = Instantiate(bulletType1, transform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
			//GameObject blt4 = Instantiate(bulletType1, transform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
			//blt1.GetComponent<Rigidbody>().velocity = 10f * new Vector3(Mathf.Sin(stime - Time.time), 0f, Mathf.Cos(stime - Time.time));
			//blt2.GetComponent<Rigidbody>().velocity = 10f * new Vector3(Mathf.Sin(stime - Time.time - 0.5f * Mathf.PI), 0f, Mathf.Cos(stime - Time.time - 0.5f * Mathf.PI));
			//blt3.GetComponent<Rigidbody>().velocity = 10f * new Vector3(Mathf.Sin(stime - Time.time - 1.0f * Mathf.PI), 0f, Mathf.Cos(stime - Time.time - 1.0f * Mathf.PI));
			//blt4.GetComponent<Rigidbody>().velocity = 10f * new Vector3(Mathf.Sin(stime - Time.time - 1.5f * Mathf.PI), 0f, Mathf.Cos(stime - Time.time - 1.5f * Mathf.PI));
			GameObject blt;
			Vector3 posVec = transform.position;
			for (int i = 0; i != 10; ++i) {
				posVec.y = 4f;
				blt = Instantiate(bulletType1, posVec, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
				blt.GetComponent<Rigidbody>().velocity = 10f * new Vector3(Mathf.Sin(stime - Time.time - 1f / 5f * Mathf.PI * i), 0f, Mathf.Cos(stime - Time.time - 1f / 5f * Mathf.PI * i));
			}
			// For random bullet
			// transform.Rotate(new Vector3(0f, 1f, 0f), )
			// Bullet Generate behaviour here
		}
		if(dtime2 >= generateDuration2 && generateStatus != 0) {
			dtime2 = 0;
			GameObject blt1 = Instantiate(bulletType2, transform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
			blt1.GetComponent<Rigidbody>().velocity = 8f * new Vector3(Mathf.Cos(Random.Range(1.25f * Mathf.PI, 1.75f * Mathf.PI)), 0f, Mathf.Sin(Random.Range(1.25f * Mathf.PI, 1.75f * Mathf.PI)));
		}
	}
}
