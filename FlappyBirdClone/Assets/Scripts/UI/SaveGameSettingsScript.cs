using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveGameSettingsScript : MonoBehaviour {

    public Slider percentageSliderObj;
    public InputField inputFieldObj;
    public Slider restDurationSlider;
    public Slider swallowDurationSlider;
    public Toggle waitForInputToggle;

	// Update is called once per frame
	public void saveGame () {
        GameSettingsControl.Instance.baselineSwallow = float.Parse(inputFieldObj.text);
        GameSettingsControl.Instance.baselinePercentage = percentageSliderObj.value;
        GameSettingsControl.Instance.restDuration = restDurationSlider.value;
        GameSettingsControl.Instance.swallowDuration = swallowDurationSlider.value;

        GameSettingsControl.Instance.score = 0.0f;
        GameSettingsControl.Instance.bestSwallow = 0.0f;
        GameSettingsControl.Instance.continousGameplay = waitForInputToggle.isOn;
    }
}
