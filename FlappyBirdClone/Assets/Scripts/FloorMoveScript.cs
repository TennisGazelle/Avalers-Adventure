using UnityEngine;
using System.Collections;

public class FloorMoveScript : MonoBehaviour
{
    public GameObject[] FrontTrees;
    public GameObject[] MiddleTrees;
    public GameObject[] BackTrees;

    public GameObject Destroyer;

    public Transform treeScaling;

    private Vector3 frontTreePosition = new Vector3(4, 0, -5);
    private Vector3 midTreePosition = new Vector3(2, 0, -3);
    private Vector3 backTreePosition = new Vector3(3, 0, -2);

    private float timeMin = 0.2f;
    private float timeMax = 1.0f;

    private ArrayList AllElements = new ArrayList();

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

        foreach (GameObject go in AllElements) {
            if (go.transform.position.x < Destroyer.transform.position.x) {
                Destroy(go);
                AllElements.Remove(go);
            }
        }
    }

    void SpawnTrees() {
        SpawnTreesAt(FrontTrees, ref frontTreePosition, 2.0f);
        SpawnTreesAt(MiddleTrees, ref midTreePosition, Random.Range(1.2f,2.0f));
        SpawnTreesAt(BackTrees, ref backTreePosition, Random.Range(0.8f,1.8f));

        Invoke("SpawnTrees", Random.Range(timeMin, timeMax));
    }

    void SpawnTreesAt(GameObject[] arrayToRange, ref Vector3 newTreePosition, float changeInX) {
        // make sure that we don't create too many trees out there
        if (newTreePosition.x + changeInX < System.Math.Abs(transform.position.x)+20) {
            GameObject obj = arrayToRange[Random.Range(0, arrayToRange.Length)];
            GameObject go = Instantiate(obj, newTreePosition, Quaternion.identity, treeScaling) as GameObject;
            AllElements.Add(go);
            newTreePosition.x += changeInX;
        }
    }
}
