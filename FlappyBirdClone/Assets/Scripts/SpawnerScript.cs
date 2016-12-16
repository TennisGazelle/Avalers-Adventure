using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        SpawnObject = SpawnObjects[Random.Range(0, SpawnObjects.Length)];
        Spawn();
    }

    void Spawn()
    {
        if (GameStateManager.GameState == GameState.Playing)
        {
            //random y position
            float y = Random.Range(0f, 0f);
            GameObject go = Instantiate(SpawnObject, this.transform.position + new Vector3(coinCount*4, y, 0),
            Quaternion.identity) as GameObject;
            coinCount++;
            //GetComponent<Renderer>().sortingLayerName = "Flappy";

        }
        Invoke("Spawn", Random.Range(timeMin, timeMax));
    }

    private GameObject SpawnObject;
    public GameObject[] SpawnObjects;

    public float timeMin = 2f;
    public float timeMax = 2f;
    public float coinCount = 0;
}
