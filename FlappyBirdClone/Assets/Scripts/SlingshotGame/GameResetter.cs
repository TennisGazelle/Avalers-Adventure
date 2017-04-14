using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class GameResetter : MonoBehaviour {
    
    //reset speeds 
    public float resetSpeed = 1.0f;
    public float resetSpeedSqr;

    // asteroid attributes
    private Rigidbody2D rb;
    private Transform astTransform;
    private Vector3 originalAstPosition;
    private Quaternion originalAstRotation;
    private float rotationResetSpeed = 1.0f;

    // house attributes
    public GameObject [] houseStructures = new GameObject[9];

    private Transform [] originalHouseTransform;
    private Vector3 [] originalHousePosition;
    private Quaternion [] originalHouseRotation;

    private int houseCounter;
    private Quaternion currentHouseOriginalRotation;

    // bool to reset target
    public bool targetReset;

    // Test

    private Quaternion test;
    GameObject testGO;

    

	// Use this for initialization
	void Start () {
        // get asteroid properties
        rb = GetComponent<Rigidbody2D>();
        astTransform = GetComponent<Transform>();
        originalAstPosition = new Vector3(astTransform.position.x, astTransform.position.y, astTransform.position.z);

        originalAstRotation = astTransform.rotation;

        resetSpeedSqr = resetSpeed * resetSpeed;

        houseCounter = 0;

        // create all structure originals
        for (int ndx = 0; ndx < houseStructures.Length; ndx++)
        {
            // make 
            originalHouseTransform[ndx] = houseStructures[ndx].transform;
            originalHousePosition[ndx] = houseStructures[ndx].transform.position;
            originalHouseRotation[ndx] = houseStructures[ndx].transform.rotation;
        }

        // test
        targetReset = false;

        test = houseStructures[0].transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {

        if (GameObject.Find("AsteroidEmpty").GetComponent<ProjectileDragging>().isShot)
        {
            if (rb.velocity.sqrMagnitude < resetSpeedSqr)
            {
                Reset();
                houseCounter++;
            }
        }
	}

    void Reset()
    {
        // reset ball
        astTransform.position = originalAstPosition;
        astTransform.rotation = Quaternion.Slerp(astTransform.transform.rotation, originalAstRotation, Time.time * rotationResetSpeed);

        // repopulate house 
        Destroy(houseStructures[houseCounter]);
        houseStructures[houseCounter+1].SetActive(true);

        // change shot flag 
        GameObject.Find("AsteroidEmpty").GetComponent<ProjectileDragging>().isShot = false;

        // trigger target reset
        targetReset = true;
    }
}
