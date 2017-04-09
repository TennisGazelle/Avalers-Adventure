using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDragging : MonoBehaviour {

    public float maxStretch = 3.0f;
    //public LineRenderer frontCatapult;
    //public LineRenderer backCatapult;

    private SpringJoint2D spring;
    private Rigidbody2D rb;

    private float baseline = GameSettingsControl.Instance.baselineSwallow;
    private float percentageOfBaseline = GameSettingsControl.Instance.baselinePercentage;

    private float tempInput;
    private float targetValue;
    private Vector3 winningForce;

    void Awake()
    {
        spring = GetComponent<SpringJoint2D>();
        rb = GetComponent<Rigidbody2D>();

        //tempInput = 50.0f;
        //targetValue = baseline * percentageOfBaseline * (0.01f);

        tempInput = 50;
        targetValue = 50;

        winningForce = new Vector3(85, 50, 0);
    }
	void Start () {
        //LineRendererSetup();
	}
	
	void Update () {
        // check the input from emg, if good check for success (temp for now)

        // if input => target * percentage, add the correct force to win, if not apply the percent there
	}

/*
    void LineRendererSetup(){
        frontCatapult.SetPosition(0, frontCatapult.transform.position);
        backCatapult.SetPosition(0, backCatapult.transform.position);

        frontCatapult.sortingLayerName = "SlingshotForeground";
        backCatapult.sortingLayerName = "SlingshotForeground";

        frontCatapult.sortingOrder = 3;
        backCatapult.sortingOrder = 1;
    }
*/
    void OnMouseDown(){

        float percentageThere;

        if (tempInput >= targetValue)
        {
            // so nothing holds onto asteroid
            Destroy(spring);

            // turn make dynamic so physics can work
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.angularDrag = 3f;

            rb.AddForce(winningForce, ForceMode2D.Impulse);
        }
        else
        {
            percentageThere = tempInput / targetValue;

            // so nothing holds onto asteroid
            Destroy(spring);

            // turn make dynamic so physics can work
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.angularDrag = 3f;

            // add force
            rb.AddForce(new Vector3((winningForce.x * percentageThere), winningForce.y, 0), ForceMode2D.Impulse);
        }
    }
}
