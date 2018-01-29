using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerController : MonoBehaviour {

	public GameObject[] gameObjects;
	public GameObject serialCom;

	private SerialCommuncication comm;

	// Use this for initialization
	void Start () {
		comm = serialCom.GetComponent<SerialCommuncication>();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("Updating...");
		int[] fingerPos = comm.UpdateFingers(gameObjects);
		for (int i = 0; i < gameObjects.Length; i++) {
			Rotate rotate = gameObjects[i].GetComponentInChildren<Rotate>();
			if((rotate.collided && (fingerPos[i] < rotate.bigAngle - 5f)) || !rotate.collided)
			{
				rotate.setAngle(fingerPos[i]);
			}
		}
	}
}
