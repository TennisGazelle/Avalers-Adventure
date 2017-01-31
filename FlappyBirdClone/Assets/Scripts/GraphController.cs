using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GraphController : MonoBehaviour {
    public float maxValue = 100;
    public float minValue = 0;
    public float upperGoalHeight = 50;
    public float lowerGoalHeight= 25;
    public float backgroundHeight = 150;
    public float backgroundWidth = 200;
    public float maxDataPlots = 80;

    public Text CurrentValueText;
    public Text maxText;
    public Text minText;

    public GameObject background;
    public GameObject upperGoalBound;
    public GameObject lowerGoalBound;
    public GameObject lineRenderer;

    private float previousValue = 0;
    private float currentValue = 0;
    private float timer;
    private float updateTime = 0.1f;

    private LineRenderer line;

    private int counter = 0;
    private float step;


    List<Vector3> dataPoints;

    //Use this for initialization
	void Awake () {
        // init height for goal bars
        Vector3 upperGoalPos = upperGoalBound.transform.localPosition;
        Vector3 lowerGoalPos = lowerGoalBound.transform.localPosition;
        upperGoalBound.transform.localPosition = new Vector3(upperGoalPos.x, upperGoalHeight - backgroundHeight, upperGoalPos.z);
        lowerGoalBound.transform.localPosition = new Vector3(lowerGoalPos.x, lowerGoalHeight - backgroundHeight, lowerGoalPos.z);
        line = lineRenderer.GetComponent<LineRenderer>();

        dataPoints = new List<Vector3>();
        line.sortingOrder = 4;
        line.sortingLayerName = "UI";

        step = (backgroundWidth / maxDataPlots);
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;

        if (timer >= updateTime)
        {
            timer = 0.0f;
            updateText();
            updateCurrentValue(Random.Range(0, 150));
            drawLine();
        }
    }

    void updateText()
    {
        // refresh texts
        maxText.text = "Max: " + maxValue;
        minText.text = "Min: " + minValue;
        CurrentValueText.text = "Current Value: " + currentValue;
        
    }

    void drawLine()
    {
        dataPoints.Add(new Vector3(step * counter, currentValue, 1.0f));
        counter++;
        // if we've plotted all, remove the first one
        if(dataPoints.Count > maxDataPlots)
        {
            dataPoints.Remove(dataPoints[0]);
            Vector3 linePos = lineRenderer.transform.localPosition;
            lineRenderer.transform.localPosition = new Vector3(linePos.x - step, linePos.y, linePos.z);
        }

        // Update Graph
        line.SetPositions(dataPoints.ToArray());
    }

    public void updateCurrentValue(float value)
    {
        previousValue = currentValue;
        currentValue = value;
    } 
}
