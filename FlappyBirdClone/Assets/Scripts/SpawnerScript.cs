using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerScript : MonoBehaviour {
    private GameObject SpawnObject;
    private List<GameObject> SpawnedObjects;
    public GameObject[] TokenObjects;

    private Transform nextLocation;
    private Vector3 spawningLocation;
    private BalloonMovement bm;

    public float timeMin = 2f;
    public float timeMax = 2f;
    public float coinCount = 0;
    public float lowSpawnHeight = 5;
    public float medSpawnHeight = 20f;
    public float highSpawnHeight = 35f;

    private float spawnDistance = 50.0f;
    private float mendelsohnSpawnDist = 10f;

    private GameMode gameMode = GameMode.Typical;

    // Use this for initialization
    void Start()
    {
        SpawnedObjects = new List<GameObject>();
        foreach (GameObject g in TokenObjects) {
            g.transform.localScale = new Vector3(50, 50, 50);
            g.transform.position = new Vector3(0, 0, 0);
        }
        SpawnObject = TokenObjects[Random.Range(0, TokenObjects.Length)];
        SetGameMode(GameSettingsControl.Instance.mode);
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
       switch(gameMode)
        {
            case GameMode.Mendelsohn:
                SpawnMendelsohn(waypoint);
                break;
            case GameMode.Typical:
                SpawnTypical(waypoint);
                break;
            case GameMode.Random:
                SpawnRandom(waypoint);
                break;
        }
     
    }

    private void SpawnMendelsohn(Transform waypoint)
    {
        Vector3 origin = GetComponent<Transform>().position;
        float distance = Vector3.Distance(origin, waypoint.position);
        float mendelsohnDist = Mathf.Min(distance, 60f);
        int numToSpawn = (int)(distance / mendelsohnSpawnDist);
        int numToSpawnMendelsohn = (int)(mendelsohnDist / mendelsohnSpawnDist);
        Vector3 direction = waypoint.position - origin;


        for (int i = 1; i < numToSpawn + 1; i++)
        {
            if (numToSpawn - i + numToSpawnMendelsohn/2 > numToSpawnMendelsohn 
                && numToSpawn - i - numToSpawnMendelsohn/2 < numToSpawnMendelsohn )
            {
                Vector3 sub = new Vector3(origin.x + direction.x / numToSpawn * i, waypoint.position.y + lowSpawnHeight, origin.z + direction.z / numToSpawn * i);
                SpawnedObjects.Add(Instantiate(TokenObjects[2], sub, this.transform.rotation));
                sub = new Vector3(origin.x + direction.x / numToSpawn * i, waypoint.position.y + medSpawnHeight, origin.z + direction.z / numToSpawn * i);
                SpawnedObjects.Add(Instantiate(TokenObjects[1], sub, this.transform.rotation));
                sub = new Vector3(origin.x + direction.x / numToSpawn * i, waypoint.position.y + highSpawnHeight, origin.z + direction.z / numToSpawn * i);
                SpawnedObjects.Add(Instantiate(TokenObjects[0], sub, this.transform.rotation));
            }
        }
    }

    private void SpawnTypical(Transform waypoint)
    {
        Vector3 origin = GetComponent<Transform>().position;
        float distance = Vector3.Distance(origin, waypoint.position);
        int numToSpawn = (int)(distance / spawnDistance);
        Vector3 direction = waypoint.position - origin;

        for (int i = 1; i < numToSpawn + 1; i++)
        {
            Vector3 sub = new Vector3(origin.x + direction.x / numToSpawn * i, waypoint.position.y + lowSpawnHeight, origin.z + direction.z / numToSpawn * i);
            SpawnedObjects.Add(Instantiate(TokenObjects[2], sub, this.transform.rotation));
            sub = new Vector3(origin.x + direction.x / numToSpawn * i, waypoint.position.y + medSpawnHeight, origin.z + direction.z / numToSpawn * i);
            SpawnedObjects.Add(Instantiate(TokenObjects[1], sub, this.transform.rotation));
            sub = new Vector3(origin.x + direction.x / numToSpawn * i, waypoint.position.y + highSpawnHeight, origin.z + direction.z / numToSpawn * i);
            SpawnedObjects.Add(Instantiate(TokenObjects[0], sub, this.transform.rotation));
        }
    }

    private void SpawnRandom(Transform waypoint)
    {
        Vector3 origin = GetComponent<Transform>().position;
        float distance = Vector3.Distance(origin, waypoint.position);
        int numToSpawn = (int)(distance / spawnDistance);
        Vector3 direction = waypoint.position - origin;

        for (int i = 1; i < numToSpawn + 1; i++)
        {
            Vector3 sub = new Vector3(origin.x + direction.x / numToSpawn * i, waypoint.position.y + lowSpawnHeight, origin.z + direction.z / numToSpawn * i);
            if (Random.Range(0, 4) != 1)
            {
                SpawnedObjects.Add(Instantiate(TokenObjects[Random.Range(0, 3)], sub, this.transform.rotation));
                SpawnedObjects[SpawnedObjects.Count - 1].GetComponent<RotateAndBobScript>().RandomizeMovement();
            }
            if (Random.Range(0, 4) != 1)
            {
                sub = new Vector3(origin.x + direction.x / numToSpawn * i, waypoint.position.y + medSpawnHeight, origin.z + direction.z / numToSpawn * i);
                SpawnedObjects.Add(Instantiate(TokenObjects[Random.Range(0,3)], sub, this.transform.rotation));
                SpawnedObjects[SpawnedObjects.Count - 1].GetComponent<RotateAndBobScript>().RandomizeMovement();
            }
            if (Random.Range(0, 4) != 1)
            {
                sub = new Vector3(origin.x + direction.x / numToSpawn * i, waypoint.position.y + highSpawnHeight, origin.z + direction.z / numToSpawn * i);
                SpawnedObjects.Add(Instantiate(TokenObjects[Random.Range(0, 3)], sub, this.transform.rotation));
                SpawnedObjects[SpawnedObjects.Count - 1].GetComponent<RotateAndBobScript>().RandomizeMovement();
            }
        }
    }

    public void RemoveLastSet()
    {

    }

    public void SetGameMode(GameMode mode)
    {
        gameMode = mode;
    }
}
