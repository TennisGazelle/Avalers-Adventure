using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonBob : MonoBehaviour {

    public float strength = 1f;
    public float amplitude = 2f;
    float originalPosY;
    float offsetInY;
	// Use this for initialization
	void Awake () {
        offsetInY = 0;
	}
	
	// Update is called once per frame
	void Update () {
        // subtract the original transforming
        //transform.position -= new Vector3(0, offsetInY, 0);
        offsetInY = Mathf.Sin(Time.time * amplitude)*strength;
        //transform.position += new Vector3(0, offsetInY, 0);
	}
}
