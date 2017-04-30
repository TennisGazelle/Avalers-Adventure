using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerScript : MonoBehaviour
{
    private GameObject SpawnObject;
    private List<GameObject> SpawnedObjects;
    public GameObject[] TokenObjects;

    private Transform nextLocation;
    private Vector3 spawningLocation;
    private BalloonMovement bm;

    public float timeMin = 2f;
    public float timeMax = 2f;
    public float coinCount = 0;

    private float spawnDistance = 10.0f;

    // Use this for initialization
    void Start()
    {
        SpawnedObjects = new List<GameObject>();
        foreach (GameObject g in TokenObjects) {
            g.transform.localScale = new Vector3(50, 50, 50);
            g.transform.position = new Vector3(0, 0, 0);
        }
        SpawnObject = TokenObjects[Random.Range(0, TokenObjects.Length)];
    }

    void Update() {

    }

    bool checkDistance(Vector3 left, Vector3 right) {
        return Vector3.Distance(left, right) < 1;
    }

    void GenerateCoin() {
        SpawnObject = TokenObjects[Random.Range(0, TokenObjects.Length)];
        Transform wp = GetComponent<BalloonMovement>().currentWaypoint;
        Transform spawnLoc = wp;
        spawnLoc.position = new Vector3(wp.position.x, wp.position.y + 20f, wp.position.z);
        SpawnedObjects.Add(Instantiate(SpawnObject, spawnLoc, false));
        coinCount++;
    }

    public void SetSpawnDistance(float amount)
    {
        spawnDistance = amount;
    }

    public void SpawnNextSet(Transform waypoint)
    {
        Vector3 origin = GetComponent<Transform>().position;
        float distance = Vector3.Distance(origin, waypoint.position);

    }

    public void RemoveLastSet()
    {

    }
}
