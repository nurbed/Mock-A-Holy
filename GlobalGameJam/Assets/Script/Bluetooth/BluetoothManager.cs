using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

public class BluetoothManager : MonoBehaviour
{
	[SerializeField] TCPConnection m_tcpConnection;
	[SerializeField] string m_msgToServer;
		
    void Start()
    {
		enabled = false;
		m_tcpConnection.OnSocketSetuped += EnableScript;

		//if connection has not been made, display button to connect
		if (m_tcpConnection.socketReady == false)
		{
			m_tcpConnection.setupSocket();
			/*if (GUILayout.Button("Connect"))
			{
				//try to connect
				Debug.Log("Attempting to connect..");
			}*/
			Debug.Log("Attempting to connect..");
		}

		//once connection has been made, display editable text field with a button to send that string to the server (see function below)
		if (m_tcpConnection.socketReady == true)
		{
			m_msgToServer = GUILayout.TextField(m_msgToServer);

			/*if (GUILayout.Button("Write to server", GUILayout.Height(30)))
			{
				SendToServer(m_msgToServer);
			}*/
			SendToServer(m_msgToServer);
		}
	}

	void Update()
    {
        //keep checking the server for messages, if a message is received from server, it gets logged in the Debug console (see function below)
        SocketResponse();
    }

	//socket reading script
    void SocketResponse()
    {
        string serverSays = m_tcpConnection.readSocket();

        if (serverSays != "")
        {
            Debug.Log("[SERVER]" + serverSays);
        }
    }

    //send message to the server
    public void SendToServer(string str)
    {
        m_tcpConnection.writeSocket(str);
        Debug.Log("[CLIENT] -> " + str);
    }

	void EnableScript()
	{
		m_tcpConnection.OnSocketSetuped -= EnableScript;
		enabled = true;
	}
}

