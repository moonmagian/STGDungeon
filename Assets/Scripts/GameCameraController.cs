using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraController : MonoBehaviour {
	public float baseWidth = 1600;
    public float baseHeight = 900;
    public float baseOrthographicSize = 5;

    void Awake()
    {      
        float newOrthographicSize = (float)Screen.height / (float)Screen.width * this.baseWidth / this.baseHeight * this.baseOrthographicSize;
        GetComponent<Camera>().orthographicSize = Mathf.Max(newOrthographicSize, this.baseOrthographicSize);
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
