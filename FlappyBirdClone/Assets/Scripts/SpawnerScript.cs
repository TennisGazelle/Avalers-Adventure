using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerScript : MonoBehaviour
{
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
        SpawnedObjects.Add(Instantiate(SpawnObject, GetComponent<BalloonMovement>().currentWaypoint, false));
        coinCount++;
    }

    private GameObject SpawnObject;
    private List<GameObject> SpawnedObjects;
    public GameObject[] TokenObjects;

    private Transform nextLocation;
    private Vector3 spawningLocation;
    private BalloonMovement bm;

    public float timeMin = 2f;
    public float timeMax = 2f;
    public float coinCount = 0;
}
