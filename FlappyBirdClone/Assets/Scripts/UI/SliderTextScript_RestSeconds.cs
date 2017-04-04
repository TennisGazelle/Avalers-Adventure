using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTextScript_RestSeconds : MonoBehaviour
{
    public Text restSlider;

    // Update is called once per frame
    public void setSliderValueText(float sliderValue)
    {
        float rounded = (float)(Math.Round((double)sliderValue, 2));
        GameSettingsControl.Instance.restDuration = rounded;
        restSlider.text = rounded.ToString() + " Seconds";
    }
}