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
    public bool continousGameplay; 

    public enum SwallowingType { BALLISTIC_TYPICAL, BALLISTIC_EFFORTFUL, MENDELSOHN };

    public float score;
    public float bestSwallow;

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
