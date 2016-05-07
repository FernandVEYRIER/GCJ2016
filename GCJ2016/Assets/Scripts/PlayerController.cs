using UnityEngine;
using System.Collections;

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

	private float velocity = 0;
	private bool jump = false;
	private bool isGrounded = true;
	private Rigidbody2D rb = null;

	private int animVelocity = Animator.StringToHash ("velocity");
	private int animGrounded = Animator.StringToHash ("isGrounded");

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
		UpdateAnims ();
	}

	void FixedUpdate()
	{
		Vector2 newVel = new Vector2(velocity, rb.velocity.y);

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
		jump = Input.GetButtonDown ("Jump");
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

	public void Die()
	{
		GameManager.GM.SpawnPlayer ();
	}
}
