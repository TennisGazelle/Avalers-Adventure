using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveformData {
    public int duration;
    public float highestValue;
    public List<float> dataPoints;
}

public class SwallowDetector : MonoBehaviour {
    public static SwallowDetector Instance;
    private List<float> noiseSample;
    public int noiseSampleMax = 500;
    public float noiseAV, noiseSD; 
    private float oldAV, oldSD, threshold;
    private static int detectionCounter;
    public List<WaveformData> waves;
    private bool noiseSet;

	// Use this for initialization
	void Awake () {
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        noiseSample = new List<float>();
        waves = new List<WaveformData>();
        reset();
    }

    void reset() {
        detectionCounter = 0;
        noiseSample.Clear();
        waves.Clear();
        noiseAV = 0;
        noiseSD = 0;
        threshold = 20;
        noiseSet = false;
    }
	
	// Update is called once per frame
	void Update () {
        sampleNoise();
        detectionCounter++;
        // every 100 frames
        if ((detectionCounter % 500) == 0) {
            if (!noiseSet)
                updateStats();
            Debug.Log("Updating statistics: Average " + noiseAV + " Std. Dev.: " + noiseSD);
            // if it changed too much, get rid of the first quarter
            if (!noiseSet && Mathf.Abs(oldSD - noiseSD) > threshold) {
                noiseSample.RemoveRange(0, noiseSampleMax / 4);
            } else {
                noiseSet = true;
                Debug.Log("noise set, looking for swallow");
                // what is two SD away?
                float upperThreshold = noiseAV + (noiseSD * 3);
                // data about highest element
                float maxElement = 0.0f;
                int maxElementIndex = 0;
                // detection flag
                bool swallowOccured = false;

                // if any element is over two SDs, set the flag
                for (int i = 0; i < noiseSample.Count; i++) {
                    if (noiseSample[i] > upperThreshold) {
                        swallowOccured = true;
                    }
                    // update highest
                    if (noiseSample[i] > maxElement) {
                        maxElement = noiseSample[i];
                        maxElementIndex = i;
                    }
                }

                // if there was a swallow, go out and find the parameters
                if (swallowOccured) {
                    Debug.Log("Swallow found at " + maxElement + " high");
                    findSwallowingLimits(maxElementIndex);
                }

                noiseSample.Clear();
            }
        }
    }

    void findSwallowingLimits(int indexOfHighest) {
        int leftIndex = indexOfHighest-1;
        int rightIndex = indexOfHighest+1;
        float threshold = noiseAV + (noiseSD * .25f);
        // go left until it stops getting closer to average
        while (noiseSample[leftIndex] > threshold && leftIndex > 0) {
            leftIndex--;
        }
        // go right until it stops getting closer to average
        while (noiseSample[rightIndex] > threshold && rightIndex < noiseSample.Count) {
            rightIndex++;
        }
        WaveformData wd = new WaveformData();
        wd.highestValue = noiseSample[indexOfHighest];
        wd.duration = rightIndex - leftIndex;
        wd.dataPoints = noiseSample.GetRange(leftIndex, wd.duration);
        waves.Add(wd);
        outputWaveformData(wd);
    }

    void outputWaveformData(WaveformData wd) {
        string filename = Application.dataPath + "/waves.txt";
        List<string> lines;
        if (System.IO.File.Exists(filename)) {
            lines = new List<string>(System.IO.File.ReadAllLines(filename));
        } else {
            lines = new List<string>();
        }
        
        lines.Add("wave " + wd.duration + " " + wd.highestValue);
        foreach(float f in wd.dataPoints) {
            lines.Add(f.ToString());
        }

        System.IO.File.WriteAllLines(filename, lines.ToArray());
        Debug.Log("send to " + filename);
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

    public float getAvgSwallowPeak() {
        // get average peak from array
        float avgPeak = 0.0f;
        foreach (WaveformData wd in waves) {
            avgPeak += wd.highestValue;
        }
        return avgPeak/waves.Count;
    }

    public float getAvgDuration() {
        float avgDuration = 0.0f;
        foreach (WaveformData wd in waves) {
            avgDuration += wd.duration;
        }
        return (avgDuration / waves.Count) * Time.fixedDeltaTime;
    }
}
