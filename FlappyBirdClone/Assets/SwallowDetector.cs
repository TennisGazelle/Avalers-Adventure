using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveformData {
    public int duration;
    public float highestValue;
}

public class SwallowDetector : MonoBehaviour {
    public static SwallowDetector Instance;
    private List<float> noiseSample;
    private int noiseSampleMax = 500;
    private float noiseAV, noiseSD, oldAV, oldSD, threshold;
    private static int detectionCounter;
    public List<WaveformData> waves;

	// Use this for initialization
	void Awake () {
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        noiseSample = new List<float>();
        waves = new List<WaveformData>();
	}
	
	// Update is called once per frame
	void Update () {
        sampleNoise();
        // every 1000 frames
        if ((detectionCounter % 1000) == 0) {
            updateStats();
            // if it changed too much, get rid of the first quarter
            if (Mathf.Abs(oldSD - noiseSD) > threshold) {
                noiseSample.RemoveRange(0, noiseSampleMax / 4);
            } else {
                float upperThreshold = noiseAV + (noiseSD * 2);
                float maxElement = 0.0f;
                int maxElementIndex = 0;
                bool swallowOccured = false;
                // if any element is over two SDs
                for (int i = 0; i < noiseSample.Count; i++) {
                    if (noiseSample[i] > upperThreshold) {
                        swallowOccured = true;
                    }
                    if (noiseSample[i] > maxElement) {
                        maxElement = noiseSample[i];
                        maxElementIndex = i;
                    }
                }

                // if there was a swallow, go out and find the parameters
                if (swallowOccured) {
                    findSwallowingLimits(maxElementIndex);
                }
            }
        }
    }

    void findSwallowingLimits(int indexOfHighest) {
        int leftIndex = indexOfHighest-1;
        int rightIndex = indexOfHighest+1;
        // go left until it stops getting closer to average
        while (noiseSample[leftIndex] < noiseSample[leftIndex+1]) {
            leftIndex--;
        }
        // go right until it stops getting closer to average
        while (noiseSample[rightIndex] > noiseSample[rightIndex-1]) {
            rightIndex++;
        }
        WaveformData wd = new WaveformData();
        wd.highestValue = noiseSample[indexOfHighest];
        wd.duration = rightIndex - leftIndex;
        waves.Add(wd);
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
