using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	private GameObject target;

	private MeshRenderer[] rend;

	private Vector3 vel;

	void Start()
	{
		rend = GetComponentsInChildren<MeshRenderer> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (target == null)
		{
			target = GameObject.FindGameObjectWithTag ("Player");
			return;
		}

		Vector3 oldPos = transform.position;
		transform.position = Vector3.SmoothDamp (transform.position, new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z), ref vel, 0.7f);

		for (int i = 0; i < rend.Length; ++i)
		{
			rend [i].material.mainTextureOffset += (Vector2)(transform.position - oldPos) / 100f;
		}
	}

	public void ResetCamera(GameObject _target)
	{
		target = _target;
		transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, transform.position.z);
	}
}
