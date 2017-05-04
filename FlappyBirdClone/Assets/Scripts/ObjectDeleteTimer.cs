using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeleteTimer : MonoBehaviour {
    public float timeToLive = 90f;

	// Use this for initialization
	void Awake () {
        Destroy(this.gameObject, timeToLive);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
