using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointReachablePath : MonoBehaviour {
    public Transform[] waypoints;
    LineRenderer line;

#if UNITY_EDITOR
    // Use this for initialization
    void Start () {
        line = GetComponent<LineRenderer>();
        line.SetPosition(0, transform.position);
        if (waypoints[0] != null)
            line.SetPosition(1, waypoints[0].position);
	}
#endif
    // Update is called once per frame


    void Update () {

	}


    public Transform[] GetReachablePaths()
    {
        return waypoints;
    }


}
