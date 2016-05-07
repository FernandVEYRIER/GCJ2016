using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	public	float angle;
	public	float speed;
	private Vector3 dest;

	void Start()
	{
		dest = transform.localScale;
	}
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, 0, angle + speed * Time.deltaTime));
		if (dest == transform.localScale) {
			dest = new Vector3(Random.Range (0.8f, 1f), Random.Range (0.8f, 1f), 1);
		}
		transform.localScale = Vector3.Lerp (transform.localScale, dest, 5 * Time.deltaTime);
	}
}
