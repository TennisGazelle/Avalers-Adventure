using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetGameMode(GameMode mode) {
        GameSettingsControl.Instance.mode = mode;
    }

    public void SetToTypical() {
        SetGameMode(GameMode.Typical);
    }

    public void SetToEfforful() {
        SetGameMode(GameMode.Effortful);
    }

    public void SetToMendelsohn() {
        SetGameMode(GameMode.Mendelsohn);
    }

    public void SetToRandom() {
        SetGameMode(GameMode.Random);
    }
}
