using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        SpawnObject = TokenObjects[Random.Range(0, TokenObjects.Length)];
        Spawn();
    }

    void Spawn()
    {
        if (GameStateManager.GameState == GameState.Playing)
        {
            GenerateCoin();
        }
      //  Invoke("Spawn", Random.Range(timeMin, timeMax));
        Invoke("Spawn", 1.5f);
    }

    void GenerateCoin() {
        SpawnObject = TokenObjects[Random.Range(0, TokenObjects.Length)];
        Instantiate(SpawnObject, this.transform.position + new Vector3(coinCount * 4, 0, 0), Quaternion.identity);
        coinCount++;
    }

    private GameObject SpawnObject;
    public GameObject[] TokenObjects;

    public float timeMin = 2f;
    public float timeMax = 2f;
    public float coinCount = 0;
}
