using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScript : MonoBehaviour {
    public Collider2D ButtonCollider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// if we're in the playing state, and 
        // the button is hit, then let's end the 
        // game, otherwise, turn the visibility off
        if (GameStateManager.GameState == GameState.Playing && WasButtonClicked()) {
            GameStateManager.GameState = GameState.Dead;
        } else if (GameStateManager.GameState == GameState.Dead && WasButtonClicked()) {
            GameStateManager.GameState = GameState.Intro;
        }
	}

    bool WasButtonClicked() {
        // grab the click
        Vector2 pointOfContact = Vector2.zero;
        if (Input.touchCount > 0)
            pointOfContact = Input.touches[0].position;
        else if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            pointOfContact = Input.mousePosition;

        // check against collider
        return ButtonCollider == Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(pointOfContact));
    }
}
