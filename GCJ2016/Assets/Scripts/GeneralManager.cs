using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GeneralManager : MonoBehaviour {

	public const float gravityEarth = 9.81f;
	public const float gravityMoon = 1.6f;

	public enum GameState
	{
		PLAY, PAUSE
	}

	protected GameState _gameState = GameState.PLAY;

	public GameState gameState
	{ get { return _gameState; } }

	virtual public void LoadLevel(int level)
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

	virtual public void Pause()
	{
		if (_gameState == GameState.PAUSE)
		{
			_gameState = GameState.PLAY;
		}
		else if (_gameState == GameState.PLAY)
		{
			_gameState = GameState.PAUSE;
		}
	}
}
