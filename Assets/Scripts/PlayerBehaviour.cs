using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
	// Use this for initialization
	private int hit = 0;
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "enemyBullet")
        {
			++hit;
			Destroy(coll.gameObject);
			// print("test");
			print("DEATH! hit = " + hit);
			// TODO: Add Death Event.
        }
    }
}
