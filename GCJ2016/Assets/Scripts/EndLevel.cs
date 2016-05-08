using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour {

	public int levelToLoad = 0;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
			GameManager.GM.LoadLevel (levelToLoad);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
			GameManager.GM.LoadLevel (levelToLoad);
	}
}
