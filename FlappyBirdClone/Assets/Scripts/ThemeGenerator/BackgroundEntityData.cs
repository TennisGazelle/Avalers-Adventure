using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BackgroundEntityData:System.Object {

    public string label;
    public GameObject backgroundObject;
    public float initialOffset;
    public float minSpacing;
    public float maxSpacing;
    public DisplayType dimensionLayout;
}

