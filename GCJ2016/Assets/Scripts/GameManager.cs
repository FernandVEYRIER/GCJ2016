using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : GeneralManager {

	public static GameManager GM;

	[Header("HUD")]
	[SerializeField] private GameObject canvasPlay;
	[SerializeField] private GameObject canvasHide;
	[SerializeField] private GameObject canvasPause;
	[SerializeField] private Text textBullet;

	[Header("Player")]
	[SerializeField] private GameObject playerPrefab;
	[SerializeField] private CameraFollow camFollow;

	private Transform spawnPoint;

	private Coroutine fadeRoutine = null;

	void Awake()
	{
		GM = this;
	}

	void Start ()
	{
		canvasPlay.SetActive (true);
		canvasPause.SetActive (false);

		spawnPoint = GameObject.FindGameObjectWithTag ("Respawn").transform;
		SpawnPlayer ();
	}

	public void SpawnPlayer()
	{
		if (fadeRoutine != null)
		{
			StopCoroutine (fadeRoutine);
		}
		fadeRoutine = StartCoroutine (SpawnEffect (canvasHide));
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			ReloadLevel ();
			return;
		}

		if (Input.GetButtonDown("Cancel"))
		{
			Pause ();
		}

		if (Input.GetKeyDown(KeyCode.T))
		{
			SpawnPlayer ();
		}
	}

	override public void Pause()
	{
		base.Pause ();
		if (_gameState == GameState.PAUSE)
		{
			Time.timeScale = 0;
			canvasPause.SetActive (true);
			canvasPlay.SetActive (false);
		}
		else if (_gameState == GameState.PLAY)
		{
			Time.timeScale = 1;
			canvasPause.SetActive (false);
			canvasPlay.SetActive (true);
		}
	}

	IEnumerator SpawnEffect(GameObject obj)
	{
		Image image = obj.GetComponent<Image> ();
		Color col;

		for (col = image.color; col.a <= 1; col.a += 0.015f)
		{
			image.color = col;
			yield return new WaitForSeconds (0.001f);
		}
		col.a = 1;
		image.color = col;

		camFollow.ResetCamera((GameObject) Instantiate (playerPrefab, spawnPoint.position, Quaternion.identity));

		for (col = image.color; col.a >= 0; col.a -= 0.015f)
		{
			image.color = col;
			yield return new WaitForSeconds (0.001f);
		}
		col.a = 0;
		image.color = col;
	}

	public override void LoadLevel(int level)
	{
		Time.timeScale = 1;
		canvasPlay.SetActive (true);
		canvasPause.SetActive (false);
		StartCoroutine (LoadEffect (canvasHide, level));
	}

	IEnumerator LoadEffect(GameObject obj, int levelToLoad)
	{
		Image image = obj.GetComponent<Image> ();
		Color col;

		for (col = image.color; col.a <= 1; col.a += 0.015f)
		{
			image.color = col;
			yield return new WaitForSeconds (0.001f);
		}
		col.a = 1;
		image.color = col;

		SceneManager.LoadScene (levelToLoad);
	}

	public void UpdateAmmo(int bulletCount)
	{
		textBullet.text = "x " + bulletCount.ToString ();
	}
}
