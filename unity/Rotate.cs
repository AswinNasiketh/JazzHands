using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	//TODO: Instead of having to go through in Unity and apply each, create a function that will auto-search for these GameObjects
	public GameObject referenceknuckle;
	public GameObject referencemid;
	public GameObject referenceend;
	public GameObject referencetip;
	public GameObject reference0x;
	public GameObject reference0y;
	public GameObject reference0z;
	public GameObject reference1x;
	public GameObject reference1y;
	public GameObject reference1z;
	public GameObject reference2x;
	public GameObject reference2y;
	public GameObject reference2z;
	public float speed;

	public bool collided = false;
	public bool baseCollided = false;
	public bool middleCollided = false;
	public bool topCollided = false;

	Vector3 x0Axis;
	Vector3 y0Axis;
	Vector3 z0Axis;
	Vector3 x1Axis;
	Vector3 y1Axis;
	Vector3 z1Axis;
	Vector3 x2Axis;
	Vector3 y2Axis;
	Vector3 z2Axis;

	public float bigAngle = 0f;
	float medAngle = 0f;
	float smallAngle = 0f;

	// Use this for initialization
	void Start () {
		// TODO: Create functions to simplify blocks below
		x0Axis = reference0x.transform.position - referenceknuckle.transform.position;
		y0Axis = reference0y.transform.position - referenceknuckle.transform.position;
		z0Axis = reference0z.transform.position - referenceknuckle.transform.position;
		x1Axis = reference1x.transform.position - referencemid.transform.position;
		y1Axis = reference1y.transform.position - referencemid.transform.position;
		z1Axis = reference1z.transform.position - referencemid.transform.position;
		x2Axis = reference2x.transform.position - referenceend.transform.position;
		y2Axis = reference2y.transform.position - referenceend.transform.position;
		z2Axis = reference2z.transform.position - referenceend.transform.position;

		float angle = Vector3.Angle(referencemid.transform.position - referenceknuckle.transform.position, y0Axis);
		transform.RotateAround(referenceknuckle.transform.position, z0Axis, -angle);

		float angle2 = Vector3.Angle(referenceend.transform.position - referencemid.transform.position, y1Axis);
		referencemid.transform.RotateAround(referencemid.transform.position, z1Axis, -angle2);

		float angle3 = Vector3.Angle(referencetip.transform.position - referenceend.transform.position, y2Axis);
		referenceend.transform.RotateAround(referenceend.transform.position, z2Axis, -angle3);
	}

	int i = 0;
	int x = 1;

	// Update is called once per frame
	void Update ()
	{
		if(baseCollided || middleCollided || topCollided)
		{
			collided = true;
		}
		else if(!baseCollided && !middleCollided && !topCollided)
		{
			collided = false;
		}
	}

	void OnCollisionEnter(Collision col)
	{
		baseCollided = true;
	}

	void OnCollisionExit(Collision col)
	{
		baseCollided = false;
	}

	public void setAngle(int i)
	{
	//asssi = .2;
		if(i > 90)
		{
			//collided = true;
			i = 90;
		}
		else 
		{
			//collided = false;
		}
		if(i < 0)
			i = 0;
		setBigAngle(i);
		setMedAngle(i);
		setSmallAngle(i);
	}

	//TODO: Setup the below functions to take arguments to distinguish between rotational parts instead of creating multiple functions

	private void setBigAngle(float angle)
	{
		float setAngle = angle - bigAngle;
		referenceknuckle.transform.RotateAround(referenceknuckle.transform.position, z0Axis, setAngle);
		bigAngle = angle;
	}

	private void setMedAngle(float angle)
	{
		float setAngle = angle - medAngle;
		referencemid.transform.RotateAround(referencemid.transform.position, z1Axis, setAngle);
		medAngle = angle;
	}

	private void setSmallAngle(float angle)
	{
		float setAngle = angle - smallAngle;
		referenceend.transform.RotateAround(referenceend.transform.position, z2Axis, setAngle);
		smallAngle = angle;
	}

	public float getAngle()
	{
		return Vector3.SignedAngle(y0Axis, (referencemid.transform.position - referenceknuckle.transform.position), z0Axis);
	}

	public float getMidAngle()
	{
		return Vector3.SignedAngle(y1Axis, (referenceend.transform.position - referencemid.transform.position), z1Axis);
	}

	public float getSmallAngle()
	{
		return Vector3.SignedAngle(y2Axis, (referencetip.transform.position - referencemid.transform.position), z2Axis);
	}
}
