using UnityEngine;
using System.Collections;

public class CustomEffector2D : MonoBehaviour {

    public float forceAngle = 90;
    public float forceMagnitude = 10;
    private bool push = true;
    private bool call = false;
    private GameObject current;

    public bool getPush()
    {
        return push;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (push && (col.tag == "Player" || col.tag == "Pushable"))
        {
            current = col.transform.gameObject;
            if (!call){
                call = true;
                Invoke("SetFalse", 0.2f);
            }
            Vector3 vecForce = Quaternion.Euler(0, 0, forceAngle) * new Vector3(forceMagnitude, 0, 0);
            current.GetComponent<Rigidbody2D>().AddRelativeForce(vecForce, ForceMode2D.Force);
        }
    }

    void SetFalse()
    {
        push = false;
        Invoke("SetTrue", 2f);
    }

    void SetTrue()
    {
        push = true;
        call = false;
    }
}
