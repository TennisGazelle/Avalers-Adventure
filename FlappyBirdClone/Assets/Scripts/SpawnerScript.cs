using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerScript : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        foreach (GameObject g in TokenObjects) {
            g.transform.localScale = new Vector3(50, 50, 50);
            g.transform.position = new Vector3(0, 0, 0);
        }
        SpawnObject = TokenObjects[Random.Range(0, TokenObjects.Length)];
    }

    void Update() {
        DestroyCoin();
    }

    bool checkDistance(Vector3 left, Vector3 right) {
        return Vector3.Distance(left, right) < 1;
    }

    void GenerateCoin() {
        SpawnObject = TokenObjects[Random.Range(0, TokenObjects.Length)];
        SpawnedObjects.Add(Instantiate(SpawnObject, GetComponent<BalloonMovement>().currentWaypoint, false));
        coinCount++;
    }

    void DestroyCoin() {
        // check to see if we're close to any of the coins,
        // and if so, destroy it
        for (int i = 0; i < SpawnedObjects.Count; i++) {
            if (checkDistance(transform.position, SpawnedObjects[i].transform.position)) {
                Destroy(SpawnedObjects[i]);
            }
        }
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
