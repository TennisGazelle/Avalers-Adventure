using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour {

    
    private float spawnDistance = 30.0f;
    public GameObject cameraObject; 

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x + 15 < cameraObject.transform.position.x){
            RepositionBackground();
        }
	}

    private void RepositionBackground(){
        Vector2 groundOffset = new Vector2(spawnDistance, 0);
        transform.position = (Vector2)transform.position + groundOffset;
    }
}
