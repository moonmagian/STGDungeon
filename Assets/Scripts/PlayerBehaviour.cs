using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    // Use this for initialization
    private int hit = 0;
    public TextMesh HitText;

    void Start()
    {
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "enemyBullet" || coll.gameObject.tag == "BulletReflecter")
        {
            ++hit;
            Destroy(coll.gameObject);
            // print("test");
            print("DEATH! hit = " + hit);
            // TODO: Add Death Event.
        }
    }

    private void Update()
    {
        HitText.text = "Hit : " + hit.ToString();
    }

    public void instanceObj(GameObject prefab)
    {
        Instantiate(prefab, transform.position, new Quaternion());
    }
}