using UnityEngine;
using System.Collections;

public class GravityBall : MonoBehaviour {

    public float sensibility = 2f;
    public float minForce = 1.6f;
    public float maxForce = 20f;
    private AreaEffector2D  effector;
    private Transform arrow;

	public float speed = 1f;
	private Vector2 target;
	private bool bHasTarget = false;

	// Use this for initialization
	void Start () {
		effector = GetComponent<AreaEffector2D> ();
        arrow = transform.GetChild(0);
        arrow.localScale = new Vector2(effector.forceMagnitude / maxForce, arrow.localScale.y);
		this.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
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
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            arrow.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Rad2Deg * (Mathf.Atan2(worldPos.y, worldPos.x))));
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
