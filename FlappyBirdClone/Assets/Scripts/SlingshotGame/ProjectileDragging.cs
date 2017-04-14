using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileDragging : MonoBehaviour {

    //gui text
    public Text timerText;
    public Text commandText;
    public Text swallowText;

    public Text bestSwallow;
    public Text currentSwallow;
    public Text targetScore;


    //private SpringJoint2D spring;
    private Rigidbody2D rb;

    //game settings
    private float baseline;
    private float percentageOfBaseline;

    private float tempInput;
    private float targetValue;
    private Vector3 winningForce;

    private float timeBetweenSwallows;
    private float restTimeLeft;
    private float swallowingWindowTimer;
    private float swallowingWindow;

    public bool isShot;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        baseline = GameSettingsControl.Instance.baselineSwallow;
        percentageOfBaseline = GameSettingsControl.Instance.baselinePercentage;

        timeBetweenSwallows = GameSettingsControl.Instance.restDuration;
        //timeBetweenSwallows = 1.5f;

        tempInput = 50.0f;
        targetValue = baseline * percentageOfBaseline * (0.01f);
        //targetValue = 50.0f;
        targetScore.text = "Target: " + targetValue.ToString("f2");
        
        restTimeLeft = timeBetweenSwallows;

        swallowingWindowTimer = 1.5f;
        swallowingWindow = 2.0f;

        // update scores 
        GameSettingsControl.Instance.towerTumbleBestSwallow = 0.0f;
        winningForce = new Vector3(100, 40, 0);
    }
	void Start () {

	}
	
	void Update () {

        // check the input from emg, if good check for success (temp for now)

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
        else if (restTimeLeft <= 0 && swallowingWindowTimer >= 0)
        {
            commandText.text = "Swallow!";
            swallowingWindowTimer -= Time.deltaTime;
            return;
        }
        else if (restTimeLeft <= 0 && swallowingWindowTimer <= 0)
        {
            //commandText.text = "Not quick enough";
            restTimeLeft = timeBetweenSwallows;
            swallowingWindowTimer = 2.0f;
            return;
        }
        else
        {
            // do nothing
        }
    }   

    void OnMouseDown(){

        float percentageThere;

        if (restTimeLeft <= 0 && swallowingWindowTimer <= swallowingWindow)
        {
            // trigger is shot 
            isShot = true;

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

            // update best score 
            if (tempInput > GameSettingsControl.Instance.towerTumbleBestSwallow)
            {
                GameSettingsControl.Instance.towerTumbleBestSwallow = tempInput;
                bestSwallow.text = "Best swallow: " + GameSettingsControl.Instance.towerTumbleBestSwallow.ToString("f2");
                currentSwallow.text = "Current swallow: " + tempInput.ToString("f2");
            }

            // reset timers
            restTimeLeft = timeBetweenSwallows;
            swallowingWindowTimer = 0.0f;
        }
    }
}
