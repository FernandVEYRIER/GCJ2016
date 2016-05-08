using UnityEngine;
using System.Collections;

public class Jukebox : MonoBehaviour {

	static GameObject jukebox;

	void Awake()
	{
		if (jukebox == null)
		{
			jukebox = this.gameObject;
			DontDestroyOnLoad (this.gameObject);
		}
		else if (jukebox != this)
		{
			Destroy (this.gameObject);
		}
	}
}
