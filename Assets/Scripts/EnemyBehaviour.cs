using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
	private bool died;
	public float life;
	public Material onHitMaterial;
	private float lastHit;
	// Use this for initialization
	void Start () {
		died = false;
	}

	// Update is called once per frame
	void Update () {
		if (Time.time - lastHit >= 0.05f)
		{
			GetComponent<MeshRenderer>().materials[1].color = new Color(1f, 1f, 1f, 0f);
		}
		// GetComponent<MeshRenderer>().materials[1] = null;
	}
	void OnTriggerEnter(Collider coll)
	{
        if (coll.gameObject.tag == "playerBullet")
        {
			// print("test");
			// print("life" + life);
			life -= coll.gameObject.GetComponent<PlayerBulletBehaviour>().damage;
			GetComponent<MeshRenderer>().materials[1].color = new Color(1f, 1f, 1f, 0.5f);
			lastHit = Time.time;
			Destroy(coll.gameObject);
			died |= life <= 0;
        }
		if(died) {
			Destroy(gameObject);
		}
    }
}
