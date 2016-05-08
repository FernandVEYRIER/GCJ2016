using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour {

	public int levelToLoad = 0;

	void OnTriggerEnter2D(Collider2D col)
	{
		GameManager.GM.LoadLevel (levelToLoad);
	}
}
