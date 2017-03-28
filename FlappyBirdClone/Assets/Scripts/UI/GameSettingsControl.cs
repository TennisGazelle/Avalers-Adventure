using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsControl : MonoBehaviour
{
    public static GameSettingsControl Instance;

    public float baselineSwallow;
    public float baselinePercentage;
    public float restDuration;
    public float swallowDuration;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
