using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Timers;
using UnityEngine;

public class FairyOneBulletGenerator : MonoBehaviour
{
    private static Vector3 UL = new Vector3(-4.5f, 5, 6);
    private static Vector3 UR = new Vector3(4.5f, 5, 6);
    private static Vector3 BR = new Vector3(4.5f, 5f, -7.24f);
    private static Vector3 BL = new Vector3(-4.5f, 5f, -7.24f);
    private static Vector3[] d3Poses = new Vector3[4] {UL, BL, BR, UR};
    public float generateDuration1;
    public float generateDuration2;
    public int generateStatus = 1;
    public GameObject bulletType1;
    public GameObject bulletType2;
    public FairyConnection connection;
    private float dtimeS1B1;
    private float dtimeS2B2;
    private float dtimeS2B1;
    private float dtimeS3B1;
    private float dtimeS3B2;
    private int d3posFlag;
    private bool dtimeS2B2enabled = false;
    private bool dtimeS3B1enabled;
    public float stime;
    public bool S3FirstStop;
    private Rigidbody rb;
    public Timer bulletTimer1;

    // Use this for initialization
    void Start()
    {
        S3FirstStop = true;
        dtimeS1B1 = 0;
        dtimeS2B1 = 0;
        dtimeS2B2 = 0;
        dtimeS3B1 = 0;
        stime = Time.time;
        d3posFlag = 0;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        dtimeS1B1 += Time.deltaTime;
        dtimeS2B1 += Time.deltaTime;
        dtimeS3B1 += Time.deltaTime;
        dtimeS3B2 += Time.deltaTime;
        if (connection.fairyStatus[0] == 1 && connection.bossStep == 0)
        {
            rb.velocity = new Vector3(-2 * Mathf.Sin(3 * (Time.time - stime)) * 3, 0,
                1.3f * Mathf.Cos(3 * (Time.time - stime)) * 3);
        }

        if (connection.fairyStatus[0] == 0 && connection.bossStep == 0)
        {
            rb.velocity = new Vector3();
        }

        if (dtimeS1B1 >= generateDuration1 && generateStatus != 0 &&
            connection.fairyStatus[0] == FairyConnection.STATUS_RUNNING && connection.bossStep == 0)
        {
            GameObject blt;
            float num = connection.fairyStatus[1] == 1 ? 10 : 20;
            float spd = connection.fairyStatus[1] == 1 ? 7 : 10;
            Vector3 posVec = transform.position;
            for (int i = 0; i != num; ++i)
            {
                posVec.y = 4f;
                blt = Instantiate(bulletType1, posVec, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
                blt.GetComponent<Rigidbody>().velocity =
                    spd * new Vector3(Mathf.Sin(stime - Time.time - 2f / num * Mathf.PI * i), 0f,
                        Mathf.Cos(stime - Time.time - 2f / num * Mathf.PI * i));
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

        if (connection.bossStep != 1 || generateStatus == 0 ||
            connection.fairyStatus[0] == FairyConnection.STATUS_STOPPED) dtimeS2B2enabled = false;
        if (dtimeS2B2enabled)
        {
            dtimeS2B2 += Time.deltaTime;
            Transform playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
            Vector3 velocityDir = playerTrans.position - transform.position;
            velocityDir.y = 0f;
            velocityDir = velocityDir.normalized;
            rb.velocity = 4f * velocityDir;
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
            rb.velocity = new Vector3();
        }

        if (connection.bossStep == 2 && connection.fairyStatus[0] == 1)
        {
            S3FirstStop = true;
            if ((transform.position - d3Poses[d3posFlag]).magnitude >= 0.1f)
            {
                rb.velocity = (d3Poses[d3posFlag] - transform.position) * 3f;
            }
            else if(!dtimeS3B1enabled)
            {
                dtimeS3B1enabled = true;
                dtimeS3B1 = 0;
            }
            if (dtimeS3B1enabled)
            {
                if (dtimeS3B1 <= 1.0f)
                {
                    if (dtimeS3B2 >= 0.3f)
                    {
                        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
                        Vector3 posVec = transform.position;
                        Vector3 bulletV = playerPos - posVec;
                        bulletV.y = 0;
                        bulletV = bulletV.normalized;
                        posVec.y = 4f;
                        GameObject blt = Instantiate(bulletType2, posVec, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
                        blt.GetComponent<Rigidbody>().velocity = 5f * bulletV;
                        GameObject blt2 = Instantiate(bulletType2, posVec, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
                        blt2.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(-25, Vector3.up) * bulletV * 5f;
                        GameObject blt3 = Instantiate(bulletType2, posVec, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
                        blt3.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(25, Vector3.up) * bulletV * 5f;
                        dtimeS3B2 = 0;
                    }
                }
                else
                {
                    dtimeS3B1enabled = false;
                    dtimeS3B1 = 0;
                    d3posFlag = d3posFlag == 3 ? 0 : d3posFlag + 1;
                }
            }
        }
        else if (connection.bossStep == 2 && connection.fairyStatus[0] == 0)
        {
            if (S3FirstStop)
            {
                rb.velocity = new Vector3();
                S3FirstStop = false;
            }
            dtimeS3B1 = 0;
            dtimeS3B1enabled = false;
            dtimeS3B2 = 0;
            d3posFlag = 0;
        }
    }
}