using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreTextController : MonoBehaviour {

    Text scoreText;

    // Use this for initialization
    void Start () {
        scoreText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "Score: " + ScoreManagerScript.Score;
    }
}
