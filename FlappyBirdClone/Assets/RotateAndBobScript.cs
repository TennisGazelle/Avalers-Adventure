using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndBobScript : MonoBehaviour {
    public float period;
    public float amplitude;
    public float offsetInY;
    public AudioClip collectSound; 
    public float constantYOffset;
    public int points = 10;

    private Vector3 origin;
    private Vector3 destA;
    private Vector3 destB;
    
    private bool moveToB;
    public float moveSpeed;

    private RandomMovementType movementType = RandomMovementType.Stationary;
	// Use this for initialization
	void Awake () {
        offsetInY = 0;
        origin = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position -= new Vector3(0, offsetInY + constantYOffset, 0);
        offsetInY = Mathf.Sin(Time.time * period) * amplitude;
        transform.position += new Vector3(0, offsetInY + constantYOffset, 0);
        transform.Rotate(Vector3.up, 2, Space.Self);
        switch (movementType)
        {
            case RandomMovementType.Circle:
                MoveRandom();
                break;
            case RandomMovementType.Horizontal:
                MoveRandom();
                break;
            case RandomMovementType.Vertical:
                MoveRandom();
                break;
            case RandomMovementType.Stationary:
                
                break;
        }
	}

    void OnTriggerEnter(Collider collector) {
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        ScoreManagerScript.Score += points;
        Destroy(gameObject);
    }

    public void RandomizeMovement()
    {
        int dice = Random.Range(0, 4);
        switch (dice)
        {
            case 0:
                movementType = RandomMovementType.Circle;
                destA = origin;
                destB = new Vector3(origin.x + Random.Range(-20, 20), origin.y + Random.Range(-5, 10), origin.z + Random.Range(-20, 20));
                moveToB = Random.Range(0, 1) == 1;
                break;
            case 1:
                movementType = RandomMovementType.Horizontal;
                destA = origin;
                destB = new Vector3(origin.x + Random.Range(-20, 20), origin.y, origin.z + Random.Range(-20, 20));
                moveToB = Random.Range(0, 1) == 1;
                break;
            case 2:
                movementType = RandomMovementType.Vertical;
                destA = new Vector3(origin.x, origin.y + Random.Range(0, 15), origin.z);
                destB = new Vector3(origin.x, origin.y + Random.Range(-30, 0), origin.z);
                moveToB = Random.Range(0, 1) == 1;
                break;
            case 3:
                movementType = RandomMovementType.Stationary;
                break;
        }
        moveSpeed = Random.Range(4, 10);
    }

    private void MoveCircle()
    {

    }

    private void MoveRandom()
    {
        if (moveToB)
        {
            transform.position = Vector3.MoveTowards(transform.position, destB, .2f);
            
            if (transform.position == destB)
            {
                moveToB = false;
            }
        }

        else
        {
            transform.position = Vector3.MoveTowards(transform.position, destA, .2f);
            if (transform.position == destA)
            {
                moveToB = true;
            }
        }
    }
}
