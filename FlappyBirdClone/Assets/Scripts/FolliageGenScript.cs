using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolliageGenScript : AbstractGenerator
{

    public GameObject[] frontTrees;
    public GameObject[] middleTrees;
    public GameObject[] backTrees;

    public GameObject[] frontTreeTop;
    public GameObject[] middleTreeTop;
    public GameObject[] backTreeTop;

    public GameObject[] lightBushes;
    public GameObject[] darkBushes;
    public GameObject[] littleLightBushes;

    public GameObject[] earth;

    private float timeMin = 0.2f;
    private float timeMax = 1.0f;

    public Transform treeScaling;

    private Vector3 frontTreePosition = new Vector3(4, 0, -5);
    private Vector3 midTreePosition = new Vector3(2, 0, -3);
    private Vector3 backTreePosition = new Vector3(3, 0, -2);

    private Vector3 frontTreeTopPosition = new Vector3(4, 2.3f, -6);
    private Vector3 midTreeTopPosition = new Vector3(2, 2.0f, -5);
    private Vector3 backTreeTopPosition = new Vector3(3, 1.7f, -4);

    private Vector3 lightBushPosition = new Vector3(3, -1.52f, -2);
    private Vector3 darkBushPosition = new Vector3(2, -1.62f, -4);
    private Vector3 littleLightBushPosition = new Vector3(4, -1.7f, -5);

    private Vector3 earthPosition = new Vector3(3, -2.3f, -4.5f);

    // Use this for initialization
    void Start()
    {
        SpawnTrees();
    }

    // Update is called once per frame
    void Update()
    {
        RemoveUnnecessaryElements();
    }

    void SpawnTrees()
    {
        SpawnElementAt(frontTrees, ref frontTreePosition, 2.0f, treeScaling);
        SpawnElementAt(middleTrees, ref midTreePosition, Random.Range(1.2f, 2.0f), treeScaling);
        SpawnElementAt(backTrees, ref backTreePosition, Random.Range(0.8f, 1.8f), treeScaling);

        SpawnElementAt(frontTreeTop, ref frontTreeTopPosition, 2.0f, treeScaling);
        SpawnElementAt(middleTreeTop, ref midTreeTopPosition, Random.Range(1.2f, 2.0f), treeScaling);
        SpawnElementAt(backTreeTop, ref backTreeTopPosition, Random.Range(0.8f, 1.8f), treeScaling);

        SpawnElementAt(lightBushes, ref lightBushPosition, 2.0f, treeScaling);
        SpawnElementAt(darkBushes, ref darkBushPosition, Random.Range(1.2f, 2.0f), treeScaling);
        SpawnElementAt(littleLightBushes, ref littleLightBushPosition, Random.Range(0.8f, 1.8f), treeScaling);

        SpawnElementAt(earth, ref earthPosition, 5.0f, treeScaling);

        //SpawnBushesAt(lightBushes, treeScaling);
        //SpawnBushesAt(darkBushes, treeScaling);
        //SpawnBushesAt(littleLightBushes, treeScaling);
        //SpawnGroundAt(Earth, treeScaling);

        Invoke("SpawnTrees", Random.Range(timeMin, timeMax));
    }
}
