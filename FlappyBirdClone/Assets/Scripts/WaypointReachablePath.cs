using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointReachablePath : MonoBehaviour {
    public Transform[] waypoints;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Transform[] GetReachablePaths()
    {
        return waypoints;
    }
}
