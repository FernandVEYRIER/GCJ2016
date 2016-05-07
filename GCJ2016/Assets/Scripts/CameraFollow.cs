﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	private GameObject target;

	private Vector3 vel;

	// Update is called once per frame
	void Update ()
	{
		if (target == null)
		{
			target = GameObject.FindGameObjectWithTag ("Player");
			return;
		}
		transform.position = Vector3.SmoothDamp (transform.position, new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z), ref vel, 0.7f);
	}

	public void ResetCamera(GameObject _target)
	{
		target = _target;
		transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, transform.position.z);
	}
}
