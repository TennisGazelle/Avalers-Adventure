using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndBobScript : MonoBehaviour {
    public float period;
    public float amplitude;
    public float offsetInY;

    public float constantYOffset;

	// Use this for initialization
	void Start () {
        offsetInY = 0;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position -= new Vector3(0, offsetInY+constantYOffset, 0);
        offsetInY = Mathf.Sin(Time.time * period) * amplitude;
        transform.position += new Vector3(0, offsetInY+constantYOffset, 0);
        transform.Rotate(Vector3.up, 2, Space.Self);
	}

    void OnTriggerEnter(Collider collector) {
        ScoreManagerScript.Score++;
        Destroy(gameObject);
    }
}
