using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public const float gravityEarth = 9.81f;
	public const float gravityMoon = 1.6f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadLevel(int level)
	{
		SceneManager.LoadSceneAsync (level);
	}

	public void ReloadLevel()
	{
		SceneManager.LoadSceneAsync (SceneManager.GetActiveScene ().buildIndex);
	}

	public void Quit()
	{
		Application.Quit ();
	}
}
