using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class GameResetter : MonoBehaviour {

    public float resetSpeed = 1.0f;
    public float resetSpeedSqr;
    private Rigidbody2D rb;
    private Transform astTransform;
    private Vector3 originalAstPosition;
    private Quaternion originalAstRotation;
    private float rotationResetSpeed = 1.0f;

    private int houseCounter; 

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        astTransform = GetComponent<Transform>();
        originalAstPosition = new Vector3(astTransform.position.x, astTransform.position.y, astTransform.position.z);
        originalAstRotation = astTransform.rotation;

        resetSpeedSqr = resetSpeed * resetSpeed;

        houseCounter = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (rb.velocity.sqrMagnitude < resetSpeedSqr)
            Reset();
	}

    void Reset()
    {
        // reset ball
        astTransform.position = originalAstPosition;
        astTransform.rotation = Quaternion.Slerp(astTransform.transform.rotation, originalAstRotation, Time.time * rotationResetSpeed);
         
        // if house1 -> disable, reset location and enable house2

        // if house2 -> disable, reset location and enable house2

        // if house1 -> disable, reset location and enable house2

    }
}
