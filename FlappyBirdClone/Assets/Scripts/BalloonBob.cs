using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonBob : MonoBehaviour {

    public float strength = 1f;
    public float amplitude = 2f;
    float originalPosY;
	// Use this for initialization
	void Awake () {
        originalPosY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        pos.y = (Mathf.Sin(Time.time * amplitude) * strength)+ originalPosY;
        transform.position = pos;
	}
}
