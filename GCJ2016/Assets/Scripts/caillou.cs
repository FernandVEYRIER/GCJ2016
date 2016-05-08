using UnityEngine;
using System.Collections;

public class caillou : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Player")
            GetComponent<Rigidbody2D>().isKinematic = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.tag == "Player")
            GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
