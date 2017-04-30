using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMovement : MonoBehaviour {

    private Rigidbody rb;
    private Vector3 winningForce;
    public ReceieveUDPStream stream;
    public Transform currentWaypoint;
    public float moveSpeed = 5f;
    public float upSpeed = 10f;

    float lowPosY;
    float highPosY;
	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody>();
        winningForce = new Vector3(0,5000,0);
        lowPosY = transform.position.y;
        highPosY = lowPosY + 10;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // transform.RotateAround(Vector3.zero, Vector3.up, 2.5f * Time.deltaTime);
        MoveToWaypoint();
        if (Input.GetKey("space") || stream.hasTypicalHappened())
        {
            //rb.AddForce(winningForce, ForceMode.Force);
            MoveHigh();
        }
        else
            MoveLow();

        Quaternion neededRotation = Quaternion.LookRotation(currentWaypoint.position - transform.position);
        Quaternion rot = new Quaternion(transform.rotation.x, neededRotation.y, transform.rotation.z, neededRotation.w);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 0.1f * Time.deltaTime);
    }

    void OnMouseDown()
    {
        rb.AddForce(winningForce, ForceMode.Force);
    }

    void MoveToWaypoint()
    {
		Vector3 newPos = new Vector3 (currentWaypoint.position.x, transform.position.y, currentWaypoint.position.z);
		transform.position = Vector3.MoveTowards(transform.position, newPos, moveSpeed * Time.deltaTime);

		if (Vector3.Distance(newPos, transform.position) < 1)
        {
            GetNewWaypoint();
        }
    }

    void MoveHigh()
    {
        if (transform.position.y < highPosY)
            transform.Translate(Vector3.up * Time.deltaTime * upSpeed, Space.Self);
    }

    void MoveLow()
    {
        if (transform.position.y > lowPosY)
            transform.Translate(Vector3.down * Time.deltaTime * upSpeed, Space.Self);
    }

    void GetNewWaypoint()
    {
        currentWaypoint = currentWaypoint.GetComponent<WaypointReachablePath>().GetReachablePaths()[0];
        GetComponent<SpawnerScript>().Invoke("GenerateCoin", 0.0f);
		//transform.LookAt (currentWaypoint);
    }
}
