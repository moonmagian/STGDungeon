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
    private float stime;
    // Use this for initialization
    void Start()
    {
        dtime1 = 0;
        dtime2 = 0;
        stime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        dtime1 += Time.deltaTime;
        dtime2 += Time.deltaTime;
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
    }
}
