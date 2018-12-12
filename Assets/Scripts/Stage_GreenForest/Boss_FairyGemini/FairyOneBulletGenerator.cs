using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class FairyOneBulletGenerator : MonoBehaviour
{
    public float generateDuration1;
    public float generateDuration2;
    public int generateStatus = 1;
    public GameObject bulletType1;
    public GameObject bulletType2;
    public FairyConnection connection;
    private float dtimeS1B1;
    private float dtimeS2B2;
    private float dtimeS2B1;
    private bool dtimeS2B2enabled = false;
    private float stime;

    public Timer bulletTimer1;

    // Use this for initialization
    void Start()
    {
        dtimeS1B1 = 0;
        dtimeS2B1 = 0;
        dtimeS2B2 = 0;
        stime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        dtimeS1B1 += Time.deltaTime;
        dtimeS2B1 += Time.deltaTime;
        if (dtimeS1B1 >= generateDuration1 && generateStatus != 0 &&
            connection.fairyStatus[0] == FairyConnection.STATUS_RUNNING && connection.bossStep == 0)
        {
            GameObject blt;
            Vector3 posVec = transform.position;
            for (int i = 0; i != 10; ++i)
            {
                posVec.y = 4f;
                blt = Instantiate(bulletType1, posVec, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
                blt.GetComponent<Rigidbody>().velocity =
                    7f * new Vector3(Mathf.Sin(stime - Time.time - 1f / 5f * Mathf.PI * i), 0f,
                        Mathf.Cos(stime - Time.time - 1f / 5f * Mathf.PI * i));
            }

            dtimeS1B1 = 0;
        }

        if (dtimeS2B1 >= 1f && dtimeS2B1 < 2f && generateStatus != 0 &&
            connection.fairyStatus[0] == FairyConnection.STATUS_RUNNING &&
            connection.bossStep == 1)
        {
            dtimeS2B2enabled = true;
        }

        if (dtimeS2B1 >= 2f && generateStatus != 0 && connection.fairyStatus[0] == FairyConnection.STATUS_RUNNING &&
            connection.bossStep == 1)
        {
            dtimeS2B2enabled = false;
            dtimeS2B1 = 0;
        }

        if (dtimeS2B2enabled)
        {
            dtimeS2B2 += Time.deltaTime;
            Transform playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
            Vector3 velocityDir = playerTrans.position - transform.position;
            velocityDir = velocityDir.normalized;
            GetComponent<Rigidbody>().velocity = 4f * velocityDir;
            if (dtimeS2B2 >= 0.2f)
            {
                Vector3 bulletDir = Vector3.Normalize(Vector3.Cross(velocityDir, new Vector3(0f, 1f, 0f)));
                Vector3 posVec = transform.position;
                posVec.y = 4f;
                GameObject blt = Instantiate(bulletType1, posVec, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
                blt.GetComponent<Rigidbody>().velocity = bulletDir;
                GameObject blt2 = Instantiate(bulletType1, posVec, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
                blt2.GetComponent<Rigidbody>().velocity = -bulletDir;
                dtimeS2B2 = 0;
            }
        }
        else if (connection.bossStep == 1)
        {
            GetComponent<Rigidbody>().velocity = new Vector3();
        }

    }
}