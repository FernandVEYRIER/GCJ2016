using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	[Header("Body")]
	[SerializeField] private GameObject arm;
	[SerializeField] private GameObject weapon;
	[SerializeField] private Transform groundCheck;

	[Header("Stats")]
	[SerializeField] private Animator animator;
	[SerializeField] private float maxVelocity = 1f;
	[SerializeField] private float jumpForce = 5f;
	[SerializeField] private LayerMask mask;
	[SerializeField] private GameObject bullet;
	public int maxGravityBalls = 3;

	private float velocity = 0;
	private bool jump = false;
	private bool isGrounded = true;
	private Rigidbody2D rb = null;

	private int animVelocity = Animator.StringToHash ("velocity");
	private int animGrounded = Animator.StringToHash ("isGrounded");

	private List<GameObject> gravityBalls = new List<GameObject>();

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateArm ();
		UpdateMovement ();

		if (Input.GetMouseButtonDown(0))
		{
			Shoot ();
		}

		UpdateAnims ();
	}

	void FixedUpdate()
	{
		Vector2 newVel;
		newVel.y = rb.velocity.y;

		newVel.x = (velocity == 0 || !isGrounded) ? rb.velocity.x : velocity;

		// Check if player is grounded
		RaycastHit2D rc = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, ~mask);
		isGrounded = (rc.transform != null && rc.transform.tag == "Ground");
		Debug.DrawRay (groundCheck.position, Vector2.down / 10.0f);

		if (jump && isGrounded)
		{
			newVel.y = jumpForce;
		}
		rb.velocity = newVel;
	}

	void UpdateMovement()
	{
		velocity = Input.GetAxis ("Horizontal") * maxVelocity;
		jump = Input.GetButton ("Jump");
	}

	void UpdateArm()
	{
		Vector3 armPos;
		Vector3 newRot;

		if (transform.localScale.x >= 0)
		{
			armPos = Input.mousePosition - Camera.main.WorldToScreenPoint (arm.transform.position);
			newRot = new Vector3 (0, 0, Mathf.Rad2Deg * (Mathf.Atan2 (armPos.y, armPos.x)));
		}
		else
		{
			armPos = Camera.main.WorldToScreenPoint (arm.transform.position) - Input.mousePosition;
			newRot = new Vector3 (0, 0, Mathf.Rad2Deg * (Mathf.Atan2 (armPos.y, armPos.x))) * -1;
		}

		arm.transform.rotation = Quaternion.Euler (newRot);
	}

	void UpdateAnims()
	{
		transform.localScale = new Vector3 ((velocity > 0) ? 1 : (velocity < 0) ? -1 : transform.localScale.x, 1, 1);
		animator.SetBool (animGrounded, isGrounded);
		animator.SetFloat (animVelocity, Mathf.Abs(velocity));
	}

	void Shoot()
	{
		if (GameManager.GM.gameState != GeneralManager.GameState.PLAY)
		{
			return;
		}

		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos.z = 0;
		GameObject go;

		RaycastHit2D hit = Physics2D.Linecast (weapon.transform.position, mousePos, LayerMask.NameToLayer("Ball"));
		if (gravityBalls.Count < maxGravityBalls)
		{
			go = (GameObject) Instantiate (bullet, weapon.transform.position, Quaternion.identity);
			gravityBalls.Add (go);
		}
		else
		{
			go = gravityBalls [0];
			gravityBalls.RemoveAt (0);
			gravityBalls.Add (go);
		}

		if (hit.transform == null)
		{
			go.GetComponent<GravityBall> ().SetTarget (weapon.transform.position, mousePos);
		}
		else
		{
			go.GetComponent<GravityBall> ().SetTarget (weapon.transform.position, hit.point);
		}
	}

	public void Die()
	{
		GameManager.GM.SpawnPlayer ();
		foreach (GameObject gBall in gravityBalls)
		{
			Destroy (gBall);
		}
	}
}
