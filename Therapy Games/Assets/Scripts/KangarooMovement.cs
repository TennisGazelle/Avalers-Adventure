using UnityEngine;
using System.Collections;

public class KangarooMovement : MonoBehaviour {
	public float movementSpeed = 10f;  
	Rigidbody playerRigidbody;
	Vector3 movement;
	public float jumpHeight = 20f;

	// Use this for initialization
	void Awake () {
		playerRigidbody = GetComponent <Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		move (horizontal, vertical);
		jump ();
	}

	void move(float h, float v){
		movement.Set (h, 0f, v);

		movement = movement.normalized * movementSpeed * Time.deltaTime;

		playerRigidbody.MovePosition (transform.position + movement);
	}

	void jump(){
		if (Input.GetKeyDown ("space")) {
			Vector3 jump = new Vector3 (0, jumpHeight, 0);

			playerRigidbody.AddForce (jump);

		}
	}

}
