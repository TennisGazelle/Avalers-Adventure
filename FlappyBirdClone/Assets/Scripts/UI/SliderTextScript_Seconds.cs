using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTextScript_Seconds : MonoBehaviour {

    public Text sliderPercentage;

    // Update is called once per frame
    public void setSliderValueText(float sliderValue){
        float rounded = (float)(Math.Round((double)sliderValue, 2));
        GameSettingsControl.Instance.swallowDuration = rounded;
        sliderPercentage.text = rounded.ToString()+" Seconds";
    }
}
