using UnityEngine;
using System.Collections;

public class CustomEffector2D : MonoBehaviour {

    public float forceAngle = 90;
    public float forceMagnitude = 10;

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Vector3 vecForce = Quaternion.Euler(0, 0, forceAngle) * new Vector3(forceMagnitude, 0, 0);
            col.GetComponent<Rigidbody2D>().AddRelativeForce(vecForce, ForceMode2D.Force);
        }
    }

    IEnumerator AddForce(Transform obj)
    {
        yield return new WaitForSeconds(0.1f);
        if (obj.GetComponent<Rigidbody2D>())
        {
            Vector3 vecForce = Quaternion.Euler(0, 0, forceAngle) * new Vector3(forceMagnitude, 0, 0);
            obj.GetComponent<Rigidbody2D>().AddRelativeForce(vecForce, ForceMode2D.Force);
        }
        
    }
}
