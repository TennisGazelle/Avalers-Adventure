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

    Thread thread;

    UdpClient client;
    string lastReceivedPacket = "";
    string allReceivedPackets = "";

    private static void Main() {
        ReceieveUDPStream receiveObj = new ReceieveUDPStream();
        receiveObj.Init();

        string text = "";

        do {
            text = Console.ReadLine();
        }
        while (!text.Equals("exit"));
    }
    
    void OnGUI() {
        Rect rectObj = new Rect(40, 10, 200, 400);
        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.UpperLeft;
        GUI.Box(rectObj, "# UDPReceive\n127.0.0.1 " + portNumber + " #\n"
                    + "shell> nc -u 127.0.0.1 : " + portNumber + " \n"
                    + "\nLast Packet: \n" + lastReceivedPacket
                    + "\n\nAll Messages: \n" + allReceivedPackets
                , style);
    }

    // Use this for initialization
    void Start() {
        Init();
    }

    void Update() {
        // hapens every frame
        
    }

    void Init() {
        thread = new Thread(new ThreadStart(ReceiveData));
        thread.IsBackground = true;
        thread.Start();
    }

    void ReceiveData() {
        client = new UdpClient(portNumber);

        do {

            try {
                IPEndPoint myIP = new IPEndPoint(IPAddress.Parse(iPAddress), portNumber);
                byte[] data = client.Receive(ref myIP);
                string text = Encoding.UTF8.GetString(data);
                print(">>" + text);
                lastReceivedPacket = text;
                allReceivedPackets = allReceivedPackets + text;
            } catch (Exception err) {
                print(err.ToString());
            }
        } while (true);
    }

    public string ResetPacketHistory() {
        allReceivedPackets = "";
        return lastReceivedPacket;
    }
}
