using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public Transform Player;
    Vector3 offset;

	// Use this for initialization
	void Awake () {
        offset =  transform.position - Player.position;
	}


	void LateUpdate () {
		transform.position = new Vector3(transform.position.x, offset.y, transform.position.z);
        //transform.LookAt(Player.transform);
    }


    
}
