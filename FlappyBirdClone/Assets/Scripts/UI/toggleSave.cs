using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleSave : MonoBehaviour {

	public void saveToggleValue(bool toggleBool)
    {
        GameSettingsControl.Instance.continousGameplay = toggleBool;
    }
}
