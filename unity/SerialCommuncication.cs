using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
public class SerialCommuncication : MonoBehaviour {
    SerialPort arduinoConnection = new SerialPort("COM4", 9600);

    private int[] currentFingerPos;
    private int[] nextFingerPos;
    private string temp;

	// Use this for initialization
	void Start () {
		Debug.Log("Starting....");
        arduinoConnection.ReadTimeout = 10;
        arduinoConnection.Open();
        currentFingerPos = new int[5];
        nextFingerPos = new int[5];
	}
	
	public int[] UpdateFingers (GameObject[] gameObjects) {
		int[] positions = new int[5];
		for (int i = 0; i < gameObjects.Length; i++) {
			Rotate rotate = gameObjects[i].GetComponentInChildren<Rotate>();
			if(rotate.collided)
			{
				positions[i] = Mathf.FloorToInt(rotate.bigAngle);
			}
			else
			{
				positions[i] = 0;
			}
		}
		setNextFingerPos(positions);
        writeNewPositions();
        readCurrentPositions(10);
        int[] fingerPos = getCurrentFingerPos();
        //Debug.Log(fingerPos[4]);
        return fingerPos;
	}

    private void writeNewPositions()
    {
        string commandString = "SET ";
        for(int i = 0; i < 5; i++)
        {
            commandString += nextFingerPos[i].ToString() + " ";
        }
        arduinoConnection.WriteLine(commandString);
        arduinoConnection.BaseStream.Flush();
    }

    private void readCurrentPositions(int timeout = 0)
    {
        arduinoConnection.WriteLine("READ");
        arduinoConnection.BaseStream.Flush();
        arduinoConnection.ReadTimeout = timeout;
        for(int i = 0; i< 5; i++)
        {
            try
            {
                currentFingerPos[i] = int.Parse(arduinoConnection.ReadLine());
                Debug.Log("Current Finger Pos: " + i + " --- " + currentFingerPos[i]);
            }
            catch (TimeoutException e)
            {
                Debug.Log("TimeOutexception");
            }           
        }
    }

    public int[] getCurrentFingerPos()
    {
        return currentFingerPos;
    }

    public void setNextFingerPos(int[] fingerPos)
    {
        nextFingerPos = fingerPos;
    }

}
