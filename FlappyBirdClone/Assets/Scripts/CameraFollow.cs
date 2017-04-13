using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public float smoothing = 8f;
    Vector3 offset;
    public Transform center;
	// Use this for initialization
	void Awake () {
        offset =  transform.position - Player.position;
	}


	void FixedUpdate () {
        Vector3 targetCamPos = new Vector3(
            Player.position.x + offset.z
            , transform.position.y
            , Player.position.z + offset.z);
       
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        //float y = transform.eulerAngles.y;
        // transform.LookAt(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z));
        Vector3 lookat =  Player.transform.position - Player.GetComponent<BalloonMovement>().currentWaypoint.position;
        lookat.Normalize();

        transform.LookAt(Player.position + lookat);
    }


    public Transform Player;
}
