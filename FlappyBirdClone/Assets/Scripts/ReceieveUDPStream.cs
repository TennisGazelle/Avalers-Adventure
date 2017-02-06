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

    public GraphController graph;

    Thread thread;

    UdpClient client;
    string lastReceivedPacket = "";
    string allReceivedPackets = "";

    // Use this for initialization
    void Start() {
        Init();
    }

    void Update() {
        // hapens every frame
        
    }

    void OnDestroy() {
        thread.Interrupt();
        thread.Abort();
    }

    void OnApplicationQuit() {
        thread.Abort();
    }

    void Init() {
        thread = new Thread(new ThreadStart(ReceiveData));
        thread.IsBackground = true;
        thread.Start();
    }

    void ReceiveData() {
        while (true) {
            try {
                IPEndPoint myIP = new IPEndPoint(IPAddress.Parse(iPAddress), portNumber);
                byte[] data = client.Receive(ref myIP);
                string text = Encoding.UTF8.GetString(data);
                lastReceivedPacket = text;
                allReceivedPackets = allReceivedPackets + text;
                Debug.Log("float:" + float.Parse(text));
                Debug.Log("int: " + Int32.Parse(text));
                Debug.Log("String" + text);
                graph.updateCurrentValue(float.Parse(text));
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
}
