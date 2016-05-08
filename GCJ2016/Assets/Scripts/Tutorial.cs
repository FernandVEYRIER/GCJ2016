using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {

	[SerializeField] private Text text;

	private GameObject player;
	private PlayerController playerController;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update()
	{
		if (player == null)
		{
			player = GameObject.FindGameObjectWithTag ("Player");
			return;
		}
		if (playerController == null)
		{
			playerController = player.GetComponent<PlayerController> ();
		}

		switch (SceneManager.GetActiveScene().buildIndex)
		{
			case 1:
				HandleLevel1 ();
				break;
			default:
				break;
		}
	}

	void HandleLevel1()
	{
		playerController.maxGravityBalls = 1;

		if (GameManager.GM.gameState == GeneralManager.GameState.PLAY && Input.GetMouseButtonDown(0))
		{
			ChangeText ("Once you placed the gravity ball, change its direction by right clicking it and dragging the arrow !");
		}
	}

	public void ChangeText(string _text)
	{
		text.text = _text;
	}
}
