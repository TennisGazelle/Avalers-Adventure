using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTextScript : MonoBehaviour {

    public Text sliderPercentage;

    // Update is called once per frame
    public void setSliderValueText(float sliderValue){
        GameSettingsControl.Instance.baselinePercentage = sliderValue;
        sliderPercentage.text = sliderValue.ToString()+"%";
    }
}
