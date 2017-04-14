using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMovement : MonoBehaviour {

    private Rigidbody rb;
    private Vector3 winningForce;
    public ReceieveUDPStream stream;
    public Transform currentWaypoint;
    public float moveSpeed = 5f;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody>();
        winningForce = new Vector3(0,5000,0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // transform.RotateAround(Vector3.zero, Vector3.up, 2.5f * Time.deltaTime);
        MoveToWaypoint();
        if (Input.GetKeyDown("space") || stream.hasTypicalHappened())
        {
            rb.AddForce(winningForce, ForceMode.Force);
        }
        if (transform.position.y < 4.4f) {
            transform.position += new Vector3(0, transform.position.y*0.01f, 0);
        }
    }

    void OnMouseDown()
    {
        rb.AddForce(winningForce, ForceMode.Force);
    }

    void MoveToWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.transform.position, 10 * Time.deltaTime);

        if (Vector3.Distance(currentWaypoint.transform.position, transform.position) < 1)
        {
            GetNewWaypoint();
        }
    }

    void GetNewWaypoint()
    {
        currentWaypoint = currentWaypoint.GetComponent<WaypointReachablePath>().GetReachablePaths()[0];
        GetComponent<SpawnerScript>().Invoke("GenerateCoin", 0.0f);
    }
}
