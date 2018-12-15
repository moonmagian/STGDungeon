using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBasicBahaviour : MonoBehaviour {
    // All bullets should extend this script to work properly
    // Destorys bullet when its out of screen to release resources.
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "bulletBorder") {
			// print("test");
			Destroy(gameObject);
			
		}
	}
}
