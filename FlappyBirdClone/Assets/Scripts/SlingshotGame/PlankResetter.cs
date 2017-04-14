using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankResetter : MonoBehaviour {

    public Rigidbody2D astRb;
    public float resetSpeed = 1.0f;
    public float resetSpeedSqr;

    private Transform plankTransform;
    private Vector3 originalPlankPosition;
    private Quaternion originalPlankRotation;
    private float rotationResetSpeed = 1.0f;

    // Use this for initialization
    void Start () {
        resetSpeedSqr = resetSpeed * resetSpeed;

        plankTransform = GetComponent<Transform>();
        originalPlankPosition = new Vector3(plankTransform.position.x, plankTransform.position.y, plankTransform.position.z);
        originalPlankRotation = plankTransform.rotation;
	}

    void Update()
    {
        if (astRb.velocity.sqrMagnitude < resetSpeedSqr)
            Reset();
    }

    void Reset()
    {
        plankTransform.position = originalPlankPosition;
        plankTransform.rotation = Quaternion.Slerp(plankTransform.transform.rotation, originalPlankRotation, Time.time * rotationResetSpeed);
    }
}
