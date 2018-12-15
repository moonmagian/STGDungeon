using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyConnection : MonoBehaviour
{
    public int[] fairyStatus = new int[2] {1, 1};
    public float[] fairyLastStopTime = new float[2] {0, 0};
    public float[] fairyStopDuration = new float[2] {10, 10};
    public int bossStep = 0;
    public float[,] stepHP = new float[2, 3] {{450, 600, 600}, {450, 600, 600}};

    public float[,] currentHP;
    public TextMesh spellname;
    public GameObject enemyLifeBar1;
    public GameObject enemyLifeBar2;
    public Rigidbody fairy1;
    public Rigidbody fairy2;
    public ParticleSystem bgp;
    public MeshRenderer bg;
    public static int STATUS_STOPPED = 0;
    public static int STATUS_RUNNING = 1;
    public Color madbgColor = new Color(0.7264f, 0.2055f, 0.3077f);

    private float[] lifeBarInitScale = new float[2] {0, 0};

    // Use this for initialization
    void Start()
    {
        currentHP = (float[,]) stepHP.Clone();
        lifeBarInitScale[0] = enemyLifeBar1.transform.localScale.x;
        lifeBarInitScale[1] = enemyLifeBar2.transform.localScale.x;
    }

    public void setFairyStop(int n)
    {
        fairyStatus[n] = 0;
        fairyLastStopTime[n] = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        switch (bossStep)
        {
            case 0:
                spellname.text = "「The Shining Gemini」";
                break;
            case 1:
                spellname.text = "「Gravity and Meteor Shower」";
                break;
            case 2:
                spellname.text = "「Spring Echo」";
                break;
        }

        if (bossStep == 2)
        {
            bgp.startColor = Color.Lerp(bgp.startColor, new Color(1f, 0f, 0f, 0.6f), 0.2f);
            bg.material.color = Color.Lerp(bg.material.color, madbgColor, 0.01f);
        }

        Vector3 f1Pos = new Vector3(0.74f, 5f, 1.5f);
        Vector3 f2Pos = new Vector3(-0.66f, 5f, 1.5f);

        enemyLifeBar1.transform.localScale =
            new Vector3(lifeBarInitScale[0] * currentHP[0, bossStep] / stepHP[0, bossStep],
                enemyLifeBar1.transform.localScale.y, enemyLifeBar1.transform.localScale.z);
        enemyLifeBar2.transform.localScale =
            new Vector3(lifeBarInitScale[1] * currentHP[1, bossStep] / stepHP[1, bossStep],
                enemyLifeBar2.transform.localScale.y, enemyLifeBar2.transform.localScale.z);
        if (fairyStatus[0] == STATUS_STOPPED && fairyStatus[1] == STATUS_STOPPED)
        {
            if (bossStep == 1 || bossStep == 0)
            {
                if (bossStep == 1)
                {
                    bgp.startColor = Color.Lerp(bgp.startColor, new Color(1f, 0f, 0f, 0.6f), 0.2f);
                    bg.material.color = Color.Lerp(bg.material.color, madbgColor, 0.01f);
                }

                bool nextStep = true;
                if ((fairy1.transform.position - f1Pos).magnitude >= 0.01f)
                {
                    fairy1.velocity = f1Pos - fairy1.transform.position;
                    nextStep = false;
                }
                else
                {
                    fairy1.velocity = new Vector3();
                }

                if ((fairy2.transform.position - f2Pos).magnitude >= 0.01f)
                {
                    fairy2.velocity = f2Pos - fairy2.transform.position;
                    nextStep = false;
                }
                else
                {
                    fairy2.velocity = new Vector3();
                }

                if (nextStep)
                {
                    ++bossStep;
                    fairyStatus[1] = STATUS_RUNNING;
                    fairyStatus[0] = STATUS_RUNNING;
                }
            }
        }

        if (fairyStatus[0] == STATUS_STOPPED && fairyStatus[1] == STATUS_RUNNING)
        {
            if (Time.time - fairyLastStopTime[0] >= fairyStopDuration[0])
            {
                if ((fairy1.transform.position - f1Pos).magnitude >= 0.01f)
                {
                    fairy1.velocity = 3f * (f1Pos - fairy1.transform.position);
                }
                else
                {
                    fairy1.velocity = new Vector3();
                    fairyStatus[0] = STATUS_RUNNING;
                    currentHP[0, bossStep] = stepHP[0, bossStep];
                    fairy1.GetComponent<FairyOneBulletGenerator>().stime = Time.time;
                }
            }
        }

        if (fairyStatus[1] == STATUS_STOPPED && fairyStatus[0] == STATUS_RUNNING)
        {
            if (Time.time - fairyLastStopTime[1] >= fairyStopDuration[1])
            {
                if ((fairy2.transform.position - f2Pos).magnitude >= 0.01f)
                {
                    fairy2.velocity = (f2Pos - fairy2.transform.position) * 3f;
                }
                else
                {
                    fairy2.velocity = new Vector3();
                    fairyStatus[1] = STATUS_RUNNING;
                    currentHP[1, bossStep] = stepHP[1, bossStep];
                    fairy2.GetComponent<FairyTwoBulletGenerator>().stime = Time.time;
                }
            }
        }
    }
}