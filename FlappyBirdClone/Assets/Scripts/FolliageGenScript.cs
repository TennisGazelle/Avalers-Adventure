using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolliageGenScript : AbstractGenerator {
    public GameObject[] Bushes;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RemoveUnnecessaryElements();
	}

    void SpawnBushes() {
        Invoke("SpawnBushes", Random.Range(0.2f, 2.0f));
    }

}
