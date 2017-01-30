using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GraphController : MonoBehaviour {
    public float maxValue = 100;
    public float minValue = 0;
    public float upperGoalHeight= 50;
    public float lowerGoalHeight= 25;
    public float backgroundHeight = 150;

    public Text CurrentValueText;
    public Text maxText;
    public Text minText;

    public GameObject background;
    public GameObject upperGoalBound;
    public GameObject lowerGoalBound;

    private float currentValue = 0;

    //Use this for initialization
	void Awake () {
        // init height for goal bars
        Vector3 upperGoalPos = upperGoalBound.transform.localPosition;
        Vector3 lowerGoalPos = lowerGoalBound.transform.localPosition;
        upperGoalBound.transform.localPosition = new Vector3(upperGoalPos.x, upperGoalHeight - backgroundHeight, upperGoalPos.z);
        lowerGoalBound.transform.localPosition = new Vector3(lowerGoalPos.x, lowerGoalHeight - backgroundHeight, lowerGoalPos.z);

    }
	
	// Update is called once per frame
	void Update () {
        updateText();
	}

    void updateText()
    {
        // refresh texts
        maxText.text = "Max: " + maxValue;
        minText.text = "Min: " + minValue;
        CurrentValueText.text = "Current Value: " + currentValue;
    }
}
