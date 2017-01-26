using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolliageGenScript : AbstractGenerator {
    public GameObject[] LightBushes;
    public GameObject[] DarkBushes;
    public GameObject[] LittleLightBushes;

    private float timeMin = 0.2f;
    private float timeMax = 1.0f;

    // Use this for initialization
    void Start () {
        SpawnBushes();
	}
	
	// Update is called once per frame
	void Update () {
        RemoveUnnecessaryElements();
	}

    void SpawnBushes() {
        SpawnBushesAt(); //params: array to build, where to build (change in X)
        Invoke("SpawnBushes", Random.Range(timeMin, timeMax));
    }

}
