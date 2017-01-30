using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolliageGenScript : AbstractGenerator
{

    public GameObject[] FrontTrees;
    public GameObject[] MiddleTrees;
    public GameObject[] BackTrees;

    public GameObject[] LightBushes;
    public GameObject[] DarkBushes;
    public GameObject[] LittleLightBushes;

    private float timeMin = 0.2f;
    private float timeMax = 1.0f;

    public Transform treeScaling;

    private Vector3 frontTreePosition = new Vector3(4, 0, -5);
    private Vector3 midTreePosition = new Vector3(2, 0, -3);
    private Vector3 backTreePosition = new Vector3(3, 0, -2);

    // Use this for initialization
    void Start()
    {
        SpawnTrees();
        SpawnBushes();
    }

    // Update is called once per frame
    void Update()
    {
        RemoveUnnecessaryElements();
    }

    void SpawnTrees()
    {
        SpawnElementAt(FrontTrees, ref frontTreePosition, 2.0f, treeScaling);
        SpawnElementAt(MiddleTrees, ref midTreePosition, Random.Range(1.2f, 2.0f), treeScaling);
        SpawnElementAt(BackTrees, ref backTreePosition, Random.Range(0.8f, 1.8f), treeScaling); 

        Invoke("SpawnTrees", Random.Range(timeMin, timeMax));
    }

    void SpawnBushes()
    {
        SpawnBushesAt(LightBushes);
        SpawnBushesAt(DarkBushes);
        SpawnBushesAt(LittleLightBushes);

        Invoke("SpawnBushes", Random.Range(timeMin, timeMax));
    }

}