using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileDragging : MonoBehaviour {

    public Text timerText;
    public Text commandText;
    public Text swallowText;

    public float maxStretch = 3.0f;

    private SpringJoint2D spring;
    private Rigidbody2D rb;

    private float baseline = GameSettingsControl.Instance.baselineSwallow;
    private float percentageOfBaseline = GameSettingsControl.Instance.baselinePercentage;

    private float tempInput;
    private float targetValue;
    private Vector3 winningForce;

    private float timeBetweenSwallows;
    private float restTimeLeft;
    private float swallowingWindowTimer;
    private float swallowingWindow;


    void Awake()
    {
        spring = GetComponent<SpringJoint2D>();
        rb = GetComponent<Rigidbody2D>();

        //tempInput = 50.0f;
        //targetValue = baseline * percentageOfBaseline * (0.01f);

        tempInput = 50;
        targetValue = 50;

        //timeBetweenSwallows = GameSettingsControl.Instance.restDuration;
        timeBetweenSwallows = 3.0f;
        restTimeLeft = timeBetweenSwallows;

        swallowingWindowTimer = 0.0f;
        swallowingWindow = 3.0f;

    winningForce = new Vector3(100, 40, 0);
    }
	void Start () {
        //LineRendererSetup();
	}
	
	void Update () {

        timerText.text = restTimeLeft.ToString("f2");
        swallowText.text = swallowingWindowTimer.ToString("f2");

        // update timer 
        if (restTimeLeft > 0)
        {
            commandText.text = "Relax";
            restTimeLeft -= Time.deltaTime;
            return;
        }

        if (restTimeLeft <= 0 && swallowingWindowTimer <= swallowingWindow)
        {
            commandText.text = "Swallow!";
            swallowingWindowTimer += Time.deltaTime;
            return;
        }
        else if (restTimeLeft <= 0 && swallowingWindowTimer >= swallowingWindow)
        {
            commandText.text = "Not quick enough";
            restTimeLeft = timeBetweenSwallows;
            //swallowingWindowTimer = 0.0f;
            return;
        }
        else
        {
            // do nothing
        }

        // check the input from emg, if good check for success (temp for now)
    }

    void OnMouseDown(){

        float percentageThere;

        if (restTimeLeft == 0 && swallowingWindowTimer <= swallowingWindow)
        {

            // so nothing holds onto asteroid
            Destroy(spring);

            // turn make dynamic so physics can work
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.angularDrag = 3f;

            if (tempInput >= targetValue)
            {
                rb.AddForce(winningForce, ForceMode2D.Impulse);
            }
            else
            {
                percentageThere = tempInput / targetValue;

                // add force
                rb.AddForce(new Vector3((winningForce.x * percentageThere), winningForce.y, 0), ForceMode2D.Impulse);
            }

            // reset timers
            restTimeLeft = timeBetweenSwallows;
            swallowingWindowTimer = 0.0f;
        }
    }
}
