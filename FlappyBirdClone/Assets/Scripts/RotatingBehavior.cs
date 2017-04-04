using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBehavior : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(Vector3.up * Time.deltaTime*5, Space.World);
	}
}
