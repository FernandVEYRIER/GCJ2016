using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityBall : MonoBehaviour {

    public float sensibility = 2f;
    public float minForce = 1.6f;
    public float maxForce = 20f;
    private CustomEffector2D  effector;
    private Transform arrow;

	public float speed = 1f;
	private Vector2 target;
	private bool bHasTarget = false;
	private Transform playerArm;
    private List<SpriteRenderer> sprites = new List<SpriteRenderer>();

	// Use this for initialization
	void Start () {
		effector = GetComponent<CustomEffector2D> ();
        arrow = transform.GetChild(0);
        arrow.localScale = new Vector2(effector.forceMagnitude / maxForce, arrow.localScale.y);
		this.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
		playerArm = GameObject.FindGameObjectWithTag ("Player").transform.GetChild (0);
		arrow.rotation = playerArm.rotation;
		effector.forceAngle = arrow.eulerAngles.z;
        for (int i = 0; i < transform.childCount; i++)
        {
            sprites.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
        }
    }

	void OnMouseOver()
	{
        float scrollWhell = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWhell != 0)
        {
            effector.forceMagnitude = Mathf.Clamp(effector.forceMagnitude + scrollWhell * sensibility, minForce, maxForce);
            float persent = effector.forceMagnitude / maxForce;
            arrow.localScale = new Vector2(persent, arrow.localScale.y);
        }
		if (Input.GetMouseButton(1))
        {
			Vector3 wordPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			arrow.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Rad2Deg * (Mathf.Atan2(wordPos.y, wordPos.x))));
			effector.forceAngle = arrow.eulerAngles.z;
        }
    }

	void FixedUpdate()
	{
		if (bHasTarget)
		{
			Vector3 move = Vector3.MoveTowards (transform.position, target, speed);
			transform.position = move;
			if ((Vector2)move == target)
			{
				bHasTarget = false;
				this.transform.localScale = new Vector3 (1, 1, 1);
				arrow.rotation = playerArm.rotation;
				effector.forceAngle = arrow.eulerAngles.z;
			}
		}
        if (effector.getPush())
        {
            foreach (SpriteRenderer item in sprites)
            {
                item.color = Color.white;
            }
        }
        else
        {
            foreach (SpriteRenderer item in sprites)
            {
                item.color = Color.gray;
            }
        }
	}

	public void SetTarget(Vector3 from, Vector3 _target)
	{
		transform.position = from;
		target = _target;
		bHasTarget = true;
		this.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
	}
}
