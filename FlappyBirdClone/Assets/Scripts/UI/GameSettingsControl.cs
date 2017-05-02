﻿using System.Collections;
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

    public GameMode mode;

    public float score;
    public float bestSwallow;

    public float averageSwallowPeak;
    public float averageSwallowDuration;

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

    public void updateRunStats() {
        averageSwallowDuration = SwallowDetector.Instance.getAvgDuration();
        averageSwallowPeak = SwallowDetector.Instance.getAvgSwallowPeak();
    }

    public void SetGameMode(GameMode gameMode) {
        mode = gameMode;
    }
}
