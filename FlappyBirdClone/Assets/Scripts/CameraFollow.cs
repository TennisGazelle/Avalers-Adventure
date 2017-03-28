using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public float smoothing = 100f;
    Vector3 offset;
	// Use this for initialization
	void Awake () {
        cameraZ = transform.position.z;
        offset = transform.position - Player.position;
	}

    float cameraZ;


	void Update () {
        Vector3 targetCamPos = new Vector3(
            Player.position.x + offset.x
            , transform.position.y
            , Player.position.z + offset.z);
        transform.RotateAround(Vector3.zero, Vector3.up, 2.5f * Time.deltaTime);
        //transform.position = new Vector3(Player.position.x + 1.5f, 0, cameraZ);
        // transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        //float y = transform.eulerAngles.y;
       // transform.LookAt(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z));
    }


    public Transform Player;
}
