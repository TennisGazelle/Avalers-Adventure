using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public GameObject target;
	Vector3 offset;
	public float smoothing = 10f;


	// Use this for initialization
	void Start () {
		offset = transform.position - target.transform.position;
	}

	// Update is called once per frame
	void Update () {
		// Create a postion the camera is aiming for based on the offset from the target.
		Vector3 targetCamPos = target.transform.position + offset;

		// Smoothly interpolate between the camera's current position and it's target position.
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
