using UnityEngine;
using System.Collections;

public class FloorMoveScript : AbstractGenerator {

    public GameObject[] FrontTrees;
    public GameObject[] MiddleTrees;
    public GameObject[] BackTrees;

    public Transform treeScaling;

    private Vector3 frontTreePosition = new Vector3(4, 0, -5);
    private Vector3 midTreePosition = new Vector3(2, 0, -3);
    private Vector3 backTreePosition = new Vector3(3, 0, -2);

    private float timeMin = 0.2f;
    private float timeMax = 1.0f;

    // Use this for initialization
    void Start()
    {
        SpawnTrees();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x < -8f)
        {
            transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
        }
        transform.Translate(-Time.deltaTime, 0, 0);

        RemoveUnnecessaryElements();
    }

    void SpawnTrees() {
        SpawnElementAt(FrontTrees, ref frontTreePosition, 2.0f, treeScaling);
        SpawnElementAt(MiddleTrees, ref midTreePosition, Random.Range(1.2f,2.0f), treeScaling);
        SpawnElementAt(BackTrees, ref backTreePosition, Random.Range(0.8f,1.8f), treeScaling);

        Invoke("SpawnTrees", Random.Range(timeMin, timeMax));
    }
}
