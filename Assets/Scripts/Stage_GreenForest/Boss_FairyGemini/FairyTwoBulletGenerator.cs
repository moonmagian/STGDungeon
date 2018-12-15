using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyTwoBulletGenerator : MonoBehaviour
{

    public float generateDuration1;
    public float generateDuration2;
    public int generateStatus = 1;
    public GameObject bulletType1;
    public GameObject bulletType2;
    public FairyConnection connection;
    private float dtime1;
    private float dtime2;
    public float stime;

    private Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        dtime1 = 0;
        dtime2 = 0;
        stime = Time.time;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        dtime1 += Time.deltaTime;
        dtime2 += Time.deltaTime;
        if (connection.fairyStatus[1] == 1 && connection.bossStep == 0)
        {
            rb.velocity = new Vector3(2 * Mathf.Sin(-3*(Time.time - stime)) * 3, 0, 1.3f * Mathf.Cos(-3*(Time.time - stime)) * 3);
        }
        if (connection.fairyStatus[1] == 0 && connection.bossStep == 0)
        {
            rb.velocity = new Vector3();
        }
        if (dtime1 >= generateDuration1 && generateStatus != 0 && connection.fairyStatus[1] == FairyConnection.STATUS_RUNNING && connection.bossStep == 0)
        {
            dtime1 = 0;
            GameObject blt;
            Vector3 posVec = transform.position;
            for (int i = 0; i != 10; ++i)
            {
                posVec.y = 3f;
                blt = Instantiate(bulletType1, posVec, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
                blt.GetComponent<Rigidbody>().velocity = 3f * new Vector3(Mathf.Sin(stime - Time.time - 1f / 5f * Mathf.PI * i), 0f, Mathf.Cos(stime - Time.time - 1f / 5f * Mathf.PI * i));
            }
        }
        if (dtime1 >= 0.38f && generateStatus != 0 && connection.fairyStatus[1] == FairyConnection.STATUS_RUNNING && connection.bossStep == 1)
        {
            dtime1 = 0;
            GameObject blt;
            Vector3 posVec = transform.position;
            for (int i = 0; i != 6; ++i)
            {
                posVec.y = 3f;
                blt = Instantiate(bulletType1, posVec, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
                blt.GetComponent<Rigidbody>().velocity = 3f * new Vector3(Mathf.Sin(stime - Time.time - 1f / 3f * Mathf.PI * i), 0f, Mathf.Cos(stime - Time.time - 1f / 
3f * Mathf.PI * i));
            }
        }
        if (dtime1 >= 0.5f && generateStatus != 0 && connection.fairyStatus[1] == FairyConnection.STATUS_RUNNING && connection.bossStep == 2)
        {
            dtime1 = 0;
            GameObject blt;
            Vector3 posVec = transform.position;
            for (int i = 0; i != 3; ++i)
            {
                posVec.y = 4f;
                blt = Instantiate(bulletType2, posVec, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
                blt.GetComponent<Rigidbody>().velocity = 2.5f * new Vector3(Mathf.Sin(stime - Time.time - 2f / 3f * Mathf.PI * i), 0f, Mathf.Cos(stime - Time.time - 2f / 
3f * Mathf.PI * i));
            }
        }
    }
}
