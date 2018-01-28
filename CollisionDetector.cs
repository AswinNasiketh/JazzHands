using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour {

	public GameObject gameObject;
	public bool middle;

	// NOTE: Collision code technically does (or should...) work, need to figure out the collisions in the model but functionality is written

	private Rotate rotate;

	void Start()
	{
		rotate = gameObject.GetComponent<Rotate>();
	}

	void OnCollisionEntered(Collision col)
	{
		if(middle)
		{
			rotate.middleCollided = true;
		}
		else
		{
			rotate.topCollided = true;
		}
	}

	void OnCollisionExit(Collision col)
	{
		if(middle)
		{
			rotate.middleCollided = false;
		}
		else
		{
			rotate.topCollided = false;
		}
	}
}
