﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyOneBehaviour : MonoBehaviour {

	private bool died;
    public Material onHitMaterial;
	public FairyConnection connection;
    private float lastHit;
    // Use this for initialization
    void Start()
    {
        died = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastHit >= 0.05f)
        {
            GetComponent<MeshRenderer>().materials[1].color = new Color(1f, 1f, 1f, 0f);
        }
		if(connection.fairyStatus[0] == 1 && connection.bossStep == 0) {
			transform.position = new Vector3(2.4f * Mathf.Cos(3 * Time.time), 5f, 1.5f + 2.0f * Mathf.Sin(3 * Time.time));
		} 
        // GetComponent<MeshRenderer>().materials[1] = null;
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "playerBullet")
		{
			if(connection.fairyStatus[0] == FairyConnection.STATUS_RUNNING && connection.bossStep == 0) {
				
				connection.currentHP[0, 0] -= coll.gameObject.GetComponent<PlayerBulletBehaviour>().damage;
                GetComponent<MeshRenderer>().materials[1].color = new Color(1f, 1f, 1f, 0.5f);
                lastHit = Time.time;
				if(connection.currentHP[0, 0]  <= 0) {
					connection.currentHP[0, 0] = 0;
					connection.setFairyStop(0);
				}
                // died |= life <= 0;
			}
			Destroy(coll.gameObject);

        }
        if (died)
        {
            Destroy(gameObject);
        }
    }
}
