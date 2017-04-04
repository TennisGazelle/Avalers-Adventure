using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonScript : MonoBehaviour {
    public float XSpeed = 1;
    public ReceieveUDPStream stream;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameStateManager.GameState == GameState.Intro) {
            if (WasScreenClicked()) {
                GameStateManager.GameState = GameState.Playing;
                BallisticBoostOnYAxis(5);
            }
        } else if (GameStateManager.GameState == GameState.Playing) {
            if (WasScreenClicked()) {
                BallisticBoostOnYAxis(5);
            }
        }
        MoveOnXAndLimitDownMovement();
    }

    bool WasScreenClicked() {
        if (Input.GetMouseButtonDown(0) || stream.hasTypicalHappened() ||
            Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended) {
            Debug.Log("clicked");
            return true;
        }
        return false;
    }

    void BallisticBoostOnYAxis(int data) {
        GetComponent<Rigidbody>().velocity = new Vector3(0, data, 0);
    }

    void MoveOnXAndLimitDownMovement() {
        Vector3 old = this.transform.position;
        old.y = 0.5f*System.Convert.ToSingle(System.Math.Sin(old.x));

        //this.transform.position += new Vector3(Time.deltaTime * XSpeed, 0, 0);
    }
}
