using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileDragging : MonoBehaviour {

    //gui text
    public Text timerText;
    public Text commandText;
    public Text swallowText;


    //private SpringJoint2D spring;
    private Rigidbody2D rb;

    //game settings
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
        rb = GetComponent<Rigidbody2D>();

        tempInput = 50.0f;
        //targetValue = baseline * percentageOfBaseline * (0.01f);
        targetValue = 50.0f;

        //timeBetweenSwallows = GameSettingsControl.Instance.restDuration;
        timeBetweenSwallows = 3.0f;
        restTimeLeft = 3.0f;

        swallowingWindowTimer = 0.0f;
        swallowingWindow = 3.0f;

        winningForce = new Vector3(100, 40, 0);
    }
	void Start () {

	}
	
	void Update () {

        //timerText.text = restTimeLeft.ToString("f2");
        swallowText.text = "Time to swallow: " + swallowingWindowTimer.ToString("f2");

        // update rest timer 
        if (restTimeLeft > 0)
        {
            timerText.text = "Time until next swallow: " + restTimeLeft.ToString("f2");
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
            swallowingWindowTimer = 0.0f;
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

        if (restTimeLeft <= 0 && swallowingWindowTimer <= swallowingWindow)
        {
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
