using UnityEngine;
using System.Collections;

public class GravityBall : MonoBehaviour {

    public float sensibility = 2f;
    public float minForce = 1.6f;
    public float maxForce = 20f;
    private AreaEffector2D  effector;
    private Transform arrow;
	// Use this for initialization
	void Start () {
		effector = GetComponent<AreaEffector2D> ();
        arrow = transform.GetChild(0);
        arrow.localScale = new Vector2(effector.forceMagnitude / maxForce, arrow.localScale.y);
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
}
