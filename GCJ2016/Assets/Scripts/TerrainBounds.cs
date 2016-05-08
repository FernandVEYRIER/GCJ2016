using UnityEngine;
using System.Collections;

/// <summary>
/// Terrain bounds.
/// Whatever come through, IT WILL DIE
/// </summary>
public class TerrainBounds : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "GravityBall")
		{
			return;
		}
		if (col.tag == "Player")
		{
			col.gameObject.GetComponent<PlayerController> ().Die ();
		}
		Destroy (col.gameObject);
	}
}
