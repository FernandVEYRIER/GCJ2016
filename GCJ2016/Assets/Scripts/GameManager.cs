using UnityEngine;
using System.Collections;

public class GameManager : GeneralManager {

	[Header("HUD")]
	[SerializeField] private GameObject canvasPlay;
	[SerializeField] private GameObject canvasPause;

	// Use this for initialization
	void Start ()
	{
		canvasPlay.SetActive (true);
		canvasPause.SetActive (false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown("Cancel"))
		{
			Pause ();
		}
	}

	override public void Pause()
	{
		base.Pause ();
		if (_gameState == GameState.PAUSE)
		{
			canvasPause.SetActive (true);
			canvasPlay.SetActive (false);
		}
		else if (_gameState == GameState.PLAY)
		{
			canvasPause.SetActive (false);
			canvasPlay.SetActive (true);
		}
	}
}
