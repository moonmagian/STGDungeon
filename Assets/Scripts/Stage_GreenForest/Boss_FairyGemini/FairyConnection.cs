using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyConnection : MonoBehaviour {
	public static int STATUS_STOPPED = 0;
	public static int STATUS_RUNNING = 1;
	public int[] fairyStatus = new int[2] { 1, 1 };
	public float[] fairyLastStopTime = new float[2] { 0, 0 };
	public float[] fairyStopDuration = new float[2] { 10, 10 };
	public int bossStep = 0;
	public float[,] stepHP = new float[2, 3] { { 300, 300, 400 }, { 300, 300, 400 } };
	public float[,] currentHP;
	// Use this for initialization
	void Start () {
		currentHP = (float[,])stepHP.Clone();
	}
	public void setFairyStop(int n) {
		fairyStatus[n] = 0;
		fairyLastStopTime[n] = Time.time;
	}
	// Update is called once per frame
	void Update () {
		if(fairyStatus[0] == STATUS_STOPPED && fairyStatus[1] == STATUS_STOPPED) {
			bossStep++;
			fairyStatus[1] = STATUS_RUNNING;
		    fairyStatus[0] = STATUS_RUNNING;
		}
		if(fairyStatus[0] == STATUS_STOPPED && fairyStatus[1] == STATUS_RUNNING ) {
			if(Time.time - fairyLastStopTime[0] >= fairyStopDuration[0]) {
				fairyStatus[0] = STATUS_RUNNING;
				currentHP[0, bossStep] = stepHP[0, bossStep];
			}
		}
		if (fairyStatus[1] == STATUS_STOPPED && fairyStatus[0] == STATUS_RUNNING)
        {
            if (Time.time - fairyLastStopTime[1] >= fairyStopDuration[1])
            {
                fairyStatus[1] = STATUS_RUNNING;
                currentHP[1, bossStep] = stepHP[1, bossStep];
            }
        }
	}
}
