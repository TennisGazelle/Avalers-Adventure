using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDragging : MonoBehaviour {

    public float maxStretch = 3.0f;
    public LineRenderer frontCatapult;
    public LineRenderer backCatapult;

    private SpringJoint2D spring;
    private Rigidbody2D rb;

	void Awake()
    {
        spring = GetComponent<SpringJoint2D>();
        rb = GetComponent<Rigidbody2D>();
    }
	void Start () {
        LineRendererSetup();
	}
	
	// Update is called once per frame
	void Update () {
		if (spring != null)
        {

        }
        else
        {

        }
	}

    void LineRendererSetup(){
        frontCatapult.SetPosition(0, frontCatapult.transform.position);
        backCatapult.SetPosition(0, backCatapult.transform.position);

        frontCatapult.sortingLayerName = "SlingshotForeground";
        backCatapult.sortingLayerName = "SlingshotForeground";

        frontCatapult.sortingOrder = 3;
        backCatapult.sortingOrder = 1;
    }

    void onSwallow(){
        // disable spring so it can move
        spring.enabled = false;

        // move back based on input from external

        // enable spring so it can fling away; turn kinematic off so it can use physics
        spring.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
