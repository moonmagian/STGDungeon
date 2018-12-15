using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float highSpeed;
    public float lowSpeed;
    public GameObject playerModel;
    private Rigidbody rb;
    private Camera c;

    private CardCore cc;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        cc = GetComponent<CardCore>();
    }

    void FixedUpdate()
    {
        int h = 0;
        int v = 0;
        bool inLowSpeed = false;
        if (Input.GetKey(KeyCode.RightArrow)) h = 1;
        else if (Input.GetKey(KeyCode.LeftArrow)) h = -1;
        if (Input.GetKey(KeyCode.UpArrow)) v = 1;
        else if (Input.GetKey(KeyCode.DownArrow)) v = -1;
        if (Input.GetKey(KeyCode.LeftShift)) inLowSpeed = true;
        float speed = !inLowSpeed ? highSpeed : lowSpeed;
        Vector3 speedVec = new Vector3(0, 0, 0);
        if (v != 0)
        {
            speedVec.z = v;
        }

        if (h != 0)
        {
            speedVec.x = h;
        }

        if (Input.GetKey(KeyCode.Z))
        {
            if (inLowSpeed)
            {
                GetComponent<PlayerBulletLSGenerator>().genEnabled = true;
                GetComponent<PlayerBulletGenerator>().genEnabled = false;
            }
            else
            {
                GetComponent<PlayerBulletGenerator>().genEnabled = true;
                GetComponent<PlayerBulletLSGenerator>().genEnabled = false;
            }
        }
        else
        {
            GetComponent<PlayerBulletGenerator>().genEnabled = false;
            GetComponent<PlayerBulletLSGenerator>().genEnabled = false;
        }

        speedVec = speedVec.normalized * speed;
        rb.velocity = speedVec;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            cc.nextSelect();
            cc.cardArrow.GetComponents<AudioSource>()[0].Play();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            cc.nextSelect(false);
            cc.cardArrow.GetComponents<AudioSource>()[0].Play();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            cc.use(cc.selecting);
        }
    }
}