using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        SpawnObject = TokenObjects[Random.Range(0, TokenObjects.Length)];
        nextTreeXValue = 5;
        Spawn();
    }

    void Spawn()
    {
        if (GameStateManager.GameState == GameState.Playing)
        {
            GenerateCoin();
        }
        Invoke("Spawn", Random.Range(timeMin, timeMax));
    }

    void GenerateCoin() {
        // if you want to do stuff with it, save
        // it as type 'GameObject go = ...'
        SpawnObject = TokenObjects[Random.Range(0, TokenObjects.Length)];
        GameObject go = Instantiate(SpawnObject, this.transform.position + new Vector3(coinCount*4, 0, 0), Quaternion.identity) as GameObject;
        coinCount++;

        // next line puts it at the same rendering
        // layer as the bird (before the trees)
        // GetComponent<Renderer>().sortingLayerName = "Flappy";
    }

    private GameObject SpawnObject;
    public GameObject[] TokenObjects;

    public float timeMin = 2f;
    public float timeMax = 2f;
    public float coinCount = 0;

    private float nextTreeXValue;
}
