using UnityEngine;
using System.Collections;

using System;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Threading;

public class ReceieveUDPStream : MonoBehaviour {
    public string iPAddress = "127.0.0.1";
    public int portNumber = 3000;
    private float highestValue = 1023.0f;

    public GraphController graph;

    Thread thread;

    UdpClient client;
    string lastReceivedPacket = "";
    string allReceivedPackets = "";

    public ArrayList incomingData;
    public static float lastnum = 0;

    private bool typicalCheck, lastTypicalCheck;

    // Use this for initialization
    void Start() {
        typicalCheck = false;
        lastTypicalCheck = false;
        Init();
    }

    void Update() {
        // hapens every frame
        if (incomingData.Count > 20) {
            incomingData.RemoveAt(0);
        }
    }

    void OnDisable() {
        if (thread != null) {
            thread.Abort();
        }
        client.Close();
    }

    void Init() {
        incomingData = new ArrayList();
        thread = new Thread(new ThreadStart(ReceiveData));
        thread.IsBackground = true;
        thread.Start();
    }

    void ReceiveData() {
        client = new UdpClient(portNumber);
        while (true) {
            try {
                IPEndPoint myIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref myIP);
                string text = Encoding.UTF8.GetString(data);
                lastReceivedPacket = text;
                allReceivedPackets = allReceivedPackets + text;

                float normalizedValue = float.Parse(text);
                graph.updateCurrentValue(normalizedValue);
                incomingData.Add(normalizedValue);
                lastnum = normalizedValue;
            } catch (Exception err) {
                print(err.ToString());
            }

            // check to see if the main thread is alive, and die if not
        }


        //Invoke("ReceiveData", 0.0f);
    }

    public string ResetPacketHistory() {
        allReceivedPackets = "";
        return lastReceivedPacket;
    }

    
    public bool hasTypicalHappened() {
        return hasTypicalStaticHappened();
        typicalCheck = lastnum > GameSettingsControl.Instance.baselineSwallow;

        if (typicalCheck == lastTypicalCheck) {
            return false;
        } else if (typicalCheck) {
            lastTypicalCheck = true;
            return true;
        } else {
            lastTypicalCheck = false;
            return false;
        }
    }

    public bool hasTypicalStaticHappened() {
        return lastnum > GameSettingsControl.Instance.baselineSwallow;
    }

    public bool hasEffortfulHappened() {
        return false;
    }
}
