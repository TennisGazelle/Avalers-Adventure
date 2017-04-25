using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwallowDetector : MonoBehaviour {
    public static SwallowDetector Instance;
    private List<float> noiseSample;
    private int noiseSampleMax = 100;
    private float noiseAV, noiseSD, oldAV, oldSD, threshold;
    private static int detectionCounter;

	// Use this for initialization
	void Awake () {
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        noiseSample = new List<float>();
	}
	
	// Update is called once per frame
	void Update () {
        if (noiseSample.Count < noiseSampleMax) {
            sampleNoise();
        }
        detect();
	}

    void detect() {
        if ((detectionCounter % 1000) == 0) {
            updateStats();
            if (Mathf.Abs(oldSD - noiseSD) > threshold) {
                // take out the first quarter of the data
                noiseSample.RemoveRange(0, noiseSampleMax / 4);
            }
        }

    
    }

    void sampleNoise() {
        noiseSample.Add(ReceieveUDPStream.lastnum);
    }

    void updateStats() {
        oldAV = noiseAV;
        oldSD = noiseSD;

        float sum = 0.0f, diffSum = 0.0f;
        foreach (float f in noiseSample) {
            sum += f;
        }
        noiseAV = sum / noiseSample.Count;

        foreach (float f in noiseSample) {
            diffSum += Mathf.Pow((f - noiseAV), 2);
        }
        noiseSD = Mathf.Sqrt(diffSum / noiseSample.Count);
    }
}
